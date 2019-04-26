using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.EventGrid
{
    public class EventGridMessagingService : IMessagingService
    {

        private readonly string asbConnectionString = null;
        private IConfiguration config;
        private ILoggingService logging;
        private IMessageSerializer serializer;
        private string topicKey;
        private string domainEndpoint;

        public EventGridMessagingService(IConfiguration config, ILoggingService logging, IMessageSerializer serializer)
        {
            this.config = config;
            this.logging = logging;
            this.serializer = serializer;

            //this.topicKey = config.GetStringOrError("EventGridDomainKey"); //TQ0iDbtOqoPlqnB2OmI7x91z7P5zRgS0EmiZl/FhMq8=
            this.domainEndpoint = /*config.GetStringOrError("EventGridDomain"); */ "https://egdemodomain.centralus-1.eventgrid.azure.net/api/events";

        }


        public void Dispose()
        {

        }

        public void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {

            var msg = new EventGridEvent()
            {
                Id = correlationId ?? Guid.NewGuid().ToString(),
                EventType = typeof(TMessage).FullName,
                Data = messageContent,
                EventTime = DateTime.UtcNow,
                Subject = topicName,
                DataVersion = "1.0"
            };
            var eventList = new List<EventGridEvent> { msg };

            string topicEndpoint = "https://thebesttopic.westus2-1.eventgrid.azure.net/api/events";
            string topicKey = "1dgI3v1QYwqSa0WPKMnd8ZV5ImWqdIRhAquy9T3pK5A=";
            //string topicHostname = new Uri(topicEndpoint).Host;

            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            client.PublishEventsAsync(topicEndpoint, eventList).GetAwaiter().GetResult();


        }

        public Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {
            throw new NotImplementedException();
        }

        public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionQueueName = null, SubscriptionOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
