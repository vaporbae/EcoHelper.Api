namespace EcoHelper.Application.Suggestions.Commands.DeleteSuggestion
{
    using System.Threading;
    using System.Threading.Tasks;
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using MediatR;

    public class DeleteSuggestionCommand : IRequest
    {

        public DeleteSuggestionRequest Data { get; set; }

        public DeleteSuggestionCommand(DeleteSuggestionRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<DeleteSuggestionCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(DeleteSuggestionCommand request, CancellationToken cancellationToken)
            {
                DeleteSuggestionRequest data = request.Data;

                var SuggestionRequest = await _uow.SuggestionsRepository.GetByIdAsync(data.Id);
                if (SuggestionRequest == null)
                {
                    throw new NotFoundException("Suggestion", data.Id);
                }
                else
                {
                    _uow.SuggestionsRepository.Remove(SuggestionRequest);
                }

                await _uow.SaveChangesAsync();

                return await Unit.Task;
            }
        }
    }
}
