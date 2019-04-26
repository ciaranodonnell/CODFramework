using COD.Platform.Logging.Core;
using System;

namespace COD.Platform.Logging.MockLogging
{
    public class MockLoggingService : ILoggingService
    {
        public ILog GetCurrentClassLogger()
        {
            return new MockLogger(Helpers.LookupHostClassNameFromStackFrame(new System.Diagnostics.StackFrame(1)));
        }

        public ILog GetCurrentClassLogger(Type loggerType)
        {
            return new MockLogger(loggerType.Name);
        }

        public ILog GetLogger(string name)
        {
            return new MockLogger(name);
        }

        public ILog GetLogger(string name, Type loggerType)
        {
            return new MockLogger($"{name}-{loggerType.Name}");
        }

        public void SetMinimumLoggingLevel(LogLevel newMinimumLogLevel)
        {

        }
    }
}
