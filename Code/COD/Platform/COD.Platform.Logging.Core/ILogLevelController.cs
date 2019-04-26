using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Logging
{
    public interface ILogLevelController
    {





        bool IsFatalEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsTraceEnabled { get; }
    }
}
