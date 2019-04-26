using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Logging
{
    public class LogFactoryLevelController : ILogLevelController
    {


        public bool IsFatalEnabled { get; set; }

        public bool IsWarnEnabled { get; set; }

        public bool IsInfoEnabled { get; set; }

        public bool IsErrorEnabled { get; set; }

        public bool IsDebugEnabled { get; set; }

        public bool IsTraceEnabled { get; set; }



    }
}
