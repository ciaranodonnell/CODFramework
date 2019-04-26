using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.CLITester.PubSub
{
    [Verb("pubsub", HelpText = "Test subscribing to a topic and publishing messages to subscribe to it")]
    public class PubSubOptions : ConnectionOptions
    {

        [Option('t', "topic", HelpText = "The Topic to run a PubSub test on")]
        public string TopicName { get; set; }


    }
}
