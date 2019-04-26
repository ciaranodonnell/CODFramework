using COD.Platform.Observability.Interfaces;

namespace COD.Platform.Observability.Core
{
    public class ServiceInfo : IServiceInfo
    {
        public string UniqueServiceName { get; set; }

        public string HostName { get; set; }

        public string NetworkAddress { get; set; }
    }
}