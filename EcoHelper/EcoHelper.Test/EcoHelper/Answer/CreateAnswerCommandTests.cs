namespace EcoHelper.Test.Answers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Answer.Commands.CreateAnswer;
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateAnswerCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateAnswerCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateAnswerShouldAddAnswerToDbContext()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = false,
                QuestionId = 1
            };
            var command = new CreateAnswerCommand(requestData);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var Answer = await _uow.AnswersRepository.FirstOrDefaultAsync(x => x.AnswerText.Equals(command.Data.AnswerText)&&x.QuestionId.Equals(command.Data.QuestionId));

            Answer.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateAnswerShouldThrowExceptionAfterProvidingNotExistingQuestionId()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = false,
                QuestionId = 1048329448
            };
            var command = new CreateAnswerCommand(requestData);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateAnswerShouldThrowExceptionAfterProvidingEmptyAnswerName()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "",
                IsCorrect = false,
                QuestionId = 1
            };
            var command = new CreateAnswerCommand(requestData);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateAnswerShouldThrowExceptionAfterProvidingNextCorrectAnswerInTheSameQuestion()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = true,
                QuestionId = 2
            };
            var command = new CreateAnswerCommand(requestData);

            var requestData2 = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 234",
                IsCorrect = true,
                QuestionId = 2
            };
            var command2 = new CreateAnswerCommand(requestData2);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }

        [Fact]
        public async Task CreateAnswerShouldThrowExceptionAfterProvidingNextTheSameAnswerNameInTheSameQuestion()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = true,
                QuestionId = 3
            };
            var command = new CreateAnswerCommand(requestData);

            var requestData2 = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = true,
                QuestionId = 3
            };
            var command2 = new CreateAnswerCommand(requestData2);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }

        [Fact]
        public async Task CreateAnswerShouldThrowExceptionAfterProvidingFifthAnswerInTheSameQuestion()
        {
            var requestData = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = true,
                QuestionId = 4
            };
            var command = new CreateAnswerCommand(requestData);

            var requestData2 = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 123",
                IsCorrect = false,
                QuestionId = 4
            };
            var command2 = new CreateAnswerCommand(requestData2);

            var requestData3 = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 1234",
                IsCorrect = false,
                QuestionId = 4
            };
            var command3 = new CreateAnswerCommand(requestData3);

            var requestData4 = new CreateAnswerRequest
            {
                AnswerText = "Dzien Dobry Test 12345",
                IsCorrect = false,
                QuestionId = 4
            };
            var command4 = new CreateAnswerCommand(requestData4);

            var commandHandler = new CreateAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None);
            await commandHandler.Handle(command3, CancellationToken.None);

            await commandHandler.Handle(command4, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }
    }
}
