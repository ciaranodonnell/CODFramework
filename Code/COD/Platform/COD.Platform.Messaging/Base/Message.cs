﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace COD.Platform.Messaging
{


    [DataContract]
    public class Message<TContent> : IMessage<TContent>
    {

        public Message(TContent message)
        {
            this.Content = message;
        }

        public Message(TContent message, string correlationId) : this(message)
        {
            this.CorrelationId = correlationId;
        }

        [DataMember]
        public TContent Content { get; protected set; }


        [DataMember]
        public DateTimeOffset GeneratedTime { get; protected set; }

        public virtual void Dispose()
        {
        }

        [DataMember]
        public string CorrelationId { get; set; }


        /// <summary>
        /// MessageId is populated on inbound messages with the Id generated by the platform.
        /// </summary>
        [DataMember]
        public string MessageId { get; set; }
        
    }
}
