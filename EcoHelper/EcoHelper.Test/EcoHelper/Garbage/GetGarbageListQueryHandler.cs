namespace EcoHelper.Test.Garbages
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.Garbage.Queries.GetGarbages;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetGarbagesListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetGarbagesListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetGarbagesTest()
        {
            var sut = new GetGarbagesQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetGarbagesQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetGarbageListResponse>();
        }
    }
}
