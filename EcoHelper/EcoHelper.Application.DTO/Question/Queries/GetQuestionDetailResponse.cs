namespace EcoHelper.Application.DTO.Question.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

    public class GetQuestionDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public IList<GetAnswerDetailResponse> Answers { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Question, GetQuestionDetailResponse>();
        }
    }
}
