namespace EcoHelper.Application.DTO.Dumpster.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

    public class GetDumpsterDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<GetGarbageDetailResponse> Garbages { get; set; }
        public IList<GetInterestingFactDetailResponse> InterestingFacts { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Dumpster, GetDumpsterDetailResponse>();
        }
    }
}
