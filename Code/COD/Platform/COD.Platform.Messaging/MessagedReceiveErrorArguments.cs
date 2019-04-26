using System;

namespace COD.Platform.Messaging
{
    public class MessagedReceiveErrorArguments<TContent> : EventArgs
    {
        public MessagedReceiveErrorArguments(Exception exceptionRaised, string message)
        {
            this.Exception = exceptionRaised;
            this.AppMessage = message;
        }

        /// <summary>
        /// The exception that happened when receiving the message
        /// </summary>
        public Exception Exception { get; }
        /// <summary>
        /// A string that helps understand what was happening when the exception happened
        /// </summary>
        public string AppMessage { get; }
    }
}