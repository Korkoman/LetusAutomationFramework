using Microsoft.Extensions.Configuration;
using PlaywrightTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightTests.Utilities
{
     public static class ConfigReader
    {
        private static IConfigurationRoot _configuration;
        private static AppSettings _settings;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
            _settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
           
        }

        public static AppSettings Settings => _settings;
    }
}
