namespace EcoHelper.Test.Answers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Application.Answer.Queries.GetAnswers;
    using Application.Interfaces.UoW;
    using Application.DTO.Answer.Queries;
    using Test.Infrastructure;
    using Xunit;

    [Collection("TestCollection")]
    public class GetAnswersListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAnswersListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAnswersTest()
        {
            var sut = new GetAnswersQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetAnswersQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetAnswerListResponse>();
        }
    }
}
