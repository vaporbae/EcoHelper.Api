namespace EcoHelper.Test.Dumpsters
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Dumpster.Queries;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.DTO.InterestingFact.Queries;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsterDetails;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetDumpsterDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetDumpsterDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetDumpsterDetail()
        {
            var sut = new GetDumpsterDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetDumpsterDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetDumpsterDetailResponse>();
            result.Id.ShouldBe(11);
            result.InterestingFacts.ShouldBeOfType<List<GetInterestingFactDetailResponse>>();
            result.Garbages.ShouldBeOfType<List<GetGarbageDetailResponse>>();
        }
        [Fact]
        public async Task GetDumpsterDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetDumpsterDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetDumpsterDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
