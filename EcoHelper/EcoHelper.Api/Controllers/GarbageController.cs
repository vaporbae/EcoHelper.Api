﻿namespace EcoHelper.Api.Controllers
{
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Garbage.Commands.CreateGarbage;
    using EcoHelper.Application.Garbage.Queries.GetGarbageDetails;
    using EcoHelper.Application.Garbage.Queries.GetGarbages;
    using EcoHelper.Application.Garbages.Commands.DeleteGarbage;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        [HttpDelete("/api/Garbage/delete")]
        public async Task<IActionResult> DeleteGarbage([FromBody]DeleteGarbageRequest Garbage)
        {
            var command = new DeleteGarbageCommand(Garbage);

            return Ok(await Mediator.Send(command));
        }
        [HttpGet("/api/Garbages")]
        public async Task<IActionResult> GetGarbages()
        {
            return Ok(await Mediator.Send(new GetGarbagesQuery()));
        }
        [HttpGet("/api/Garbage/details/{id}")]
        public async Task<IActionResult> GetGarbageDetails(int id)
        {
            var query = new GetGarbageDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
