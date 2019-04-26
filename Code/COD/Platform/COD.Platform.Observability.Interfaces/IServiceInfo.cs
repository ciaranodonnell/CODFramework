namespace COD.Platform.Observability.Interfaces
{
    public interface IServiceInfo
    {
        string UniqueServiceName { get; }
        string HostName { get; }
        string NetworkAddress { get; }

        
        
    }
}