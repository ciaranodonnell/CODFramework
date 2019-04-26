using COD.Platform.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.UnitTesting
{
    [DataContract]
    public class TestingMessage
    {

        [DataMember]
        public string MessageString { get; set; }

    }
}
