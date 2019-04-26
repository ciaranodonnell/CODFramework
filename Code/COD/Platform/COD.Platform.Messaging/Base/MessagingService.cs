using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging
{
    public abstract class MessagingService : IMessagingService
    {

        public abstract Task SendMessageToTopicAsync<TContent>(string topicName, TContent messageContent, string correlationId = null);
     
        public abstract void SendMessageToTopic<TContent>(string topicName, TContent messageContent, string correlationId = null);


        public abstract ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionName, SubscriptionOptions options);


        public abstract void Dispose();
    }
}
