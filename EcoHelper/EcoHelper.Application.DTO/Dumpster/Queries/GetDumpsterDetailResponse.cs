namespace EcoHelper.Application.DTO.Dumpster.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

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
