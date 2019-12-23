namespace EcoHelper.Application.Suggestion.Commands.CreateSuggestion
{
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateSuggestionCommand : IRequest
    {
        public CreateSuggestionRequest Data { get; set; }

        public CreateSuggestionCommand(CreateSuggestionRequest data)
        {
            this.Data = data;
        }

        public class Handler : IRequestHandler<CreateSuggestionCommand, Unit>
        {
            private readonly IUnitOfWork _uow;

            public Handler(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<Unit> Handle(CreateSuggestionCommand request, CancellationToken cancellationToken)
            {
                CreateSuggestionRequest data = request.Data;

                var vResult = await new CreateSuggestionCommandValidator(_uow).ValidateAsync(data, cancellationToken);

                if (!vResult.IsValid)
                {
                    throw new ValidationException(vResult.Errors);
                }


                var entitySuggestion = new EcoHelper.Domain.Entities.Suggestion
                {
                    Dumpster = data.Dumpster,
                    Garbage = data.Garbage
                };

                _uow.SuggestionsRepository.Add(entitySuggestion);

                await _uow.SaveChangesAsync(cancellationToken);

                return await Unit.Task;
            }
        }
    }
}
