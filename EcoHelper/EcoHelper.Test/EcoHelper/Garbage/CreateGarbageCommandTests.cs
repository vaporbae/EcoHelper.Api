namespace EcoHelper.Test.Garbages
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Garbage.Commands.CreateGarbage;
    using EcoHelper.Application.DTO.Garbage.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateGarbageCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateGarbageCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateGarbageShouldAddGarbageToDbContext()
        {
            var requestData = new CreateGarbageRequest
            {
                Name = "Dzien Dobry 123",
                DumpsterId = 13
            };
            var command = new CreateGarbageCommand(requestData);

            var commandHandler = new CreateGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var Garbage = await _uow.GarbagesRepository.FirstOrDefaultAsync(x => x.Name.Equals(command.Data.Name) && x.DumpsterId.Equals(command.Data.DumpsterId));

            Garbage.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateGarbageShouldThrowExceptionAfterProvidingNotExistingDumpsterId()
        {
            var requestData = new CreateGarbageRequest
            {
                Name = "Dzien Dobry 123",
                DumpsterId = 1439734
            };
            var command = new CreateGarbageCommand(requestData);

            var commandHandler = new CreateGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateGarbageShouldThrowExceptionAfterProvidingEmptyGarbageName()
        {
            var requestData = new CreateGarbageRequest
            {
                Name = "",
                DumpsterId = 1
            };
            var command = new CreateGarbageCommand(requestData);

            var commandHandler = new CreateGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateGarbageShouldThrowExceptionAfterProvidingNextSameNameInDumpsterGarbages()
        {
            var requestData = new CreateGarbageRequest
            {
                Name = "Dzien Dobry 123456789",
                DumpsterId = 13
            };
            var command = new CreateGarbageCommand(requestData);

            var requestData2 = new CreateGarbageRequest
            {
                Name = "Dzien Dobry 123456789",
                DumpsterId = 13
            };
            var command2 = new CreateGarbageCommand(requestData2);

            var commandHandler = new CreateGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
