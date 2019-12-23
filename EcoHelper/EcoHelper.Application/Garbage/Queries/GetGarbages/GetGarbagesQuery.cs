namespace EcoHelper.Application.Garbage.Queries.GetGarbages
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using static EcoHelper.Application.DTO.Garbage.Queries.GetGarbageListResponse;

    public class GetGarbagesQuery : IRequest<GetGarbageListResponse>
    {
        public class Handler : IRequestHandler<GetGarbagesQuery, GetGarbageListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetGarbageListResponse> Handle(GetGarbagesQuery request, CancellationToken cancellationToken)
            {
                return new GetGarbageListResponse
                {
                    Garbages = await _uow.GarbagesRepository.ProjectTo<GarbageLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
