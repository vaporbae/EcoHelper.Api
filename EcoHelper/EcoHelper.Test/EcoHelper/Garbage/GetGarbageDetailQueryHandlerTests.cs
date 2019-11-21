namespace EcoHelper.Test.Garbages
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Garbage.Queries.GetGarbageDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;

    [Collection("TestCollection")]
    public class GetGarbageDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;

        public GetGarbageDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task GetGarbageDetail()
        {
            var sut = new GetGarbageDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetGarbageDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetGarbageDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetGarbageDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetGarbageDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetGarbageDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
