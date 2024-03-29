﻿namespace EcoHelper.Application.Answer.Commands.CreateAnswer
{
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using FluentValidation;
    using System.Linq;

    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerRequest>
    {
        public CreateAnswerCommandValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("You must set QuestionId.");
            RuleFor(x => x.QuestionId).MustAsync(async (request, val, token) =>
            {
                var result = await uow.QuestionsRepository.GetByIdAsync(val);

                if (result == null)
                {
                    throw new ValidationException("This question does not exist.");
                }

                return true;
            }).WithMessage("This question does not exist.");

            RuleFor(x => x.AnswerText).NotEmpty().WithMessage("Name cannot be empty");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                var result = await uow.QuestionsRepository.GetFirstAsync(x => x.Id == val.QuestionId, null, "Answers");
                var answers = result.Answers;

                if (answers != null)
                    if (answers.Where(y => y.AnswerText.ToLower().Equals(val.AnswerText.ToLower())).Count() > 0)
                    {
                        return false;
                    }

                return true;
            }).WithMessage("This answer to this question already exists.");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                var result = await uow.QuestionsRepository.GetFirstAsync(x => x.Id == val.QuestionId, null, "Answers");
                var answers = result.Answers;

                if (answers != null)
                    if (answers.Count() > 3)
                    {
                        return false;
                    }

                return true;
            }).WithMessage("This question already has 4 questions.");

            RuleFor(x => x).MustAsync(async (request, val, token) =>
            {
                if (val.IsCorrect == false)
                    return true;

                var result = await uow.QuestionsRepository.GetFirstAsync(x => x.Id == val.QuestionId, null, "Answers");
                var answers = result.Answers;

                if (answers != null)
                    if (answers.Where(y => y.IsCorrect == true).Count() > 0)
                    {
                        return false;
                    }

                return true;
            }).WithMessage("This question already has correct answer.");
        }
    }
}
