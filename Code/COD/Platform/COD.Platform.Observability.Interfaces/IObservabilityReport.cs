using System;

namespace COD.Platform.Observability.Interfaces
{
    public interface IObservabilityReport
    {
        IServiceInfo ServiceInfo { get; }
        string Message { get; }
        int MessageId { get; }
        string CauseCorrlationId { get; }


        string DetailedJsonInfo { get; }

    }
}
