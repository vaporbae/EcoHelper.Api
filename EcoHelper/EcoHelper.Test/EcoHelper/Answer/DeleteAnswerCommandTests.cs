namespace EcoHelper.Test.Answers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Answers.Commands.DeleteAnswer;

    [Collection("TestCollection")]
    public class DeleteAnswerCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteAnswerCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteAnswerShouldDeleteAnswerFromDbContext()
        {
            var requestData = new IdRequest(10);
            var command = new DeleteAnswerCommand(requestData);


            var Answer = await _uow.AnswersRepository.GetByIdAsync(1);
            Answer.ShouldNotBeNull();

            var commandHandler = new DeleteAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedAnswer = await _uow.AnswersRepository.GetByIdAsync(1);

            deletedAnswer.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteAnswerWithNotExistingIdShouldNotDeleteAnswerFromDbContext()
        {
            var requestData = new IdRequest(2812942);
            var command = new DeleteAnswerCommand(requestData);

            var commandHandler = new DeleteAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
