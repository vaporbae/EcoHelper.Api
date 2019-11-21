namespace EcoHelper.Application.Question.Commands.CreateQuestion
{
    using FluentValidation;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Question.Commands;
    using System.Linq;

    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("You must set Question Text.");
            RuleFor(x => x.QuestionText).MustAsync(async (request, val, token) =>
            {
                var result = await uow.QuestionsRepository.GetAllAsync();

                if (result.Where(y => y.QuestionText.ToLower().Equals(val.ToLower())).Count() > 0)
                {
                    return false;
                }

                return true;
            }).WithMessage("This Question already exists.");
        }
    }
}
