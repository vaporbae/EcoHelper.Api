using Microsoft.AspNetCore.Authorization;
namespace EcoHelper.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using EcoHelper.Application.Answer.Commands.CreateAnswer;
    using EcoHelper.Application.Answer.Queries.GetAnswerDetails;
    using EcoHelper.Application.Answer.Queries.GetAnswers;
    using EcoHelper.Application.Answers.Commands.DeleteAnswer;
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.DTO.Common;

    public class AnswerController :BaseController
    {
        //[Authorize]
        [HttpPost("/api/answer/create")]
        public async Task<IActionResult> CreateAnswer([FromBody]CreateAnswerRequest answer)
        {
            var command = new CreateAnswerCommand(answer);

            return Ok(await Mediator.Send(command));
        }

        //[Authorize]
        [HttpPost("/api/answer/delete")]
        public async Task<IActionResult> DeleteAnswer([FromBody]DeleteAnswerRequest Answer)
        {
            var command = new DeleteAnswerCommand(Answer);

            return Ok(await Mediator.Send(command));
        }
        [HttpGet("/api/Answers")]
        public async Task<IActionResult> GetAnswers()
        {
            return Ok(await Mediator.Send(new GetAnswersQuery()));
        }

        [HttpGet("/api/Answer/details/{id}")]
        public async Task<IActionResult> GetAnswerDetails(int id)
        {
            var query = new GetAnswerDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
