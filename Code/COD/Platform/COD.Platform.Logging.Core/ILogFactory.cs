using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace COD.Platform.Logging.Core
{
    public interface ILoggingService
    {
        ILog GetLogger(string name);


       ILog GetCurrentClassLogger();
       
       ILog GetCurrentClassLogger(Type loggerType);
       ILog GetLogger(string name, Type loggerType);


        void SetMinimumLoggingLevel(LogLevel newMinimumLogLevel);
    }
}
