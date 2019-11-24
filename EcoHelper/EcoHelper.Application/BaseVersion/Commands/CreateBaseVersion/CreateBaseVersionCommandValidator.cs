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
            RuleFor(x => x.Ver).GreaterThan(0).WithMessage("Value must be bigger than 0");

            RuleFor(x => x.Ver).MustAsync(async (request, val, token) =>
            {
                var result = await uow.BaseVersionsRepository.GetAllAsync();
                var result2 = result.ToList();
                if (result2.Count() > 0)
                {
                    var lastVer = result.Where(x => x.Id == result2[(result.Count() - 1)].Id).First();

                    if (lastVer.Ver>val)
                    {
                        throw new ValidationException("Ver must be bigger than the last one.");
                    }
                }
                return true;
            }).WithMessage("Ver must be bigger than the last one.");
        }
    }
}
