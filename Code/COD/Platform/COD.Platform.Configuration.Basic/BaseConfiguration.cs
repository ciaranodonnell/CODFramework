using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace COD.Platform.Configuration.Basic
{
    public abstract class BaseConfiguration : IConfiguration
    {
        private ILog log;

        public BaseConfiguration(ILoggingService logFactory)
        {
            this.log = logFactory.GetCurrentClassLogger();
        }


        [DebuggerStepThrough]
        public bool GetBool(string name, bool defaultValue = false)
        {
            string value = GetString(name, null);
            if (value != null)
            {
                if (bool.TryParse(value, out bool result))
                {
                    return result;
                }
                else
                {
                    if (value == "Y" || value == "y" || value == "1") return true;
                    if (value == "n" || value == "N" || value == "0") return false;

                    string errorMessage = $"The configuration value is NOT convertible to a boolean. Name = \"{ name }\", value = \"{value}\"";

                    log.Debug(errorMessage);

                    //Need to decide if this should be default or exception
                    //Choosing default value now perhaps supports the "Convention over Configuration Pattern"
                    //however missing config is one thing, this is invalid config
                    //return defaultValue;


                    throw new ConfigurationException(errorMessage);
                }
            }
            else
            {
                return defaultValue;
            }
        }


        [DebuggerStepThrough]
        public int GetInt32(string name, int defaultValue = 0)
        {
            string value = GetString(name, null);
            if (value != null)
            {
                if (int.TryParse(value, out int result))
                {
                    return result;
                }
                else
                {
                    string errorMessage = $"The configuration value is NOT convertible to a int. Name = \"{ name }\", value = \"{value}\"";

                    log.Debug(errorMessage);

                    //Need to decide if this should be default or exception
                    //Choosing default value now perhaps supports the "Convention over Configuration Pattern"
                    //however missing config is one thing, this is invalid config
                    //return defaultValue;


                    throw new ConfigurationException(errorMessage);
                }
            }
            else
            {
                return defaultValue;
            }
        }

        public abstract string GetString(string name, string defaultValue = null);


        [DebuggerStepThrough]
        public string GetStringOrError(string name)
        {
            string value = GetString(name, null);
            if (value != null)
            {
                return value;
            }
            else
            {
                throw new ConfigurationException($"Configuration value {name} is missing");
            }

        }
    }
}
