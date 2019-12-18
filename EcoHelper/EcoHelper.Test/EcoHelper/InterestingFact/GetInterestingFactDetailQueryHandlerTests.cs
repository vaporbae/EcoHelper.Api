namespace EcoHelper.Test.InterestingFacts
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.InterestingFact.Queries.GetInterestingFactDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;
    using AutoMapper;

    [Collection("TestCollection")]
    public class GetInterestingFactDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetInterestingFactDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetInterestingFactDetail()
        {
            var sut = new GetInterestingFactDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetInterestingFactDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetInterestingFactDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetInterestingFactDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetInterestingFactDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetInterestingFactDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
