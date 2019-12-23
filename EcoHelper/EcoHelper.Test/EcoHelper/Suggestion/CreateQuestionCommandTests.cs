namespace EcoHelper.Test.Suggestions
{
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Suggestion.Commands.CreateSuggestion;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

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

            await commandHandler.Handle(command, CancellationToken.None);

            var Suggestion = await _uow.SuggestionsRepository.GetFirstAsync(x => x.Dumpster.Equals(requestData.Dumpster) && x.Garbage.Equals(requestData.Garbage));

            Suggestion.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateSuggestionShouldThrowExceptionAfterProvidingEmptyDumpsterName()
        {
            var requestData = new CreateSuggestionRequest
            {
                Dumpster = "",
                Garbage = "Garbage Test 123"
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
