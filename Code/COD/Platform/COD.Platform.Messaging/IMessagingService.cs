using COD.Platform.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging
{
    public interface IMessagingService: IDisposable
    {
        #region Sending
        void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null);

        Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null);


        #endregion


        #region Receiving 
        ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionQueueName = null, SubscriptionOptions options = null);

        

        #endregion
    }
}
