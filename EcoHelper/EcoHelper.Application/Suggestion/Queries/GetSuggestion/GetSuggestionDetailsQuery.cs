namespace EcoHelper.Application.Suggestion.Queries.GetSuggestionDetails
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

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

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<GetSuggestionDetailResponse> Handle(GetSuggestionDetailsQuery request, CancellationToken cancellationToken)
            {
                IdRequest data = request.Data;

                var entity = await _uow.SuggestionsRepository.GetByIdAsync(data.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(EcoHelper.Domain.Entities.Suggestion), data.Id);
                }

                return GetSuggestionDetailResponse.Create(entity);
            }
        }
    }
}
