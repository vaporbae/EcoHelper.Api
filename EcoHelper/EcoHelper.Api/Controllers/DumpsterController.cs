﻿namespace EcoHelper.Api.Controllers
{
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using EcoHelper.Application.Dumpster.Commands.CreateDumpster;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsters;
    using EcoHelper.Application.Dumpsters.Commands.DeleteDumpster;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        [HttpDelete("/api/Dumpster/delete")]
        public async Task<IActionResult> DeleteDumpster([FromBody]DeleteDumpsterRequest Dumpster)
        {
            var command = new DeleteDumpsterCommand(Dumpster);

            return Ok(await Mediator.Send(command));
        }
        [HttpGet("/api/Dumpsters")]
        public async Task<IActionResult> GetDumpsters()
        {
            return Ok(await Mediator.Send(new GetDumpstersQuery()));
        }

        [HttpGet("/api/Dumpster/details/{id}")]
        public async Task<IActionResult> GetDumpsterDetails(int id)
        {
            var query = new GetDumpsterDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
