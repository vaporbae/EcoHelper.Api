namespace EcoHelper.Test.Dumpsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Dumpsters.Commands.DeleteDumpster;

    [Collection("TestCollection")]
    public class DeleteDumpsterCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteDumpsterCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteDumpsterShouldDeleteDumpsterFromDbContext()
        {
            var requestData = new IdRequest(10);
            var command = new DeleteDumpsterCommand(requestData);


            var Dumpster = await _uow.DumpstersRepository.GetByIdAsync(1);
            Dumpster.ShouldNotBeNull();

            var commandHandler = new DeleteDumpsterCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedDumpster = await _uow.DumpstersRepository.GetByIdAsync(1);

            deletedDumpster.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteDumpsterWithNotExistingIdShouldNotDeleteDumpsterFromDbContext()
        {
            var requestData = new IdRequest(2812942);
            var command = new DeleteDumpsterCommand(requestData);

            var commandHandler = new DeleteDumpsterCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
