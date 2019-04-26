# Mocking the Message Broker


There is a MockMessagingService class that implements the IMessagingService interface. This will return Mock versions of the subscription objects to any code that subscribes to Topics. It also exposes a message called SendMessageToSelf which allows the tests to send messages that will be received by those subscribers.


## Testing connected services
There is also a flag for **SendAllMessagesToSelf**. This flag allows you to "Start" two seperate services that should communicate in the same UnitTest and have messages sent by one component arrive that the other.




