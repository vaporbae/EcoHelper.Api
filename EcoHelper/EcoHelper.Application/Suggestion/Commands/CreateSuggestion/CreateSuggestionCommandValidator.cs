namespace EcoHelper.Application.Suggestion.Commands.CreateSuggestion
{
    using FluentValidation;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Suggestion.Commands;

    public class CreateSuggestionCommandValidator : AbstractValidator<CreateSuggestionRequest>
    {
        public CreateSuggestionCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Dumpster).NotEmpty().WithMessage("Dumpster name cannot be empty.");
            RuleFor(x => x.Garbage).NotEmpty().WithMessage("Garbage name cannot be empty.");
            
        }
    }
}
