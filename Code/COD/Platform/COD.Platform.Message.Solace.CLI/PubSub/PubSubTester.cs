using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using System;

namespace COD.Platform.Messaging.CLITester.PubSub
{
    public class PubSubTester
    {
        private ILoggingService logging;
        private ILog log;
        private IConfiguration config;

        public PubSubTester(COD.Platform.Logging.Core.ILoggingService logging, IConfiguration config)
        {
            this.logging = logging;
            this.log = logging.GetCurrentClassLogger();
            this.config = config;
        }

        public void RunTest(IMessagingService msgService, PubSubOptions options)
        {

            var subscription = msgService.SubscribeToTopic<TestMessage>(options.TopicName);
            subscription.MessageReceived += this.Subscription_MessageReceived;

            int msgNumber = 0;
            log.Info($"Sending message number {++msgNumber}");
            msgService.SendMessageToTopic(options.TopicName, new TestMessage { TestInt = msgNumber, TestString = "hello" }, Guid.NewGuid().ToString());
            log.Info($"Sending message number {++msgNumber}");
            msgService.SendMessageToTopic(options.TopicName, new TestMessage { TestInt = msgNumber, TestString = "hello" }, Guid.NewGuid().ToString());

        }

        private void Subscription_MessageReceived(object sender, MessagedReceivedArguments<TestMessage> args)
        {
            log.Info($"Test Message Recieved {args.Message.Content.TestInt}");
            args.Message.Acknowledge();
        }
    }
}