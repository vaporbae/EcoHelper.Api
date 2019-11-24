namespace EcoHelper.Test.Answers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Answer.Queries.GetAnswerDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;

    [Collection("TestCollection")]
    public class GetBaseVersionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;

        public GetBaseVersionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task GetAnswerDetail()
        {
            var sut = new GetAnswerDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetAnswerDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetAnswerDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetAnswerDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetAnswerDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetAnswerDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
