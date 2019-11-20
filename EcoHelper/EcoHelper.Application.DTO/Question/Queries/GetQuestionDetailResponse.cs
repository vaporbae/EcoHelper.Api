namespace EcoHelper.Application.DTO.Question.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class GetQuestionDetailResponse
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public IList<Domain.Entities.Answer> Answers { get; set; }

        public static Expression<Func<Domain.Entities.Question, GetQuestionDetailResponse>> Projection
        {
            get
            {
                return Question => new GetQuestionDetailResponse
                {
                    Id = Question.Id,
                    QuestionText = Question.QuestionText,
                    Answers = Question.Answers.ToList()
                };
            }
        }

        public static GetQuestionDetailResponse Create(Domain.Entities.Question Question)
        {
            return Projection.Compile().Invoke(Question);
        }
    }
}
