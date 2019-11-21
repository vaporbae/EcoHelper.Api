namespace EcoHelper.Test.Dumpsters
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Dumpster.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Domain.Entities;

    [Collection("TestCollection")]
    public class GetDumpsterDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;

        public GetDumpsterDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task GetDumpsterDetail()
        {
            var sut = new GetDumpsterDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetDumpsterDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetDumpsterDetailResponse>();
            result.Id.ShouldBe(11);
            result.InterestingFacts.ShouldBeOfType<List<InterestingFact>>();
            result.Garbages.ShouldBeOfType<List<Garbage>>();
        }
        [Fact]
        public async Task GetDumpsterDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetDumpsterDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetDumpsterDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
