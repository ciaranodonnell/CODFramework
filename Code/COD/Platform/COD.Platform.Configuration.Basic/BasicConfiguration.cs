using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace COD.Platform.Configuration.Basic
{
    public class BasicConfiguration : BaseConfiguration
    {


        [DebuggerStepThrough]
        public BasicConfiguration(ILoggingService logFactory) : base(logFactory)
        {
        }

        [DebuggerStepThrough]
        public BasicConfiguration() : base()
        {
        }

        Dictionary<string, string> values = new Dictionary<string, string>();
        
        [DebuggerStepThrough]
        public void AddConfigurationOption(string key, string value)
        {
            this.values.Add(key, value);
        }


        [DebuggerStepThrough]
        public override string GetString(string name, string defaultValue = null)
        {
            string value = null;
            if (values.TryGetValue(name, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }

        }

    }
}
