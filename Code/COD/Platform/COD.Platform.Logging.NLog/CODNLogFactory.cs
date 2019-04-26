using COD.Platform.Logging;
using COD.Platform.Logging.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace COD.Platform.Logging.NLog
{
    public class CODNLogLoggingService : ILoggingService
    {
        private LogFactory nlogFactory;

        public CODNLogLoggingService(global::NLog.Config.LoggingConfiguration nlogConfig = null)
        {
            this.nlogFactory = new global::NLog.LogFactory(nlogConfig);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ILog GetCurrentClassLogger()
        {
            return GetLogger(Helpers.LookupHostClassNameFromStackFrame(new System.Diagnostics.StackFrame(1)));
        }

        public ILog GetCurrentClassLogger(Type loggerType)
        {
            return new CODNLogLogger(this.nlogFactory.GetCurrentClassLogger(loggerType));
        }

        public ILog GetLogger(string name)
        {
            return new CODNLogLogger(this.nlogFactory.GetLogger(name));
        }

        public ILog GetLogger(string name, Type loggerType)
        {
            return new CODNLogLogger(nlogFactory.GetLogger(name, loggerType));
        }

        public void SetMinimumLoggingLevel(COD.Platform.Logging.Core.LogLevel newMinimumLogLevel)
        {
            global::NLog.LogLevel newLevel = global::NLog.LogLevel.Off;
            switch (newMinimumLogLevel)
            {
                case COD.Platform.Logging.Core.LogLevel.Debug:
                    newLevel = global::NLog.LogLevel.Debug;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Trace:
                    newLevel = global::NLog.LogLevel.Trace;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Warn:
                    newLevel = global::NLog.LogLevel.Warn;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Info:
                    newLevel = global::NLog.LogLevel.Info;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Fatal:
                    newLevel = global::NLog.LogLevel.Fatal;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Error:
                    newLevel = global::NLog.LogLevel.Error;
                    break;
                case COD.Platform.Logging.Core.LogLevel.Off:
                    newLevel = global::NLog.LogLevel.Off;
                    break;
            }

            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.DisableLoggingForLevel(newLevel);
            }

            LogManager.ReconfigExistingLoggers();
        }
    }
}
