namespace EcoHelper.Test.Infrastructure
{
    using EcoHelper.Persistence;
    using System;

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
