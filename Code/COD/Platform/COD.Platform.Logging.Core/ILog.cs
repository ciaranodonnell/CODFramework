using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Logging.Core
{
    public interface ILog
    {


        /// <summary>
        ///  Gets a value indicating whether logging is enabled for the Fatal level.
        /// </summary>
        bool IsFatalEnabled { get; }


        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Warn level.
        /// </summary>
        bool IsWarnEnabled { get; }


        /// <summary>
        ///  Gets a value indicating whether logging is enabled for the Info level.
        /// </summary>
        bool IsInfoEnabled { get; }


        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Error level.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Debug level.
        /// </summary>
        bool IsDebugEnabled { get; }


        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Error level.
        /// </summary>
        bool IsTraceEnabled { get; }


        ///<summary>
        /// Writes the diagnostic message at the Debug level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Debug level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Debug level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(string message);



        ///<summary>
        /// Writes the diagnostic message at the Debug level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Debug level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Debug level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(Exception exception, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Debug level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Debug(Exception exception, [Localizable(false)] string message);

        ///<summary>
        /// Writes the diagnostic message at the Info level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Info level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Info level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(string message);



        ///<summary>
        /// Writes the diagnostic message at the Info level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Info level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Info level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(Exception exception,  [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Info level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Info(Exception exception, [Localizable(false)] string message);


        ///<summary>
        /// Writes the diagnostic message at the Error level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Error level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Error level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(string message);



        ///<summary>
        /// Writes the diagnostic message at the Error level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Error level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Error level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Error level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Error(Exception exception, [Localizable(false)] string message);


        ///<summary>
        /// Writes the diagnostic message at the Trace level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Trace level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Trace level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(string message);



        ///<summary>
        /// Writes the diagnostic message at the Trace level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Trace level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Trace level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(Exception exception, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Trace level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Trace(Exception exception, [Localizable(false)] string message);



        ///<summary>
        /// Writes the diagnostic message at the Warn level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Warn level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Warn level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(string message);



        ///<summary>
        /// Writes the diagnostic message at the Warn level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Warn level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Warn level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(Exception exception, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Warn level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Warn(Exception exception, [Localizable(false)] string message);


        ///<summary>
        /// Writes the diagnostic message at the Fatal level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(IFormatProvider formatProvider, string message, params object[] argument);


        ///<summary>
        /// Writes the diagnostic message at the Fatal level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(string message, params object[] argument);



        ///<summary>
        /// Writes the diagnostic message at the Fatal level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(string message);



        ///<summary>
        /// Writes the diagnostic message at the Fatal level using the specified value as
        /// a parameter and formatting it with the standard format provider.
        /// </summary>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(Func<string> messageGeneratingFunction);


        ///<summary>
        /// Writes the diagnostic message and exception at the Fatal level using the specified value as
        /// a parameter and formatting it with the supplied format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Fatal level using the specified values as
        /// parameters and formatting then with the default format provider.
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="formatProvider">A format provider to help format the message</param>
        /// <param name="message">the log message</param>
        /// <param name="arguments">The arguments to be formatted in the message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);


        ///<summary>
        /// Writes the diagnostic message and exception at the Fatal level
        /// </summary>
        /// <param name="exception">An Exception to log with the message</param>
        /// <param name="message">the log message</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Fatal(Exception exception, [Localizable(false)] string message);
    }
}
