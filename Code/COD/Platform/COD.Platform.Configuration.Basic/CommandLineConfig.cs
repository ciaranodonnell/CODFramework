using System;
using System.Collections.Generic;
using System.Text;
using COD.Platform.Logging.Core;
using Microsoft.Extensions.Configuration;

namespace COD.Platform.Configuration.Basic
{
    public class CommandLineConfig : BaseConfiguration
    {
        private IConfigurationRoot config;

        public CommandLineConfig(ILoggingService logFactory, string[] args) : base(logFactory)
        {
            config = new ConfigurationBuilder().AddCommandLine(args).Build();
            

        }

        public override string GetString(string name, string defaultValue = null)
        {
            return config[name] ?? defaultValue;
        }
    }
}
