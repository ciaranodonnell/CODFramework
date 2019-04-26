using COD.Platform.Observability.Interfaces;
using System;

namespace COD.Platform.Observability.Core
{
    public class ObservabilityReport : IObservabilityReport
    {

        public ServiceInfo ServiceInfo { get; set; }
        IServiceInfo IObservabilityReport.ServiceInfo => ServiceInfo;

        public string Message { get; set; }

        public int MessageId { get; set; }

        public string CauseCorrlationId { get; set; }

        public string DetailedJsonInfo { get; set; }

		public DateTimeOffset ObservationTime { get; set; }
    }
}
