namespace EcoHelper.Application.InterestingFact.Queries.GetInterestingFacts
{
    using AutoMapper;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using static EcoHelper.Application.DTO.InterestingFact.Queries.GetInterestingFactListResponse;

    public class GetInterestingFactsQuery : IRequest<GetInterestingFactListResponse>
    {
        public class Handler : IRequestHandler<GetInterestingFactsQuery, GetInterestingFactListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetInterestingFactListResponse> Handle(GetInterestingFactsQuery request, CancellationToken cancellationToken)
            {
                return new GetInterestingFactListResponse
                {
                    InterestingFacts = await _uow.InterestingFactsRepository.ProjectTo<InterestingFactLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
