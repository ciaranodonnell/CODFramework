
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using COD.Platform.Messaging;
using COD.Platform.Messaging.Base;
using COD.Platform.Messaging.Core.Serialization;
using Microsoft.Azure.ServiceBus;

namespace COD.Platform.Messaging.AzureServiceBus
{
    [DataContract]
    class AzureInboundMessage<TContent> : InboundMessage<TContent>
    {
        private Microsoft.Azure.ServiceBus.Message innerMessage;
        private bool subscriptionIsReceiveAndDelete;
        private SubscriptionClient azureSubscription;

        internal AzureInboundMessage() : base()
        {
        }

        public override void Dispose()
        {

        }


        internal static AzureInboundMessage<TContent> LoadFromBrokeredMessage(Microsoft.Azure.ServiceBus.Message inboundMessage,
            SubscriptionType subscriptioType, IMessageSerializer serializer, Microsoft.Azure.ServiceBus.SubscriptionClient subscription)
        {


            var content = serializer.Deserialize<TContent>(inboundMessage.Body);

            var msg = new AzureInboundMessage<TContent>
            {
                innerMessage = inboundMessage,
                azureSubscription = subscription,
                Content = content,
                subscriptionIsReceiveAndDelete = subscriptioType == SubscriptionType.ReceiveAndDelete,
                //This probably isnt necessary
                CorrelationId = inboundMessage.CorrelationId,
                ReceivedTime = DateTimeOffset.UtcNow,
                MessageId = inboundMessage.MessageId,
            };

            return msg;

        }


        public override void Acknowledge()
        {
            azureSubscription.CompleteAsync(innerMessage.SystemProperties.LockToken).Wait();
        }

        public override void Abort()
        {
            azureSubscription.AbandonAsync(innerMessage.SystemProperties.LockToken).Wait();
        }


    }




}
