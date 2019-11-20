namespace EcoHelper.Application.InterestingFacts.Commands.DeleteInterestingFact
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteInterestingFactCommand : IRequest
    {
        public DeleteInterestingFactRequest Data { get; set; }

        public DeleteInterestingFactCommand(DeleteInterestingFactRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<DeleteInterestingFactCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteInterestingFactCommand request, CancellationToken cancellationToken)
            {
                DeleteInterestingFactRequest data = request.Data;

                var InterestingFactRequest = await _uow.InterestingFactsRepository.GetByIdAsync(data.Id);
                if (InterestingFactRequest == null)
                {
                    throw new NotFoundException("InterestingFact", data.Id);
                }
                else
                {
                    _uow.InterestingFactsRepository.Remove(InterestingFactRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
