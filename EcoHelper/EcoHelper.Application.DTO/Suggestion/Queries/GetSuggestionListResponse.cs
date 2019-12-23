namespace EcoHelper.Application.DTO.Suggestion.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

    public class GetSuggestionListResponse
    {
        public IList<SuggestionLookupModel> Suggestions { get; set; }

        public class SuggestionLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string Dumpster { get; set; }
            public string Garbage { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.Suggestion, SuggestionLookupModel>();
            }
        }
    }
}
