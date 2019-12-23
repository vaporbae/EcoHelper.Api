namespace EcoHelper.Api.Controllers
{
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Question.Commands;
    using EcoHelper.Application.Question.Commands.CreateQuestion;
    using EcoHelper.Application.Question.Queries.GetQuestionDetails;
    using EcoHelper.Application.Question.Queries.GetQuestions;
    using EcoHelper.Application.Questions.Commands.DeleteQuestion;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class QuestionController : BaseController
    {
        [Authorize]
        [HttpPost("/api/Question/create")]
        public async Task<IActionResult> CreateQuestion([FromBody]CreateQuestionRequest Question)
        {
            var command = new CreateQuestionCommand(Question);

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpDelete("/api/Question/delete")]
        public async Task<IActionResult> DeleteQuestion([FromBody]DeleteQuestionRequest Question)
        {
            var command = new DeleteQuestionCommand(Question);

            return Ok(await Mediator.Send(command));
        }
        [HttpGet("/api/Questions")]
        public async Task<IActionResult> GetQuestions()
        {
            return Ok(await Mediator.Send(new GetQuestionsQuery()));
        }

        [HttpGet("/api/Question/details/{id}")]
        public async Task<IActionResult> GetQuestionDetails(int id)
        {
            var query = new GetQuestionDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
