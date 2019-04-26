using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace COD.Platform.Messaging.CLITester.PubSub
{
    [DataContract]
    public class TestMessage
    {

        [DataMember]
        public string TestString { get; set; }

        [DataMember]
        public int TestInt { get; set; }

    }
}
