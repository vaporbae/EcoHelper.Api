namespace EcoHelper.Application.Garbages.Commands.DeleteGarbage
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteGarbageCommand : IRequest
    {
        public DeleteGarbageRequest Data { get; set; }

        public DeleteGarbageCommand(DeleteGarbageRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<DeleteGarbageCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteGarbageCommand request, CancellationToken cancellationToken)
            {
                DeleteGarbageRequest data = request.Data;

                var GarbageRequest = await _uow.GarbagesRepository.GetByIdAsync(data.Id);
                if (GarbageRequest == null)
                {
                    throw new NotFoundException("Garbage", data.Id);
                }
                else
                {
                    _uow.GarbagesRepository.Remove(GarbageRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
