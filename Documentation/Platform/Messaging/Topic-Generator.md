# Topic Generator
In order to have good topic structures, it will be necessary to publish to topics that contain
some information from the message. A clear example of this is the [MenuChangeSuspected](/Message-Contracts/MenuChangeSuspectedEvent.md) event.
The topic for this event is _MenuChangeSuspected/\{PropertyId\}/\{RevenueCenterId\}_

The easy way to get the topic string for this is with the TopicGenerator class.
Its a static class with some helper methods to generate complete topic strings for you.

## Example

``` csharp
class Example
{
    public int RevenueCenterId {get;set;}
    public int PropertyId {get;set;}
}

class NestedExample
{
    public Example SubExample {get;set;}
}

var example = new Example { RevenueCenterId = 1, PropertyId = 2};
var example2 = new NestedExample { SubExample = example };

var topic1 = "TopicString/{PropertyId}/{RevenueCenterId}";
var topic2 = "TopicString/{SubExample.PropertyId}";


var completeTopic1 = TopicGenerator.GetPropertyFromObject(example, topic1);


var completeTopic2 = TopicGenerator.GetPropertyFromObject(example2, topic2);

//completedTopic1 = "TopicString/2/1"
//completedTopic2 = "TopicString/2"


```

