namespace EcoHelper.Application.Questions.Commands.DeleteQuestion
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Question.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteQuestionCommand : IRequest
    {
        public DeleteQuestionRequest Data { get; set; }

        public DeleteQuestionCommand(DeleteQuestionRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<DeleteQuestionCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
            {
                DeleteQuestionRequest data = request.Data;

                var QuestionRequest = await _uow.QuestionsRepository.GetByIdAsync(data.Id);
                if (QuestionRequest == null)
                {
                    throw new NotFoundException("Question", data.Id);
                }
                else
                {
                    _uow.QuestionsRepository.Remove(QuestionRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
