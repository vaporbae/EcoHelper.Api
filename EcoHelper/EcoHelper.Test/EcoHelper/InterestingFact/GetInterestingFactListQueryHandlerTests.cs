namespace EcoHelper.Test.InterestingFacts
{
    using AutoMapper;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.InterestingFact.Queries.GetInterestingFacts;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetInterestingFactsListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetInterestingFactsListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetInterestingFactsTest()
        {
            var sut = new GetInterestingFactsQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetInterestingFactsQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetInterestingFactListResponse>();
        }
    }
}
