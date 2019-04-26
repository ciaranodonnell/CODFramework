using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace COD.Platform.Logging.MockLogging
{
    public class MockLogger : ILog
    {
        private string loggerName;

        public MockLogger(string loggerName) { this.loggerName = loggerName; }

        public bool IsFatalEnabled { get; set; }

        public bool IsWarnEnabled { get; set; }

        public bool IsInfoEnabled { get; set; }

        public bool IsErrorEnabled { get; set; }

        public bool IsDebugEnabled { get; set; }

        public bool IsTraceEnabled { get; set; }
        public string LoggerName { get { return loggerName; } }

        private void DoLog(string statement, [CallerMemberName] string level = "")
        {
            System.Diagnostics.Debug.WriteLine($"{loggerName} - {level}: {statement}");
        }


        public void Trace(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Trace(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Trace(string message)
        {
            DoLog(message);
        }

        public void Trace(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }


        public void Info(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Info(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Info(string message)
        {
            DoLog(message);
        }

        public void Info(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Info(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }


        public void Error(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Error(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Error(string message)
        {
            DoLog(message);
        }

        public void Error(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Error(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Warn(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Warn(string message)
        {
            DoLog(message);
        }

        public void Warn(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Warn(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Fatal(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Fatal(string message)
        {
            DoLog(message);
        }

        public void Fatal(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Fatal(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }


        public void Debug(IFormatProvider formatProvider, string message, params object[] argument)
        {
            DoLog(string.Format(formatProvider, message, argument));
        }

        public void Debug(string message, params object[] argument)
        {
            DoLog(string.Format(message, argument));
        }

        public void Debug(string message)
        {
            DoLog(message);
        }

        public void Debug(Func<string> messageGeneratingFunction)
        {
            DoLog(messageGeneratingFunction());
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(formatProvider, message, args)}");
        }

        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            DoLog($"{exception.ToString()} = {string.Format(message, args)}");
        }

        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            DoLog($"{exception.ToString()} = {string.Format(message)}");
        }

    }
}
