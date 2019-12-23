﻿namespace EcoHelper.Application.DTO.Question.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

    public class GetQuestionListResponse
    {
        public IList<QuestionLookupModel> Questions { get; set; }

        public class QuestionLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string QuestionText { get; set; }

            public IList<Domain.Entities.Answer> Answers { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.Question, QuestionLookupModel>();
            }
        }
    }
}
