namespace EcoHelper.Application.Garbage.Commands.CreateGarbage
{
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using System.Linq;

    public class CreateGarbageCommandValidator : AbstractValidator<CreateGarbageRequest>
    {
        public CreateGarbageCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.DumpsterId).NotEmpty().WithMessage("You must set DumpsterId.");
            RuleFor(x => x.DumpsterId).MustAsync(async (request, val, token) =>
            {
                var result = await uow.DumpstersRepository.GetByIdAsync(val);

                if (result == null)
                {
                    throw new ValidationException("This dumpster does not exist.");
                }

                return true;
            }).WithMessage("This dumpster does not exist.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                var result = await uow.GarbagesRepository.GetAllAsync();

                if (result.Where(y => y.Name.ToLower().Equals(val.Name.ToLower())).Count() > 0)
                {
                    return false;
                }

                return true;
            }).WithMessage("This Garbage already exists.");
        }
    }
}
