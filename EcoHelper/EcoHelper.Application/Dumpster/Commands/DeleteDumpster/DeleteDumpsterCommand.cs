namespace EcoHelper.Application.Dumpsters.Commands.DeleteDumpster
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteDumpsterCommand : IRequest
    {

        public DeleteDumpsterRequest Data { get; set; }

        public DeleteDumpsterCommand(DeleteDumpsterRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<DeleteDumpsterCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteDumpsterCommand request, CancellationToken cancellationToken)
            {
                DeleteDumpsterRequest data = request.Data;

                var DumpsterRequest = await _uow.DumpstersRepository.GetByIdAsync(data.Id);
                if (DumpsterRequest == null)
                {
                    throw new NotFoundException("Dumpster", data.Id);
                }
                else
                {
                    _uow.DumpstersRepository.Remove(DumpsterRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
