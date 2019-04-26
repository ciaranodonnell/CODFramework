namespace COD.Platform.Messaging
{
    public interface ISubscriptionClient<TContent>
    {
        string Topic { get; }

        event MessageReceivedDelegate<TContent> MessageReceived;

        event MessageReceiveErrorDelegate<TContent> MessageReceiveError;

        void Dispose();
        void Unsubscribe();
    }

    public delegate void MessageReceivedDelegate<TContent>(object sender, MessagedReceivedArguments<TContent> args);
    public delegate void MessageReceiveErrorDelegate<TContent>(object sender, MessagedReceiveErrorArguments<TContent> args);

}