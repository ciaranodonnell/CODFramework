using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.ServiceHosting
{
    public interface IHostedService
    {


        event Action ServiceFailed;

        void RunService();

        void Shutdown();

    }
}
