namespace EcoHelper.Test.Suggestions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Suggestion.Commands.CreateSuggestion;
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using System.Linq;

    [Collection("TestCollection")]
    public class CreateSuggestionCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateSuggestionCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateSuggestionShouldAddSuggestionToDbContext()
        {
            var requestData = new CreateSuggestionRequest
            {
                Dumpster = "Dumpster Test 123",
                Garbage = "Garbage Test 123"
            };
            var command = new CreateSuggestionCommand(requestData);

            var commandHandler = new CreateSuggestionCommand.Handler(_uow);

            var Suggestions = await _uow.SuggestionsRepository.GetAllAsync();

            var s = Suggestions.Last();

            s.Dumpster.ShouldBe(requestData.Dumpster);
            s.Garbage.ShouldBe(requestData.Garbage);
        }

        [Fact]
        public async Task CreateSuggestionShouldThrowExceptionAfterProvidingEmptyDumpsterName()
        {
            var requestData = new CreateSuggestionRequest
            {
                Dumpster = "",
                Garbage = "Dumpster Test 123"
            };
            var command = new CreateSuggestionCommand(requestData);

            var commandHandler = new CreateSuggestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateSuggestionShouldThrowExceptionAfterProvidingEmptyGarbageName()
        {
            var requestData = new CreateSuggestionRequest
            {
                Dumpster = "Dumpster Test 123",
                Garbage = ""
            };
            var command = new CreateSuggestionCommand(requestData);

            var commandHandler = new CreateSuggestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

    }
}
