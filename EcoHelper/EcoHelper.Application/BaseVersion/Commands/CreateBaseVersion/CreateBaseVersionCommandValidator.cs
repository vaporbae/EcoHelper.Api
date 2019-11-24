namespace EcoHelper.Application.BaseVersion.Commands.CreateBaseVersion
{
    using FluentValidation;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.BaseVersion.Commands;
    using System.Linq;
    using System;

    public class CreateBaseVersionCommandValidator : AbstractValidator<CreateBaseVersionRequest>
    {
        public CreateBaseVersionCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Ver).NotEmpty().WithMessage("You must set Ver.");
            RuleFor(x => x.Ver).MustAsync(async (request, val, token) =>
            {

                double p;
                if (double.TryParse(val,out p))
                {
                    throw new ValidationException("Ver must be convertable to double.");
                }

                return true;
            }).WithMessage("Ver must be convertable to double.");

            RuleFor(x => x.Ver).MustAsync(async (request, val, token) =>
            {
                var result = await uow.BaseVersionsRepository.GetAllAsync();
                var lastVer = result.Where(x => x.Id == (result.Count() - 1));

                if (Convert.ToDouble(lastVer)>Convert.ToDouble(val))
                {
                    return false;
                }

                return true;
            }).WithMessage("Ver must be bigger than the last one.");
        }
    }
}
