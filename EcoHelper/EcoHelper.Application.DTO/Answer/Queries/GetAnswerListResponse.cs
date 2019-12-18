namespace EcoHelper.Application.DTO.Answer.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetAnswerListResponse
    {
        public IList<AnswerLookupModel> Answers { get; set; }

        public class AnswerLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string AnswerText { get; set; }
            public bool IsCorrect { get; set; }
            public int QuestionId { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.Answer, AnswerLookupModel>();
            }
        }
    }
}
