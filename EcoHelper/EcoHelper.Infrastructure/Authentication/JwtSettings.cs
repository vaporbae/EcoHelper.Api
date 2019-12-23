using System;

namespace EcoHelper.Infrastructure.Authentication
{
    public sealed class JwtSettings
    {
        public string Key { get; set; }
        public TimeSpan Lease { get; set; }
    }
}