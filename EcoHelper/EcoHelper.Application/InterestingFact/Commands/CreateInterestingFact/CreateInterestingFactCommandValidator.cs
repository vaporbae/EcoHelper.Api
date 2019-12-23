namespace EcoHelper.Application.InterestingFact.Commands.CreateInterestingFact
{
    using FluentValidation;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using System.Linq;

    public class CreateInterestingFactCommandValidator : AbstractValidator<CreateInterestingFactRequest>
    {
        public CreateInterestingFactCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("You must set Title.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("You must set Description.");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                var dumpsterId = val.DumpsterId ?? default(int);
                if (val.DumpsterId == null)
                {
                    return true;
                }
                else
                {
                    var result = await uow.DumpstersRepository.GetByIdAsync(dumpsterId);

                    if (result==null)
                    {
                        throw new ValidationException("This dumpster does not exist.");
                    }

                    return true;
                }

            }).WithMessage("This dumpster does not exist.");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                var dumpsterId = val.DumpsterId ?? default(int);
                if (val.DumpsterId == null)
                {
                    return true;
                }
                else
                {
                    var result = await uow.DumpstersRepository.GetFirstAsync(x => x.Id == val.DumpsterId, null, "InterestingFacts");
                    var dumpsterFacts = result.InterestingFacts;
                    
                    if(dumpsterFacts!=null)
                    if (dumpsterFacts.Where(y=>y.Title.ToLower().Equals(val.Title.ToLower())).Count()>0)
                    {
                        return false;
                    }

                    return true;
                }
               
            }).WithMessage("This fact already exists for this dumpster.");
        }
    }
}
