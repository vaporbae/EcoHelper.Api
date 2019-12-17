namespace EcoHelper.Test.Suggestions
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Suggestion.Queries.GetSuggestionDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Domain.Entities;

    [Collection("TestCollection")]
    public class GetSuggestionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;

        public GetSuggestionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task GetSuggestionDetail()
        {
            var sut = new GetSuggestionDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetSuggestionDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetSuggestionDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetSuggestionDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetSuggestionDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetSuggestionDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
