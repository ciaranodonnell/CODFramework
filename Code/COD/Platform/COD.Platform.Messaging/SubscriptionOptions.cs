namespace COD.Platform.Messaging
{
    public class SubscriptionOptions
    {
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = true;
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.PeekAndLock;
   }



}