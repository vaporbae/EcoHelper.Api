namespace EcoHelper.Test.Infrastructure
{
    using EcoHelper.Application.DTO.Authentication;
    using EcoHelper.Common;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using System.IO;

    public static class JwtSettingFactory
    {
        public static IOptions<JwtSettings> Create()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            configuration.AddJsonFile(GlobalConfig.DEV_APPSETTINGS);

            var buildedConfiguration = configuration.AddEnvironmentVariables()
                                                    .Build();

            return Options.Create(buildedConfiguration.GetSection("JwtSettings").Get<JwtSettings>());
        }
    }
}
