namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Application.DTO.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.DTO.User.Commands;
    using EcoHelper.Application.User.Commands.CreateUser;
    using EcoHelper.Application.User.Commands.UpdateUser;
    using EcoHelper.Application.User.Queries.GetUserDetails;
    using EcoHelper.Application.User.Queries.GetUsers;

    public class UserController : BaseController
    {
        #region Common
        //[HttpPost("/api/register")]
        //public async Task<IActionResult> Registration([FromBody]CreateUserRequest user)
        //{
        //    return Ok(await Mediator.Send(new CreateUserCommand(user)));
        //}

        [Authorize]
        [HttpPost("/api/user/update")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserRequest user)
        {
            return Ok(await Mediator.Send(new UpdateUserCommand(user)));
        }

        [Authorize]
        [HttpGet("/api/users")]
        public async Task<IActionResult> GetUsersList()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }
        #endregion
    }
}
