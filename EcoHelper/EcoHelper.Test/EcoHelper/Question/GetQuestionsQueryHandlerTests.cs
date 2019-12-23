namespace EcoHelper.Test.Questions
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Question.Queries;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Question.Queries.GetQuestions;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetQuestionsListQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetQuestionsListQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetQuestionsTest()
        {
            var sut = new GetQuestionsQuery.Handler(_uow, _mapper);

            var result = await sut.Handle(new GetQuestionsQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetQuestionListResponse>();
        }
    }
}
