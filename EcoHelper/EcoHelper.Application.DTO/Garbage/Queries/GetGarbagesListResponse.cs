namespace EcoHelper.Application.DTO.Garbage.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;
    using System.Collections.Generic;

    public class GetGarbageListResponse
    {
        public IList<GarbageLookupModel> Garbages { get; set; }

        public class GarbageLookupModel : IHaveCustomMapping
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DumpsterId { get; set; }

            public void CreateMappings(Profile configuration)
            {
                configuration.CreateMap<Domain.Entities.Garbage, GarbageLookupModel>();
            }
        }
    }
}
