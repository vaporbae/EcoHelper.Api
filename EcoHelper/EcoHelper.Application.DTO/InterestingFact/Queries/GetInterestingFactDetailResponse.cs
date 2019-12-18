namespace EcoHelper.Application.DTO.InterestingFact.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetInterestingFactDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? DumpsterId { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.InterestingFact, GetInterestingFactDetailResponse>();
        }
    }
}
