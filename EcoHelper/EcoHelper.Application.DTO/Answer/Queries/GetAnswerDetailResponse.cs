namespace EcoHelper.Application.DTO.Answer.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EcoHelper.Application.DTO.Answer.Commands;

    public class GetAnswerDetailResponse
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public static Expression<Func<Domain.Entities.Answer, GetAnswerDetailResponse>> Projection
        {
            get
            {
                return Answer => new GetAnswerDetailResponse
                {
                    Id = Answer.Id,
                    AnswerText = Answer.AnswerText,
                    IsCorrect = Answer.IsCorrect
                };
            }
        }

        public static GetAnswerDetailResponse Create(Domain.Entities.Answer Answer)
        {
            return Projection.Compile().Invoke(Answer);
        }
    }
}
