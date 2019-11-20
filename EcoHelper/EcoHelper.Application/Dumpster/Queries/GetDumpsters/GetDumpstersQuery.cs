namespace EcoHelper.Application.Dumpster.Queries.GetDumpsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoHelper.Application.DTO.Dumpster.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using static EcoHelper.Application.DTO.Dumpster.Queries.GetDumpsterListResponse;

    public class GetDumpstersQuery : IRequest<GetDumpsterListResponse>
    {
        public class Handler : IRequestHandler<GetDumpstersQuery, GetDumpsterListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetDumpsterListResponse> Handle(GetDumpstersQuery request, CancellationToken cancellationToken)
            {
                return new GetDumpsterListResponse
                {
                    Dumpsters = await _uow.DumpstersRepository.ProjectTo<DumpsterLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
