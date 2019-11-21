namespace EcoHelper.Test.Dumpsters
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using EcoHelper.Application.Dumpster.Queries.GetDumpsters;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Dumpster.Queries;
    using EcoHelper.Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class GetDumpstersListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetDumpstersListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetDumpstersTest()
        {
            var sut = new GetDumpstersQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetDumpstersQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetDumpsterListResponse>();
        }
    }
}
