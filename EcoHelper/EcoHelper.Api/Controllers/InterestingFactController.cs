using Microsoft.AspNetCore.Authorization;
namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.InterestingFact.Commands.CreateInterestingFact;
    using EcoHelper.Application.InterestingFact.Queries.GetInterestingFactDetails;
    using EcoHelper.Application.InterestingFact.Queries.GetInterestingFacts;
    using EcoHelper.Application.InterestingFacts.Commands.DeleteInterestingFact;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.DTO.Common;

    public class InterestingFactController : BaseController
    {
        //[Authorize]
        [HttpPost("/api/InterestingFact/create")]
        public async Task<IActionResult> CreateInterestingFact([FromBody]CreateInterestingFactRequest InterestingFact)
        {
            var command = new CreateInterestingFactCommand(InterestingFact);

            return Ok(await Mediator.Send(command));
        }

        //[Authorize]
        [HttpPost("/api/InterestingFact/delete")]
        public async Task<IActionResult> DeleteInterestingFact([FromBody]DeleteInterestingFactRequest InterestingFact)
        {
            var command = new DeleteInterestingFactCommand(InterestingFact);

            return Ok(await Mediator.Send(command));
        }
        [HttpGet("/api/InterestingFacts")]
        public async Task<IActionResult> GetInterestingFacts()
        {
            return Ok(await Mediator.Send(new GetInterestingFactsQuery()));
        }

        [HttpGet("/api/InterestingFact/details/{id}")]
        public async Task<IActionResult> GetInterestingFactDetails(int id)
        {
            var query = new GetInterestingFactDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
