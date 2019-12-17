namespace EcoHelper.Application.DTO.Suggestion.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

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
                configuration.CreateMap<Domain.Entities.Suggestion, SuggestionLookupModel>()
                    .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(cDTO => cDTO.Dumpster, opt => opt.MapFrom(c => c.Dumpster))
                    .ForMember(cDTO => cDTO.Garbage, opt => opt.MapFrom(c => c.Garbage));
            }
        }
    }
}
