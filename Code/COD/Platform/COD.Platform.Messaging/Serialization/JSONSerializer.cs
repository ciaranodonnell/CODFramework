using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace COD.Platform.Messaging.Core.Serialization
{
    public class JSONSerializer : StringBasedSerializer
    {
        public override T DeserializeFromString<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public override string SerializeToString<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
