namespace EcoHelper.Application.DTO.InterestingFact.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

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
                configuration.CreateMap<Domain.Entities.InterestingFact, InterestingFactLookupModel>();
            }
        }
    }
}
