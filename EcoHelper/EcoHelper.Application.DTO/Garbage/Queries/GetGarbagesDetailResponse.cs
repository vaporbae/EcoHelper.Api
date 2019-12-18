namespace EcoHelper.Application.DTO.Garbage.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetGarbageDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DumpsterId { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Garbage, GetGarbageDetailResponse>();
        }
    }
}
