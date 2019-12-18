namespace EcoHelper.Application.DTO.Answer.Queries
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class GetBaseVersionDetailResponse : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Ver { get; set; }

        void IHaveCustomMapping.CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.BaseVersion, GetBaseVersionDetailResponse>();
        }
    }
}
