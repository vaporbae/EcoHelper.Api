namespace EcoHelper.Test.Answers
{
    using EcoHelper.Application.Answers.Commands.DeleteAnswer;
    using EcoHelper.Application.DTO.Answer.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

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
            var requestData = new DeleteAnswerRequest { Id = 10 };
            var command = new DeleteAnswerCommand(requestData);


            var Answer = await _uow.AnswersRepository.GetByIdAsync(10);
            Answer.ShouldNotBeNull();

            var commandHandler = new DeleteAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedAnswer = await _uow.AnswersRepository.GetByIdAsync(10);

            deletedAnswer.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteAnswerWithNotExistingIdShouldNotDeleteAnswerFromDbContext()
        {
            var requestData = new DeleteAnswerRequest { Id = 2372178 };
            var command = new DeleteAnswerCommand(requestData);

            var commandHandler = new DeleteAnswerCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
