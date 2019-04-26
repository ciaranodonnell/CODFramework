using System;

namespace COD.Platform.Messaging
{
    public class MessagedReceivedArguments<TContent> : EventArgs
    {
        public DateTimeOffset ReceivedTimestamp { get; set; }
        public IInboundMessage<TContent> Message { get; set; }
        public long ProcessMessageNumber { get; set; }
    }
}