namespace EcoHelper.Application.Suggestion.Queries.GetSuggestionDetails
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSuggestionDetailsQuery : IRequest<GetSuggestionDetailResponse>
    {
        public IdRequest Data { get; set; }

        public GetSuggestionDetailsQuery(IdRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<GetSuggestionDetailsQuery, GetSuggestionDetailResponse>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<GetSuggestionDetailResponse> Handle(GetSuggestionDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.SuggestionsRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Suggestion), data.Id);
                }

                return _mapper.Map<GetSuggestionDetailResponse>(entity);
            }
        }
    }
}
