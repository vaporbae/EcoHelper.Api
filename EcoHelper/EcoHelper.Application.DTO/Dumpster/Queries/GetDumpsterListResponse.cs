namespace EcoHelper.Application.DTO.Dumpster.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetDumpsterListResponse
    {
        public IList<DumpsterLookupModel> Dumpsters { get; set; }

        public class DumpsterLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public IList<Domain.Entities.Garbage> Garbages { get; set; }
            public IList<Domain.Entities.InterestingFact> InterestingFacts { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.Dumpster, DumpsterLookupModel>()
                    .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                    .ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.Name))
                    .ForMember(cDTO => cDTO.Garbages, opt => opt.MapFrom(c => c.Garbages.ToList()))
                    .ForMember(cDTO => cDTO.InterestingFacts, opt => opt.MapFrom(c => c.InterestingFacts.ToList()));
            }
        }
    }
}
