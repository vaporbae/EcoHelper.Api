namespace EcoHelper.Test.Dumpsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Dumpster.Commands.CreateDumpster;
    using EcoHelper.Application.DTO.Dumpster.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateDumpsterCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateDumpsterCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateDumpsterShouldAddDumpsterToDbContext()
        {
            var requestData = new CreateDumpsterRequest
            {
                Name = "Dzien dobry 123"
            };
            var command = new CreateDumpsterCommand(requestData);

            var commandHandler = new CreateDumpsterCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var Dumpster = await _uow.DumpstersRepository.FirstOrDefaultAsync(x => x.Name.Equals(command.Data.Name));

            Dumpster.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateDumpsterShouldThrowExceptionAfterProvidingEmptyName()
        {
            var requestData = new CreateDumpsterRequest
            {
                Name = ""
            };
            var command = new CreateDumpsterCommand(requestData);

            var commandHandler = new CreateDumpsterCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateDumpsterShouldThrowExceptionAfterProvidingNextExistingDumpsterName()
        {
            var requestData = new CreateDumpsterRequest
            {
                Name = "Dzien Dobry Test 123"
            };
            var command = new CreateDumpsterCommand(requestData);

            var requestData2 = new CreateDumpsterRequest
            {
                Name = "Dzien Dobry Test 123"
            };
            var command2 = new CreateDumpsterCommand(requestData2);

            var commandHandler = new CreateDumpsterCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }
    }
}
