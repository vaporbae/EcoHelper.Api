namespace EcoHelper.Test.Questions
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using EcoHelper.Application.Question.Queries.GetQuestionDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.DTO.Question.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Test.Infrastructure;
    using Xunit;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Domain.Entities;

    [Collection("TestCollection")]
    public class GetQuestionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;

        public GetQuestionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
        }

        [Fact]
        public async Task GetQuestionDetail()
        {
            var sut = new GetQuestionDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetQuestionDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetQuestionDetailResponse>();
            result.Id.ShouldBe(11);
            result.Answers.ShouldBeOfType<List<Answer>>();
        }
        [Fact]
        public async Task GetQuestionDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetQuestionDetailsQuery.Handler(_uow);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetQuestionDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
