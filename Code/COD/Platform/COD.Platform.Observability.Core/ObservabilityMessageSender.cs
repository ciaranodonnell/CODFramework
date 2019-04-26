using COD.Platform.Configuration.Core;
using COD.Platform.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Observability.Core
{
    public class ObservabilityMessageSender
    {
        private readonly string topicName = "Observability/Report";
        private readonly IMessagingService msgService;

        public ObservabilityMessageSender(IMessagingService msgService)
        {
            this.msgService = msgService;
        }

        public void SendObservabilityReport(string message, int messageId, string causeCorrelationId, ServiceInfo serviceInfo, string jsonInfo = "")
        {
            var obsvEvent = new ObservabilityReport()
            {
                CauseCorrlationId = causeCorrelationId,
                Message = message,
                MessageId = messageId,
                ServiceInfo = serviceInfo,
                DetailedJsonInfo = jsonInfo
            };

            msgService.SendMessageToTopic(topicName, obsvEvent);
        }
    }
}
