namespace EcoHelper.Test.Questions
{
    using EcoHelper.Application.DTO.Question.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Questions.Commands.DeleteQuestion;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class DeleteQuestionCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteQuestionCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteQuestionShouldDeleteQuestionFromDbContext()
        {
            var requestData = new DeleteQuestionRequest { Id = 10 };
            var command = new DeleteQuestionCommand(requestData);


            var Question = await _uow.QuestionsRepository.GetByIdAsync(10);
            Question.ShouldNotBeNull();

            var commandHandler = new DeleteQuestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedQuestion = await _uow.QuestionsRepository.GetByIdAsync(10);

            deletedQuestion.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteQuestionWithNotExistingIdShouldNotDeleteQuestionFromDbContext()
        {
            var requestData = new DeleteQuestionRequest { Id = 23847938 };
            var command = new DeleteQuestionCommand(requestData);

            var commandHandler = new DeleteQuestionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
