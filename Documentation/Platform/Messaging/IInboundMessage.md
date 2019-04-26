# IInboundMessage\<TContent\> : Message\<TContent\>

This interface represents the messages that come in from a message broker. It includes extra fields compared the message, including the ReceivedTime and methods for controlling the redelivery of the message.

```csharp
public interface IInboundMessage<TMessage> : IMessage<TMessage>
{

    /// <summary>
    /// The time the message was recevied. This is just for information, incase you want to judge the delay in processing the message
    /// </summary>
    DateTimeOffset ReceivedTime { get; }

    /// <summary>
    /// The correlation id sent with the message. This was added the message that started a system wide action and should be propogated to all events/messages that are triggered by this message
    /// </summary>
    string CorrelationId { get; }

    /// <summary>
    /// This is a message Id that might be assigned by the message broker. It if for informational purposes to help correlate to platform level logging
    /// </summary>
    string MessageId { get; }

    /// <summary>
    /// Call this method to inform the message broker that the message has been received and processed. 
    /// This will prevent the message from re-appearing on the inbound queue. 
    /// Failure to cause this method means the message will not be removed from the queue and will be redelivered.
    /// </summary>
    void Acknowledge();


    /// <summary>
    /// Calling this method tells the message bus that processing the message has failed and the message should be left on the queue, and redelivered immediately.
    /// </summary>
    void Abort();

}
        
```

## Instantiation
This is the Interface that is used for messages that are delivered from Subscriptions. You shouldnt need to instatiate one directly unless you are implementing a new message broker wrapper. 


## Dead Lettering
There is a common concept in the message bus world called the Dead Letter and the Dead Letter queue. 
This is a message bus feature that can be useful when the bus is able to detect that something has 
not beed processed before the expiry of its TTL (Time To Live) or if it has been delivered a number 
of times and not acknowledged, indicating a Poison Letter.

However, in the Marco Polo platform, we dont want to explicitly make use of this feature. 
It is common for Dead Letter queues to not be monitored well and teams to ignore the fact that
they should be processing those messages and diagnosing the problems that caused them to be 
put there. 

Therefore, the strategy in this solution is to purposefully choose the next step in the processing for 
messages that arrive in our dead letter scenarios. We will do this by purposely sendind the mesasge
to a new topic and acknowledging the original message. Our message contract includes the original timestamp 
of the event that caused the event. This should not be altered when resending the message for dead letter processing. 
Instead, if a new timestamp or error information is to be included, then a new Message contract
should be created to contain the original message and the processing errors. 




