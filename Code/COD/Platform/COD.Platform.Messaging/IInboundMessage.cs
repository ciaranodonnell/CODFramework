using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging
{
    public interface IInboundMessage<TMessage> : IMessage<TMessage>
    {

        /// <summary>
        /// The time the message was recevied. This is just for information, incase you want to judge the delay in processing the message
        /// </summary>
        DateTimeOffset ReceivedTime { get; }

        /// <summary>
        /// The correlation id sent with the message. This was added the message that started a system wide action and should be propogated to all events/messages that are triggered by this message
        /// </summary>
        string CorrelationId { get; }

        /// <summary>
        /// This is a message Id that might be assigned by the message broker. It if for informational purposes to help correlate to platform level logging
        /// </summary>
        string MessageId { get; }

        /// <summary>
        /// Call this method to inform the message broker that the message has been received and processed. 
        /// This will prevent the message from re-appearing on the inbound queue. 
        /// Failure to cause this method means the message will not be removed from the queue and will be redelivered.
        /// </summary>
        void Acknowledge();


        /// <summary>
        /// Calling this method tells the message bus that processing the message has failed and the message should be left on the queue, and redelivered immediately.
        /// </summary>
        void Abort();
    }
}
