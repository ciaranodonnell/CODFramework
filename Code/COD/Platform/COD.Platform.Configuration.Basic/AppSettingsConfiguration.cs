using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public class AppSettingsConfiguration : BaseConfiguration
    {
        private IConfigurationRoot configuration;

        public AppSettingsConfiguration(Logging.Core.ILoggingService logFactory) : base(logFactory)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(System.Environment.CurrentDirectory)
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  ;
            this.configuration = builder.Build();
        }
        public override string GetString(string name, string defaultValue = null)
        {
            return this.configuration[name] ?? defaultValue;
        }
    }
}
