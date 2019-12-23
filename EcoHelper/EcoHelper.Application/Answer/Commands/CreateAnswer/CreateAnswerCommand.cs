namespace EcoHelper.Application.Answer.Commands.CreateAnswer
{
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateAnswerCommand : IRequest
    {
        public CreateAnswerRequest Data { get; set; }

        public CreateAnswerCommand(CreateAnswerRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateAnswerCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
            {
                CreateAnswerRequest data = request.Data;

                var vResult = await new CreateAnswerCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entityAnswer = new EcoHelper.Domain.Entities.Answer
                {
                    AnswerText = data.AnswerText,
                    IsCorrect = data.IsCorrect,
                    QuestionId = data.QuestionId
                };

                _uow.AnswersRepository.Add(entityAnswer);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
