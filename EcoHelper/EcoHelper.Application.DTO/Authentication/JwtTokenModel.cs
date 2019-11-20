namespace EcoHelper.Application.DTO.Authentication
{
    using System;

    public class JwtTokenModel
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
