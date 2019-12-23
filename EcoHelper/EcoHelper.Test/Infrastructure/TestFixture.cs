namespace EcoHelper.Test.Infrastructure
{
    using AutoMapper;
    using EcoHelper.Application.DTO.Authentication;
    using EcoHelper.Application.Interfaces.UoW;
    using EcoHelper.Infrastructure.UoW;
    using EcoHelper.Persistence;
    using Microsoft.Extensions.Options;
    using System;
    using Xunit;

    public class TestFixture : IDisposable
    {
        public EcoHelperDbContext Context { get; private set; }
        public IUnitOfWork UoW { get; private set; }
        public IMapper Mapper { get; private set; }
        public IOptions<JwtSettings> JwtSettings { get; private set; }

        public TestFixture()
        {
            Context = EcoHelperContextFactory.Create();
            UoW = new UnitOfWork(Context);
            Mapper = AutoMapperFactory.Create();
            JwtSettings = JwtSettingFactory.Create();
        }

        public void Dispose()
        {
            EcoHelperContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("TestCollection")]
    public class QueryCollection : ICollectionFixture<TestFixture>
    {

    }

    [CollectionDefinition("FriendsTestCollection")]
    public class FriendsTestCollection : ICollectionFixture<TestFixture>
    {

    }
}
