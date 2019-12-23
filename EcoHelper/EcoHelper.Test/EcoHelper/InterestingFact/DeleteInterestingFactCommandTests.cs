﻿namespace EcoHelper.Test.InterestingFacts
{
    using EcoHelper.Application.DTO.InterestingFact.Commands;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.InterestingFacts.Commands.DeleteInterestingFact;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class DeleteInterestingFactCommandTests
    {
        private readonly IUnitOfWork _uow;

        public DeleteInterestingFactCommandTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task DeleteInterestingFactShouldDeleteInterestingFactFromDbContext()
        {
            var requestData = new DeleteInterestingFactRequest { Id = 10 };
            var command = new DeleteInterestingFactCommand(requestData);


            var InterestingFact = await _uow.InterestingFactsRepository.GetByIdAsync(10);
            InterestingFact.ShouldNotBeNull();

            var commandHandler = new DeleteInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedInterestingFact = await _uow.InterestingFactsRepository.GetByIdAsync(10);

            deletedInterestingFact.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteInterestingFactWithNotExistingIdShouldNotDeleteInterestingFactFromDbContext()
        {
            var requestData = new DeleteInterestingFactRequest { Id = 24322342 };
            var command = new DeleteInterestingFactCommand(requestData);

            var commandHandler = new DeleteInterestingFactCommand.Handler(_uow);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

    }
}
