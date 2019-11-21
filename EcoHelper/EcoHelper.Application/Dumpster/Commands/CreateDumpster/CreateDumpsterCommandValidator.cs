namespace EcoHelper.Application.Dumpster.Commands.CreateDumpster
{
    using FluentValidation;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using System.Linq;

    public class CreateDumpsterCommandValidator : AbstractValidator<CreateDumpsterRequest>
    {
        public CreateDumpsterCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("You must set dumpster name.");
            RuleFor(x => x.Name).MustAsync(async (request, val, token) =>
            {
                var result = await uow.DumpstersRepository.GetAllAsync();

                if (result.Where(y=>y.Name.ToLower().Equals(val.ToLower())).Count()>0)
                {
                    return false;
                }

                return true;
            }).WithMessage("This dumpster already exists.");

            
        }
    }
}
