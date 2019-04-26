using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Mock
{
    public class MockMessagingService : IMessagingService
    {

        private ConnectionDetails myConnectionDetails = null;
        private Dictionary<string, object> mySubscriptions = new Dictionary<string, object>();
        private Dictionary<string, object> wildCardSubscriptions = new Dictionary<string, object>();
        private Dictionary<string, List<object>> mySentMessages = new Dictionary<string, List<object>>();

        public void Connect(ConnectionDetails details)
        {
            myConnectionDetails = details;
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        public bool SendAllMessagesToSelf { get; set; }


        public bool IsConnected { get; private set; }


        public List<object> GetSentMessages(string topic)
        {
            return mySentMessages.ContainsKey(topic) ? mySentMessages[topic] : new List<object>();
        }

        public void SendMessageToTopic<TContent>(string topicName, TContent message, string correlationId = null)
        {


            List<object> messages;

            var messageType = typeof(TContent);

            if (!mySentMessages.TryGetValue(topicName, out messages))
            {
                mySentMessages[topicName] = messages = new List<object>();
            }

            messages.Add(message);

            if (SendAllMessagesToSelf)
            {
                SendAMessageToSelf(topicName, message, correlationId);
            }

        }

        public void SendAMessageToSelf<TContent>(string topic, TContent message, string correlationId = null)
        {


            var messageType = typeof(TContent);
            object sub;
            if (mySubscriptions.TryGetValue(topic, out sub))
            {
                var subscription = sub as MockSubscription<TContent>;
                subscription.RaiseMessage(message, correlationId);
            }


            foreach (var entry in wildCardSubscriptions)
            {
                if (topic.StartsWith(entry.Key))
                {
                    sub = entry.Value;

                    var subscription = sub as MockSubscription<TContent>;
                    subscription.RaiseMessage(message, correlationId);

                }
            }

        }



        public Task SendMessageToTopicAsync<TContent>(string topicName, TContent messageContent, string correlationId = null)
        {
            return Task.Run(() => SendMessageToTopic(topicName, messageContent, correlationId));
        }

        public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topic, string subscriptionName = null, SubscriptionOptions options = null)
        {
            object sub;
            MockSubscription<TContent> subscription;
            var messageType = typeof(TContent);

            if (topic.Contains(">"))
            {
                topic = topic.Replace(">", "");

                if (wildCardSubscriptions.TryGetValue(topic, out sub))
                {
                    subscription = sub as MockSubscription<TContent>;
                }
                else
                {
                    subscription = new MockSubscription<TContent>(topic);
                    wildCardSubscriptions[topic] = subscription;
                }
            }
            else
            {
                if (mySubscriptions.TryGetValue(topic, out sub))
                {
                    subscription = sub as MockSubscription<TContent>;
                }
                else
                {
                    subscription = new MockSubscription<TContent>(topic);
                    mySubscriptions[topic] = subscription;
                }
            }
            return subscription;
        }

        public void Dispose() { }
    }
}
