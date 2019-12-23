namespace EcoHelper.Test.Questions
{
    using EcoHelper.Application.DTO.Question.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Question.Commands.CreateQuestion;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateQuestionCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateQuestionCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateQuestionShouldAddQuestionToDbContext()
        {
            var requestData = new CreateQuestionRequest
            {
                QuestionText = "Dzien Dobry Test 123"
            };
            var command = new CreateQuestionCommand(requestData);

            var commandHandler = new CreateQuestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var Question = await _uow.QuestionsRepository.FirstOrDefaultAsync(x => x.QuestionText.Equals(command.Data.QuestionText));

            Question.ShouldNotBeNull();
        }


        [Fact]
        public async Task CreateQuestionShouldThrowExceptionAfterProvidingEmptyQuestionName()
        {
            var requestData = new CreateQuestionRequest
            {
                QuestionText = ""
            };
            var command = new CreateQuestionCommand(requestData);

            var commandHandler = new CreateQuestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }


        [Fact]
        public async Task CreateQuestionShouldThrowExceptionAfterProvidingNextTheSameQuestionName()
        {
            var requestData = new CreateQuestionRequest
            {
                QuestionText = "Dzien Dobry Test 1234"
            };
            var command = new CreateQuestionCommand(requestData);

            var requestData2 = new CreateQuestionRequest
            {
                QuestionText = "Dzien Dobry Test 1234"
            };
            var command2 = new CreateQuestionCommand(requestData2);

            var commandHandler = new CreateQuestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }
    }
}
