using COD.Platform.Messaging.Base;
using COD.Platform.Messaging.Core.Serialization;
using SolaceSystems.Solclient.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging.Solace
{
    public class SolaceMessage<TContent> : InboundMessage<TContent>
    {
        
        private Action ackAction;
        private Action abortAction;
        

        internal SolaceMessage(IMessage mbMessage, IMessageSerializer serializer, Action acknowledgeAction, Action abortAction)
        {
            this.CorrelationId = mbMessage.CorrelationId;
            this.Content = serializer.Deserialize<TContent>((byte[])(Array)mbMessage.GetBinaryAttachment());
            this.ackAction = acknowledgeAction;
            this.abortAction = abortAction;
        }


        
        public override void Abort()
        {
            abortAction();
        }

        public override void Acknowledge()
        {
            ackAction();
        }

        
    }
}