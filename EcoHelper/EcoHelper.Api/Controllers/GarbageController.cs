using Microsoft.AspNetCore.Authorization;
namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.Garbage.Commands.CreateGarbage;
    using EcoHelper.Application.Garbage.Queries.GetGarbageDetails;
    using EcoHelper.Application.Garbage.Queries.GetGarbages;
    using EcoHelper.Application.Garbages.Commands.DeleteGarbage;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.DTO.Common;

    public class GarbageController : BaseController
    {
        [Authorize]
        [HttpPost("/api/Garbage/create")]
        public async Task<IActionResult> CreateGarbage([FromBody]CreateGarbageRequest Garbage)
        {
            var command = new CreateGarbageCommand(Garbage);

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("/api/Garbage/delete")]
        public async Task<IActionResult> DeleteGarbage([FromBody]DeleteGarbageRequest Garbage)
        {
            var command = new DeleteGarbageCommand(Garbage);

            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("/api/Garbages")]
        public async Task<IActionResult> GetGarbages()
        {
            return Ok(await Mediator.Send(new GetGarbagesQuery()));
        }

        [Authorize]
        [HttpGet("/api/Garbage/details/{id}")]
        public async Task<IActionResult> GetGarbageDetails(int id)
        {
            var query = new GetGarbageDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
