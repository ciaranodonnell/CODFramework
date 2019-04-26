using COD.Platform.Configuration.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public class EnvironmentConfiguration : BaseConfiguration
    {

        public EnvironmentConfiguration(Logging.Core.ILoggingService logFactory) : base(logFactory)
        { }

        public override string GetString(string name, string defaultValue = null)
        {
            return System.Environment.GetEnvironmentVariable(name) ?? defaultValue;
        }
    }
}
