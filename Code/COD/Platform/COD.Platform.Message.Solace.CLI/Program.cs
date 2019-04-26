using CommandLine;
using COD.Platform.Messaging;
using COD.Platform.Messaging.Solace;
using System;
using System.Collections.Generic;
using COD.Platform.Logging.NLog;
using COD.Platform.Configuration.Basic;
using COD.Platform.Messaging.CLITester.PubSub;
using NLog.Config;
using System.IO;
using System.Reflection;
using COD.Platform.Messaging.Core.Serialization;
using COD.Platform.Configuration.Core;
using COD.Platform.Messaging.AzureServiceBus;

namespace COD.Platform.Messaging.CLITester
{
    class Program
    {
        private static CODNLogLoggingService logging;
        private static IConfiguration config;

        static void Main(string[] args)
        {

            Console.WriteLine("Starting Messaging CLI Tester");

            logging = new CODNLogLoggingService(GetNLogConfiguration());
            config = new LayeredConfiguration(logging, new CommandLineConfig(logging, args),
                new AppSettingsConfiguration(logging), new EnvironmentConfiguration(logging));


            Console.ReadLine();
        }

        private static LoggingConfiguration GetNLogConfiguration()
        {
            string path =
               Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "nlog.config");
            return new XmlLoggingConfiguration(path);
        }

        private static object HandleCommandLineError(IEnumerable<Error> error)
        {
            throw new NotImplementedException();
        }


        public static IMessagingService GenerateMessagingService(ConnectionOptions options)
        {
            switch (options.Broker)
            {
                case "solace":
                    var sms = new SolaceMessagingService(logging, config, new JSONSerializer(), "CLITester_" + System.Environment.MachineName);
                    //sms.Init(new ConnectionDetails(config) { ClientId = "CLITester", ThrowOnFailure = true });
                    return sms;

                case "azuresb":
                    return new AzureServiceBusMessagingService(config, logging, new JSONSerializer());


                case "rabbit":
                    return null;// new AzureMessagingService(logging, config);


            }

            throw new Configuration.Core.ConfigurationException("Invalid Broker Chosen");
        }

        private static object RunPubSubTest(PubSubOptions options)
        {
            PubSubTester tester = new PubSubTester(logging, config);
            tester.RunTest(GenerateMessagingService(options), options);

            return 0;
        }
    }
}
