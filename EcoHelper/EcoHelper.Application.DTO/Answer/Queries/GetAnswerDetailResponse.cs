namespace EcoHelper.Application.DTO.Answer.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetAnswerDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Answer, GetAnswerDetailResponse>();
        }
    }
}
