using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.Core
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }
}
