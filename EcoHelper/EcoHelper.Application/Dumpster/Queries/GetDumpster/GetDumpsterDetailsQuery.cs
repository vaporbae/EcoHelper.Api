namespace EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Dumpster.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class GetDumpsterDetailsQuery : IRequest<GetDumpsterDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetDumpsterDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetDumpsterDetailsQuery, GetDumpsterDetailResponse>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetDumpsterDetailResponse> Handle(GetDumpsterDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.DumpstersRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.Dumpster), data.Id);
                }

                return GetDumpsterDetailResponse.Create(entity);
            }
        }
    }
}
