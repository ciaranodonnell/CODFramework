using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Logging.NLog;
using Logzio.DotNet.NLog;
using System;
using System.Linq;
using NLogProper = global::NLog;

namespace COD.Platform.Logging.CODLogzio
{
    public class LogzioThroughNLogLoggingService : ILoggingService
    {

        CODNLogLoggingService logger;

        public LogzioThroughNLogLoggingService(IConfiguration config, NLogProper.Config.LoggingConfiguration nlogConfig = null)
        {
            if (nlogConfig.AllTargets.FirstOrDefault((t) => (t.GetType() == typeof(Logzio.DotNet.NLog.LogzioTarget))) == null)
            {
                nlogConfig.AddTarget("Logzio", new LogzioTarget { Token = config.GetStringOrError("LOGGING_LOGZIO_TOKEN") });

                NLogProper.LogLevel minLevel, maxLevel;
                string value;
                minLevel = (value = config.GetString("LOGGING_LOGZIO_MINLEVEL")) == null ? NLogProper.LogLevel.Error : NLogProper.LogLevel.FromString(value);
                maxLevel = (value = config.GetString("LOGGING_LOGZIO_MAXLEVEL")) == null ? NLogProper.LogLevel.Fatal : NLogProper.LogLevel.FromString(value);


                nlogConfig.AddRule(minLevel, maxLevel, "Logzio");
            }

            this.logger = new CODNLogLoggingService(nlogConfig);
        }

        public ILog GetLogger(string name)
        {
            return logger.GetLogger(name);
        }

        public ILog GetCurrentClassLogger()
        {
            return GetLogger(Helpers.LookupHostClassNameFromStackFrame(new System.Diagnostics.StackFrame(1)));
        }

        public ILog GetCurrentClassLogger(Type loggerType)
        {
            return logger.GetCurrentClassLogger(loggerType);
        }

        public ILog GetLogger(string name, Type loggerType)
        {
            return logger.GetLogger(name, loggerType);
        }

        public void SetMinimumLoggingLevel(LogLevel newMinimumLogLevel)
        {
            logger.SetMinimumLoggingLevel(newMinimumLogLevel);
        }
    }
}
