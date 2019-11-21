namespace EcoHelper.Test.Garbages
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Garbages.Commands.DeleteGarbage;
    using EcoHelper.Application.DTO.Garbage.Commands;

    [Collection("TestCollection")]
    public class DeleteGarbageCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteGarbageCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteGarbageShouldDeleteGarbageFromDbContext()
        {
            var requestData = new DeleteGarbageRequest { Id = 10 };
            var command = new DeleteGarbageCommand(requestData);


            var Garbage = await _uow.GarbagesRepository.GetByIdAsync(1);
            Garbage.ShouldNotBeNull();

            var commandHandler = new DeleteGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedGarbage = await _uow.GarbagesRepository.GetByIdAsync(1);

            deletedGarbage.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteGarbageWithNotExistingIdShouldNotDeleteGarbageFromDbContext()
        {
            var requestData = new DeleteGarbageRequest { Id = 24322342 };
            var command = new DeleteGarbageCommand(requestData);

            var commandHandler = new DeleteGarbageCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
