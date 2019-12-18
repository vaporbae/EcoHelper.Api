namespace EcoHelper.Application.Question.Commands.CreateQuestion
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Question.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;

    public class CreateQuestionCommand : IRequest
    {
        public CreateQuestionRequest Data { get; set; }

        public CreateQuestionCommand(CreateQuestionRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateQuestionCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
            {
                CreateQuestionRequest data = request.Data;

                var vResult = await new CreateQuestionCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityQuestion = new Domain.Entities.Question
                {
                    QuestionText = data.QuestionText,
                    Answers = new List<Domain.Entities.Answer>()
                };

                _uow.QuestionsRepository.Add(entityQuestion);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
