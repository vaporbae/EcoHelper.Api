namespace EcoHelper.Application.Interfaces
{
    using EcoHelper.Application.DTO.Authentication;

    public interface IJwtService
    {
        JwtTokenModel GenerateJwtToken(string email, int id, bool isAdmin = false, bool isReset = false);
        bool ValidateStringToken(string token);
        int GetUserIdFromToken(string token);
        bool IsResetPasswordToken(string token);
    }
}
