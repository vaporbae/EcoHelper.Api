namespace EcoHelper.Application.DTO.User
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Interfaces.Mapping;

    public class UserLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.User, UserLookupModel>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Email, opt => opt.MapFrom(c => c.Email));
        }
    }
}
