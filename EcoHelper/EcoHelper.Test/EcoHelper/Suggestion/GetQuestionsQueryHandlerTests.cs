namespace EcoHelper.Test.Suggestions
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using EcoHelper.Application.Suggestion.Queries.GetSuggestions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Suggestion.Queries;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class GetSuggestionsListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSuggestionsListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSuggestionsTest()
        {
            var sut = new GetSuggestionsQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetSuggestionsQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetSuggestionListResponse>();
        }
    }
}
