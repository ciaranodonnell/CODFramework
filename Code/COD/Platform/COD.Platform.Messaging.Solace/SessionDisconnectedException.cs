using System;
using System.Runtime.Serialization;

namespace COD.Platform.Messaging.Solace
{
    [Serializable]
    internal class SessionDisconnectedException : Exception
    {
        public SessionDisconnectedException()
        {
        }

        public SessionDisconnectedException(string message) : base(message)
        {
        }

        public SessionDisconnectedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionDisconnectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}