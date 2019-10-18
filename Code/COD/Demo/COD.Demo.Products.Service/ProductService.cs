using COD.Platform.ServiceHosting;
using System;

namespace COD.Demo.Products.Service
{
    public class ProductService : IHostedService
    {
        public event Action ServiceFailed;

        public void RunService()
        {
            
        }

        public void Shutdown()
        {
            
        }
    }
}
