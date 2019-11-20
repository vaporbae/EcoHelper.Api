namespace EcoHelper.Application.DTO.InterestingFact.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetInterestingFactListResponse
    {
        public IList<InterestingFactLookupModel> InterestingFacts { get; set; }

        public class InterestingFactLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int? DumpsterId { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.InterestingFact, InterestingFactLookupModel>()
                    .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(cDTO => cDTO.Title, opt => opt.MapFrom(c => c.Title))
                    .ForMember(cDTO => cDTO.Description, opt => opt.MapFrom(c => c.Description))
                    .ForMember(cDTO => cDTO.DumpsterId, opt => opt.MapFrom(c => c.DumpsterId));
            }
        }
    }
}
