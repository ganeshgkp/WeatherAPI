using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MyOrg.Common.Helper
{
    public class ConfigurationHelper
    {
#if DEBUG
        private const string DefaultEnvironmentName = "Development";
#else
        private const string DefaultEnvironmentName = "Production";
#endif

        static IConfigurationRoot _Configuration;
        private IConfigurationRoot Configuration;

        public ConfigurationHelper()
        {
            if (_Configuration == null)
            {
                var environmentName =
                    Environment.GetEnvironmentVariable("ASPNET_ENVIRONMENT") ??
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                    DefaultEnvironmentName;

                _Configuration = GetConfiguration(environmentName);
            }

            Configuration = _Configuration;
        }
        private IConfigurationRoot GetConfiguration(string environmentName)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }
        public string GetConfig(params string[] names)
        {
            if (names == null || names.Length < 1)
                return null;

            if (names.Length > 1)
            {
                IConfigurationSection configuration = Configuration.GetSection(names[0]);
                var idx = 1;
                while (idx < names.Length - 1)
                {
                    configuration = configuration.GetSection(names[idx]);
                    idx++;
                }

                return configuration[names[idx]];
            }

            return Configuration[names[0]];
        }
    }
}
