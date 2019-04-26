using COD.Platform.Messaging.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.Mock
{
    public class MockMessage<TContent> : InboundMessage<TContent>
    {
        public MockMessage(TContent message, DateTimeOffset receviedTime) : base(message, receviedTime)
        {
        }

        public override void Acknowledge()
        {
            AcknowledgeCount++;
        }

        public override void Abort()
        {
            AbortCount++;
        }
        
        public int AcknowledgeCount { get; set; }
        public int AbortCount { get; set; }
        


    }
}
