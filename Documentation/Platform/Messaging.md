# Platform Messaging Library
The Messaging Library is designed to wrap the Message Broker implemenation and client libraries 
with a standard API. This prevents the client application from having to worry about things like
serialization formats, message headers, the broker specific APIs or having to find a way to Mock the Broker.


---

## IMessagingService
The IMessagingService Interface is the primary entry point for the HMS.Platform.Messaging library.
This is accepted through Dependency Injection into all services.

## SubscriptionClient

The [ISubscriptionClient](Messaging/ISubscriptionClient.md) interface is the type returned when you subscribe to a Topic or Queue on the message broker. 
The type exposes and event for receiving messages, plus methods for managing the subscription




## Testing Message Flows
[See Mocks](Mocks.md)
