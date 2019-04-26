using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COD.Platform.ServiceHosting
{
    public class LongRunningServiceHost
    {
        private static EventWaitHandle handle;
        private ILoggingService logging;
        private ILog log;
        private IHostedService service;

        public LongRunningServiceHost(IHostedService service, ILoggingService logging)
        {
            this.logging = logging;
            this.log = logging.GetCurrentClassLogger();
            this.service = service;
        }

        public void RunService()
        {
            log.Trace(() => $"Going to run a service of type {service.GetType().FullName}");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;

            log.Trace(() => $"Registered Unhandled Exception Event");
            handle = new AutoResetEvent(false);

            Console.CancelKeyPress += Console_CancelKeyPress;


            service.ServiceFailed += Service_ServiceFailed;
            service.RunService();


            Console.WriteLine("Application Running. Use Cancel Keys (normally Ctrl+C) to close");

            log.Info("Service Started, about to block main thread");

            handle.WaitOne();
            log.Info("Main Thread Resumed to Shutdown");
            service.Shutdown();
            log.Info("Service Shutdown. Goodbye!");
        }

        private void Service_ServiceFailed()
        {
            log.Debug("Service Failed. Stopping Service Host");
            service.ServiceFailed -= Service_ServiceFailed; 
            Shutdown();
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            log.Warn($"AppDomain is being unloaded. ");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Fatal(e.ExceptionObject as Exception, $"Unhandled Exception caught in LongRunningServiceHost. Is Terminating = {e.IsTerminating}");
            Shutdown();
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            log.Debug("Cancel key pressed");
            Console.CancelKeyPress -= Console_CancelKeyPress;

            Shutdown();
        }

        public void Shutdown()
        {
            log.Debug($"Shutdown called. Handle = {handle}");
            if (handle != null) handle.Set();

        }


    }
}
