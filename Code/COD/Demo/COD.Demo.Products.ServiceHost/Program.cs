using COD.Demo.Products.Service;
using COD.Platform.Configuration.Basic;
using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Logging.MockLogging;
using COD.Platform.ServiceHosting;
using System;

namespace COD.Demo.Products.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggingService logging = CreateLoggingService();
            IConfiguration config = CreateConfigurationService(logging, args);


            var  productService = new ProductService();

            var serviceHost = new MultiServiceServiceHost(logging, productService);

            serviceHost.RunService();

        }

        private static IConfiguration CreateConfigurationService(ILoggingService logging, string[] args)
        {
            IConfiguration config = new LayeredConfiguration(new AppSettingsConfiguration(logging), new CommandLineConfig(args));
            return config;
        }

        private static ILoggingService CreateLoggingService()
        {
            ILoggingService service = new MockLoggingService();
            return service;
        }
    }
}
