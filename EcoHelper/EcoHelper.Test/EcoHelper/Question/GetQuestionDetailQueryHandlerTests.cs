﻿namespace EcoHelper.Test.Questions
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Answer.Queries;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Question.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Application.Question.Queries.GetQuestionDetails;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetQuestionDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetQuestionDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetQuestionDetail()
        {
            var sut = new GetQuestionDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetQuestionDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetQuestionDetailResponse>();
            result.Id.ShouldBe(11);
            result.Answers.ShouldBeOfType<List<GetAnswerDetailResponse>>();
        }
        [Fact]
        public async Task GetQuestionDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetQuestionDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetQuestionDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
