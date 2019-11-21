namespace EcoHelper.Test.Infrastructure
{
    using System;
    using EcoHelper.Persistence;

    public class CommandTestBase : IDisposable
    {
        protected readonly EcoHelperDbContext _context;

        public CommandTestBase()
        {
            _context = EcoHelperContextFactory.Create();
        }

        public void Dispose()
        {
            EcoHelperContextFactory.Destroy(_context);
        }
    }
}
