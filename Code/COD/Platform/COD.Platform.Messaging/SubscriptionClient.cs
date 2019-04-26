using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging
{


    public abstract class SubscriptionClient<TContent> : IDisposable, ISubscriptionClient<TContent>
    {


        bool isDisposed = false;

        public event MessageReceivedDelegate<TContent> MessageReceived;

        public event MessageReceiveErrorDelegate<TContent> MessageReceiveError;

        protected void OnMessageReceived(object sender, MessagedReceivedArguments<TContent> args)
        {
            MessageReceived?.Invoke(sender, args);
        }
        protected void OnMessageReceiveError(Exception ex, string message)
        {
            MessageReceiveError?.Invoke(this, new MessagedReceiveErrorArguments<TContent>(ex, message));
        }

        public virtual void Dispose()
        {
            isDisposed = true;
        }

        public string Topic { get; protected set; }


        public abstract void Unsubscribe();

    }
}
