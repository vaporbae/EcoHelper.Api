namespace EcoHelper.Test.BaseVersions
{
    using EcoHelper.Application.BaseVersion.Commands.CreateBaseVersion;
    using EcoHelper.Application.DTO.BaseVersion.Commands;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class CreateBaseVersionCommandTests
    {
        private readonly IUnitOfWork _uow;

        public CreateBaseVersionCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task CreateBaseVersionShouldAddBaseVersionToDbContext()
        {
            var requestData = new CreateBaseVersionRequest
            {
                Ver = 1.04
            };
            var command = new CreateBaseVersionCommand(requestData);

            var commandHandler = new CreateBaseVersionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var BaseVersion = await _uow.BaseVersionsRepository.FirstOrDefaultAsync(x => x.Ver.Equals(command.Data.Ver));

            BaseVersion.ShouldNotBeNull();
        }

        [Fact]
        public async Task CreateBaseVersionShouldThrowExceptionAfterProvidingEmptyVer()
        {
            var requestData = new CreateBaseVersionRequest
            {
                Ver = 0
            };
            var command = new CreateBaseVersionCommand(requestData);

            var commandHandler = new CreateBaseVersionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task CreateBaseVersionShouldThrowExceptionAfterProvidingNextSmallerVersion()
        {
            var requestData = new CreateBaseVersionRequest
            {
                Ver = 2.0
            };
            var command = new CreateBaseVersionCommand(requestData);

            var requestData2 = new CreateBaseVersionRequest
            {
                Ver = 1.02
            };
            var command2 = new CreateBaseVersionCommand(requestData2);

            var commandHandler = new CreateBaseVersionCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);
            await commandHandler.Handle(command2, CancellationToken.None).ShouldThrowAsync<FluentValidation.ValidationException>();

        }
    }
}

