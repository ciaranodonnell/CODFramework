using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.Base
{
    public abstract class InboundMessage<TContent> : Message<TContent>, IInboundMessage<TContent>
    {

        public InboundMessage(TContent message, DateTimeOffset receviedTime)
        : base(message)
        {
            this.ReceivedTime = receviedTime;
        }

        Action DoAcknowledge = null, DoAbort = null;


        public InboundMessage(TContent messasge, DateTimeOffset receviedTime, Action DoAcknowledge, Action DoAbort) : this(messasge, receviedTime)
        {
            this.DoAbort = DoAbort;
            this.DoAcknowledge = DoAcknowledge;
        }
        /// <summary>
        /// This is a constructor for sub classes to use when they want to handle all of the initialization.
        /// </summary>
        protected InboundMessage() : base(default(TContent))
        {

        }


        public DateTimeOffset ReceivedTime { get; set; }

        /// <summary>
        /// Completes the processing of the message and tells the message bus to remove the message
        /// </summary>
        public  virtual void Acknowledge()
        {
            DoAcknowledge?.Invoke();
        }

        public virtual void Abort()
        {
            DoAbort?.Invoke();
        }

    }
}
