namespace EcoHelper.Application.Answers.Commands.DeleteAnswer
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteAnswerCommand : IRequest
    {
        private IdRequest answer;

        public DeleteAnswerRequest Data { get; set; }

        public DeleteAnswerCommand(DeleteAnswerRequest data)
        {
            this.Data = data;
        }

        public DeleteAnswerCommand(IdRequest answer)
        {
            this.answer = answer;
        }

        public class Handler : IRequestHandler<DeleteAnswerCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
            {
                DeleteAnswerRequest data = request.Data;

                var answerRequest = await _uow.AnswersRepository.GetByIdAsync(data.Id);
                if (answerRequest == null)
                {
                    throw new NotFoundException("Answer", data.Id);
                }
                else
                {
                    _uow.AnswersRepository.Remove(answerRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
