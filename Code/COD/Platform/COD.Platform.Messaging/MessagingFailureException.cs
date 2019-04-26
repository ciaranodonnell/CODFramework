using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Messaging
{
    public class MessagingFailureException : Exception
    {

        public MessagingFailureException(string message) : base(message) { }
    }
}
