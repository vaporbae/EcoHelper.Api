using Microsoft.AspNetCore.Authorization;
namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.Dumpster.Commands.CreateDumpster;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsters;
    using EcoHelper.Application.Dumpsters.Commands.DeleteDumpster;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using EcoHelper.Application.DTO.Common;

    public class DumpsterController : BaseController
    {
        [Authorize]
        [HttpPost("/api/Dumpster/create")]
        public async Task<IActionResult> CreateDumpster([FromBody]CreateDumpsterRequest Dumpster)
        {
            var command = new CreateDumpsterCommand(Dumpster);

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("/api/Dumpster/delete")]
        public async Task<IActionResult> DeleteDumpster([FromBody]DeleteDumpsterRequest Dumpster)
        {
            var command = new DeleteDumpsterCommand(Dumpster);

            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("/api/Dumpsters")]
        public async Task<IActionResult> GetDumpsters()
        {
            return Ok(await Mediator.Send(new GetDumpstersQuery()));
        }

        [Authorize]
        [HttpGet("/api/Dumpster/details/{id}")]
        public async Task<IActionResult> GetDumpsterDetails(int id)
        {
            var query = new GetDumpsterDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
