namespace EcoHelper.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.Authentication.Commands.ResetPassword;
    using EcoHelper.Application.Authentication.Queries.Authentication;
    using EcoHelper.Application.Authentication.Queries.GetResetPasswordToken;
    using EcoHelper.Application.DTO.Authentication.Queries;

    public class AuthenticationController : BaseController
    {
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest model)
        {
            return Ok(await Mediator.Send(new GetValidTokenQuery(model)));
        }

        [HttpPost("/api/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]GetResetPasswordTokenQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("/api/resetPassword/{token}")]
        public async Task<IActionResult> ResetPassword(string token, [FromBody]ResetPasswordCommand request)
        {
            request.Token = token;

            return Ok(await Mediator.Send(request));
        }
    }
}
