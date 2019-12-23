namespace EcoHelper.Test.Suggestions
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Suggestion.Queries.GetSuggestionDetails;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetSuggestionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSuggestionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSuggestionDetail()
        {
            var sut = new GetSuggestionDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetSuggestionDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetSuggestionDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetSuggestionDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetSuggestionDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetSuggestionDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
