namespace EcoHelper.Application.Suggestion.Queries.GetSuggestions
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using static EcoHelper.Application.DTO.Suggestion.Queries.GetSuggestionListResponse;

    public class GetSuggestionsQuery : IRequest<GetSuggestionListResponse>
    {
        public class Handler : IRequestHandler<GetSuggestionsQuery, GetSuggestionListResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetSuggestionListResponse> Handle(GetSuggestionsQuery request, CancellationToken cancellationToken)
            {
                return new GetSuggestionListResponse
                {
                    Suggestions = await _uow.SuggestionsRepository.ProjectTo<SuggestionLookupModel>(_mapper, cancellationToken)
                };
            }
        }
    }
}
