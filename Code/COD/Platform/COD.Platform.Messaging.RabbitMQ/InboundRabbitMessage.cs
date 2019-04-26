using COD.Platform.Messaging.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.Rabbit
{
    class InboundRabbitMessage<TContent> : InboundMessage<TContent>
    {

        public InboundRabbitMessage(TContent content, string correlationId, DateTimeOffset received,
            string deliveryTag,
            Action abortAction, Action ackAction)
        {
            this.CorrelationId = correlationId;
            this.Content = content;
            ReceivedTime = received;
            this.abortAction = abortAction;
            this.ackAction = ackAction;
            MessageId = deliveryTag;

        }

        private Action abortAction;
        private Action ackAction;


        public override void Abort()
        {
            abortAction?.Invoke();
        }

        public override void Acknowledge()
        {
            ackAction?.Invoke();
        }

    }
}
