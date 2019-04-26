using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Logging.Core
{
    public class LoggerLevelController : ILogLevelController
    {

        public LoggerLevelController() : this(null)
        {

        }

        public LoggerLevelController(ILogLevelController baseController)
        {
            this.baseController = baseController;
        }

        private ILogLevelController baseController;

        public bool? IsFatalEnabled { get; set; }

        public bool? IsWarnEnabled { get; set; }
        public bool? IsInfoEnabled { get; set; }
        public bool? IsErrorEnabled { get; set; }
        public bool? IsDebugEnabled { get; set; }
        public bool? IsTraceEnabled { get; set; }



        bool ILogLevelController.IsFatalEnabled
        {
            get { return this.IsFatalEnabled.GetValueOrDefault(baseController?.IsFatalEnabled ?? false); }
        }
        bool ILogLevelController.IsWarnEnabled
        {
            get { return this.IsWarnEnabled.GetValueOrDefault(baseController?.IsWarnEnabled ?? false); }
        }
        bool ILogLevelController.IsInfoEnabled
        {
            get { return this.IsInfoEnabled.GetValueOrDefault(baseController?.IsInfoEnabled ?? false); }
        }
        bool ILogLevelController.IsErrorEnabled
        {
            get { return this.IsErrorEnabled.GetValueOrDefault(baseController?.IsErrorEnabled ?? false); }
        }
        bool ILogLevelController.IsDebugEnabled
        {
            get { return this.IsDebugEnabled.GetValueOrDefault(baseController?.IsDebugEnabled ?? false); }
        }
        bool ILogLevelController.IsTraceEnabled
        {
            get { return this.IsTraceEnabled.GetValueOrDefault(baseController?.IsTraceEnabled ?? false); }
        }


    }
}