namespace EcoHelper.Test.BaseVersions
{
    using AutoMapper;
    using EcoHelper.Application.BaseVersion.Queries.GetAnswerDetails;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetBaseVersionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetBaseVersionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetBaseVersionDetail()
        {
            var sut = new GetBaseVersionDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetBaseVersionDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetBaseVersionDetailResponse>();
        }
    }
}
