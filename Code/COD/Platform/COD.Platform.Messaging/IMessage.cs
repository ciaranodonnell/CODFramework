namespace COD.Platform.Messaging
{
    public interface IMessage<TContent>
    {
        TContent Content { get; }
    }
}