﻿namespace EcoHelper.Test.Garbages
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Common;
    using EcoHelper.Application.DTO.Garbage.Queries;
    using EcoHelper.Application.Exceptions;
    using EcoHelper.Application.Garbage.Queries.GetGarbageDetails;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Test.Infrastructure;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("TestCollection")]
    public class GetGarbageDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetGarbageDetailQueryHandlerTests(TestFixture fixture)
        {
            _uow = fixture.UoW;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetGarbageDetail()
        {
            var sut = new GetGarbageDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(11);
            var result = await sut.Handle(new GetGarbageDetailsQuery(requestData), CancellationToken.None);

            result.ShouldBeOfType<GetGarbageDetailResponse>();
            result.Id.ShouldBe(11);
        }
        [Fact]
        public async Task GetGarbageDetailForNotExistingIdShouldThrowException()
        {
            var sut = new GetGarbageDetailsQuery.Handler(_uow, _mapper);

            var requestData = new IdRequest(2389493);
            var result = await sut.Handle(new GetGarbageDetailsQuery(requestData), CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
    }
}
