using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.AzureServiceBus
{
    public class AzureServiceBusMessagingService : IMessagingService
    {


        private readonly string asbConnectionString = null;
        private IConfiguration config;
        private ILoggingService logging;
        private IMessageSerializer serializer;

        public AzureServiceBusMessagingService(IConfiguration config, ILoggingService logging, IMessageSerializer serializer)
        {
            this.config = config;
            this.logging = logging;
            this.serializer = serializer;
        }



        public void Dispose()
        {
        }

        public void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {

            var client = new TopicClient(asbConnectionString, topicName);
            client.SendAsync(new Message(serializer.SerializeToArray(messageContent))).Wait();
        }

        public async Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {
            var client = new TopicClient(asbConnectionString, topicName);
            await client.SendAsync(new Message(serializer.SerializeToArray(messageContent)));
        }

        public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionQueueName = null, SubscriptionOptions options = null)
        {
            return new AzureServiceBusTopicClient<TContent>(logging, asbConnectionString, topicName, subscriptionQueueName, options, serializer);
        }
    }
}
