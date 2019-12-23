namespace EcoHelper.Test.Suggestions
{
    using EcoHelper.Application.DTO.Suggestion.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Suggestions.Commands.DeleteSuggestion;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class DeleteSuggestionCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteSuggestionCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteSuggestionShouldDeleteSuggestionFromDbContext()
        {
            var requestData = new DeleteSuggestionRequest { Id = 10 };
            var command = new DeleteSuggestionCommand(requestData);


            var Suggestion = await _uow.SuggestionsRepository.GetByIdAsync(10);
            Suggestion.ShouldNotBeNull();

            var commandHandler = new DeleteSuggestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedSuggestion = await _uow.SuggestionsRepository.GetByIdAsync(10);

            deletedSuggestion.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteSuggestionWithNotExistingIdShouldNotDeleteSuggestionFromDbContext()
        {
            var requestData = new DeleteSuggestionRequest { Id = 23847938 };
            var command = new DeleteSuggestionCommand(requestData);

            var commandHandler = new DeleteSuggestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
