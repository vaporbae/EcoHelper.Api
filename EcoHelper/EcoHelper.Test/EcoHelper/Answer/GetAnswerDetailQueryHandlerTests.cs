namespace EcoHelper.Test.Answers
{
    using AutoMapper;
    using EcoHelper.Application.Answer.Queries.GetAnswerDetails;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.Exceptions;
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
        public async Task GetAnswerDetail()
        {
            var sut = new GetAnswerDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetAnswerDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetAnswerDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetAnswerDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetAnswerDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetAnswerDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
