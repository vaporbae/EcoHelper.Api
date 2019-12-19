namespace EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
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
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetDumpsterDetailResponse> Handle(GetDumpsterDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.DumpstersRepository.GetFirstAsync(x => x.Id == data.Id, null, "Garbages,InterestingFacts");

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.Dumpster), data.Id);
                }

                return _mapper.Map<GetDumpsterDetailResponse>(entity);
            }
        }
    }
}
