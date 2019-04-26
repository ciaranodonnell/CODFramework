using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public class LayeredConfiguration : BaseConfiguration
    {
        private IConfiguration[] configs;

        /// <summary>
        /// An IConfigurationImplementation that allows a more specific configuration to override less. 
        /// </summary>
        /// <param name="loggingService">A logging service</param>
        /// <param name="orderedConfigList">A list of configuration providers, in descending order of importance.</param>
        public LayeredConfiguration(ILoggingService loggingService, params IConfiguration[] orderedConfigList)
            : base(loggingService)
        {
            if (orderedConfigList == null) throw new ArgumentNullException(nameof(orderedConfigList));
            if (orderedConfigList.Length == 0) throw new ArgumentException(nameof(orderedConfigList) + " must have at least one entry");

            this.configs = orderedConfigList;
        }

        public override string GetString(string name, string defaultValue = null)
        {
            string value = null;
            foreach (var config in configs)
            {
                value = config.GetString(name, null);
                if (value != null) return value;
            }
            return defaultValue;
        }

    }
}
