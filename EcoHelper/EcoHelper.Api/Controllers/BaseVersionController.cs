using Microsoft.AspNetCore.Authorization;
namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.BaseVersion.Commands.CreateBaseVersion;
    using EcoHelper.Application.DTO.BaseVersion.Commands;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.BaseVersion.Queries.GetAnswerDetails;

    public class BaseVersionController : BaseController
    {
        [Authorize]
        [HttpPost("/api/BaseVersion/create")]
        public async Task<IActionResult> CreateBaseVersion([FromBody]CreateBaseVersionRequest BaseVersion)
        {
            var command = new CreateBaseVersionCommand(BaseVersion);

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpGet("/api/BaseVersion/details/{id}")]
        public async Task<IActionResult> GetBaseVersionDetails(int id)
        {
            var query = new GetBaseVersionDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
