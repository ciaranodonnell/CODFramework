using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.Mock
{
    public class MockSubscription<TMessage> : ISubscriptionClient<TMessage>
    {
        public string Topic { get; set; }

        public event MessageReceivedDelegate<TMessage> MessageReceived;
        public event MessageReceiveErrorDelegate<TMessage> MessageReceiveError;

        internal MockSubscription(string topic)
        {
            Topic = topic;
            IsSubscribed = true;
        }


        public bool IsSubscribed
        {
            get;
            private set;
        }

        internal void RaiseMessage(TMessage message, string correlationId)
        {
            if (IsSubscribed)
            {
                MessageReceived(this, new MessagedReceivedArguments<TMessage> { Message = new MockMessage<TMessage>(message, DateTimeOffset.Now) { CorrelationId = correlationId } });
            }
        }


        public void Dispose()
        {
            Unsubscribe();
        }

        public void Unsubscribe()
        {
            IsSubscribed = false;
        }
    }
}
