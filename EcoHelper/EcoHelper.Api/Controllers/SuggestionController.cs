﻿namespace EcoHelper.Api.Controllers
{
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.Suggestion.Commands.CreateSuggestion;
    using EcoHelper.Application.Suggestion.Queries.GetSuggestionDetails;
    using EcoHelper.Application.Suggestion.Queries.GetSuggestions;
    using EcoHelper.Application.Suggestions.Commands.DeleteSuggestion;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SuggestionController : BaseController
    {
        [HttpPost("/api/Suggestion/create")]
        public async Task<IActionResult> CreateSuggestion([FromBody]CreateSuggestionRequest Suggestion)
        {
            var command = new CreateSuggestionCommand(Suggestion);

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpDelete("/api/Suggestion/delete")]
        public async Task<IActionResult> DeleteSuggestion([FromBody]DeleteSuggestionRequest Suggestion)
        {
            var command = new DeleteSuggestionCommand(Suggestion);

            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("/api/Suggestions")]
        public async Task<IActionResult> GetSuggestions()
        {
            return Ok(await Mediator.Send(new GetSuggestionsQuery()));
        }
        [Authorize]
        [HttpGet("/api/Suggestion/details/{id}")]
        public async Task<IActionResult> GetSuggestionDetails(int id)
        {
            var query = new GetSuggestionDetailsQuery(new IdRequest(id));

            return Ok(await Mediator.Send(query));
        }
    }
}
