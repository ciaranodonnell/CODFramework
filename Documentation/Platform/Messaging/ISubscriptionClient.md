# ISubscriptionClient\<TContent\>

The interface is the type returned when you subscribe to a Topic or Queue on the message broker. 
The type exposes and event for receiving messages, plus methods for managing the subscription

The Generic Type Parameter TContent is the type of application message that will be expected on that Topic/Queue

It is good practice to only send a **single** type of message on a specific topic or Queue.

```csharp
public interface ISubscriptionClient<TContent>
{
    string Topic { get; }

    event MessageReceivedDelegate<TContent> MessageReceived;

    void Dispose();
    void Unsubscribe();
}

public delegate void MessageReceivedDelegate<TContent>(object sender, MessagedReceivedArguments<TContent> args);
        
```

## Instantiation
Types that implement this interface are created by the IMessagingService implementation you are using. 
There is typically a different implementation of this interface for each type of message bus you use. 

## Receiving Events

The 


