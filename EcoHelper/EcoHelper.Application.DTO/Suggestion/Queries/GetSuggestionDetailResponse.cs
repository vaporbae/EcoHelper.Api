namespace EcoHelper.Application.DTO.Suggestion.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetSuggestionDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Dumpster { get; set; }
        public string Garbage { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Suggestion, GetSuggestionDetailResponse>();
        }
    }
}
