using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.CLITester
{
    public class ConnectionOptions
    {

        [Option('h', "host", Required = true, HelpText = "The Address of the Message Broker")]
        public string Host { get; set; }


        [Option('u', "username", Required = true, HelpText = "The username for the Message Broker")]
        public string UserName { get; set; }


        [Option('p', "password", Required = true, HelpText = "The Password for the Message Broker")]
        public string Password { get; set; }


        [Option('b', "broker", Required = true, HelpText = "Which Message Broker? solace, mock, azuresb")]
        public string Broker { get; set; } = "solace";


        [Option('v', "VPN", Required = true, HelpText = "VPN value needed for some brokers")]
        public string VPN { get; set; }



    }
}
