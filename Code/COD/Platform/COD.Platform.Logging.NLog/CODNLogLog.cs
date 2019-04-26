using COD.Platform.Logging.Core;
using System;
using System.ComponentModel;
using NLogProper = global::NLog;


namespace COD.Platform.Logging.NLog
{
    public class CODNLogLogger : ILog
    {

        NLogProper.Logger logger;

        internal CODNLogLogger(NLogProper.Logger logger)
        {
            this.logger = logger;
        }

        public bool IsFatalEnabled => logger.IsFatalEnabled;

        public bool IsWarnEnabled => logger.IsWarnEnabled;

        public bool IsInfoEnabled => logger.IsInfoEnabled;

        public bool IsErrorEnabled => logger.IsErrorEnabled;

        public bool IsDebugEnabled => logger.IsDebugEnabled;

        public bool IsTraceEnabled => logger.IsTraceEnabled;

        
        public void Debug(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, params object[] argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(Func<string> messageGeneratingFunction)
        {
            if (IsDebugEnabled)
            logger.Debug(messageGeneratingFunction());
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, formatProvider, message, args);
        }

        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, message, args);
        }

        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            logger.Debug(exception, message);
        }



        public void Trace(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, params object[] argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(Func<string> messageGeneratingFunction)
        {
            if (IsTraceEnabled)
                logger.Trace(messageGeneratingFunction());
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace(exception, formatProvider, message, args);
        }

        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace(exception, message, args);
        }

        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            logger.Trace(exception, message);
        }



        public void Warn(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, params object[] argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(Func<string> messageGeneratingFunction)
        {
            if (IsWarnEnabled)
                logger.Warn(messageGeneratingFunction());
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Warn(exception, formatProvider, message, args);
        }

        public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Warn(exception, message, args);
        }

        public void Warn(Exception exception, [Localizable(false)] string message)
        {
            logger.Warn(exception, message);
        }



        public void Error(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, params object[] argument)
        {
            logger.Error(message, argument);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Func<string> messageGeneratingFunction)
        {
            if (IsErrorEnabled)
                logger.Error(messageGeneratingFunction());
        }

        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, formatProvider, message, args);
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, message, args);
        }

        public void Error(Exception exception, [Localizable(false)] string message)
        {
            logger.Error(exception, message);
        }



        public void Fatal(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, params object[] argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(Func<string> messageGeneratingFunction)
        {
            if (IsFatalEnabled)
                logger.Fatal(messageGeneratingFunction());
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(exception, formatProvider, message, args);
        }

        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(exception, message, args);
        }

        public void Fatal(Exception exception, [Localizable(false)] string message)
        {
            logger.Fatal(exception, message);
        }


        public void Info(IFormatProvider formatProvider, string message, params object[] argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, params object[] argument)
        {
            logger.Info(message, argument);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(Func<string> messageGeneratingFunction)
        {
            if (IsInfoEnabled)
                logger.Info(messageGeneratingFunction());
        }

        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Info(exception, formatProvider, message, args);
        }

        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Info(exception, message, args);
        }

        public void Info(Exception exception, [Localizable(false)] string message)
        {
            logger.Info(exception, message);
        }

    }
}
