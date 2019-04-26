using System;
using System.Threading;
using System.Threading.Tasks;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using Microsoft.Azure.ServiceBus;

namespace COD.Platform.Messaging.AzureServiceBus
{
    internal class AzureServiceBusTopicClient<TContent> : ISubscriptionClient<TContent>
    {
        private ILoggingService logging;
        private ILog log;
        private string asbConnectionString;
        private string topicName;
        private string subscriptionQueueName;
        private SubscriptionOptions options;
        private IMessageSerializer serializer;
        private TopicClient client;
        private SubscriptionClient subscriptionClient;

        public AzureServiceBusTopicClient(ILoggingService logging, string asbConnectionString, string topicName, string subscriptionQueueName, SubscriptionOptions options, IMessageSerializer serializer)
        {
            this.logging = logging;
            this.log = logging.GetCurrentClassLogger();
            this.asbConnectionString = asbConnectionString;
            this.topicName = topicName;
            this.subscriptionQueueName = subscriptionQueueName;
            this.options = options;
            this.serializer = serializer;
            ConnectToSubscription();

        }

        private void ConnectToSubscription()
        {
            this.subscriptionClient = new SubscriptionClient(asbConnectionString, topicName, subscriptionQueueName);


            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether the message pump should automatically complete the messages after returning from user callback.
                // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
                AutoComplete = false
            };

            // Register the function that processes messages.
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

        }
        int ProcessMessageNumber = 0;
        private Task ProcessMessagesAsync(Message arg1, CancellationToken arg2)
        {
            return Task.Run(() =>
            {
                var msg = AzureInboundMessage<TContent>.LoadFromBrokeredMessage(arg1, this.options.SubscriptionType, this.serializer, this.subscriptionClient);
                MessageReceived?.Invoke(this, new MessagedReceivedArguments<TContent> { Message = msg, ProcessMessageNumber = ++ProcessMessageNumber, ReceivedTimestamp = DateTimeOffset.UtcNow });
            });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.Run(() =>
            {
                log.Error("Error processing ASB Message", arg.Exception);
            });

        }

        public string Topic { get { return topicName; } }

        public event MessageReceivedDelegate<TContent> MessageReceived;
        public event MessageReceiveErrorDelegate<TContent> MessageReceiveError;

        public void Dispose()
        {

        }

        public void Unsubscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}