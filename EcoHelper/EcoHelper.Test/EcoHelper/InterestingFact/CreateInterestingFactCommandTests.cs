namespace EcoHelper.Test.InterestingFacts
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.InterestingFact.Commands.CreateInterestingFact;
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateInterestingFactCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateInterestingFactCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateInterestingFactShouldAddInterestingFactToDbContext()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 123",
                Description = "Dzien Dobry 123",
                DumpsterId = 1
            };
            var command = new CreateInterestingFactCommand(requestData);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var InterestingFact = await _uow.InterestingFactsRepository.FirstOrDefaultAsync(x => x.Title.Equals(command.Data.Title) && x.DumpsterId.Equals(command.Data.DumpsterId));

            InterestingFact.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateInterestingFactWithEmptyDumpsterIdShouldAddInterestingFactToDbContext()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 1234",
                Description = "Dzien Dobry 1234"
            };
            var command = new CreateInterestingFactCommand(requestData);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var InterestingFact = await _uow.InterestingFactsRepository.FirstOrDefaultAsync(x => x.Title.Equals(command.Data.Title) && x.Description.Equals(command.Data.Description));

            InterestingFact.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateInterestingFactShouldThrowExceptionAfterProvidingNotExistingDumpsterId()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 123",
                Description = "Dzien Dobry 123",
                DumpsterId = 13489729
            };
            var command = new CreateInterestingFactCommand(requestData);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateInterestingFactShouldThrowExceptionAfterProvidingEmptyTitle()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "",
                Description = "Dzien Dobry 123",
                DumpsterId = 1
            };
            var command = new CreateInterestingFactCommand(requestData);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateInterestingFactShouldThrowExceptionAfterProvidingEmptyDescription()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 123",
                Description = "",
                DumpsterId = 1
            };
            var command = new CreateInterestingFactCommand(requestData);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateInterestingFactShouldThrowExceptionAfterProvidingNextTheSameInterestingFactNameInTheSameDumpster()
        {
            var requestData = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 123",
                Description = "Dzien Dobry 123",
                DumpsterId = 3
            };
            var command = new CreateInterestingFactCommand(requestData);

            var requestData2 = new CreateInterestingFactRequest
            {
                Title = "Dzien Dobry 123",
                Description = "Dzien Dobry 123",
                DumpsterId = 3
            };
            var command2 = new CreateInterestingFactCommand(requestData2);

            var commandHandler = new CreateInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }
    }
}
