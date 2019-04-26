using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Rabbit
{
    public class RabbitMQMessagingService : IMessagingService
    {
        private IConnection rabbitMQ;
        private ILoggingService loggingService;
        private ILog log;
        private IConfiguration configuration;
        private ConnectionDetails connectionDetails;
        private IMessageSerializer serializer;

        public RabbitMQMessagingService(ILoggingService loggingService, IConfiguration config, IMessageSerializer serializer)
        {
            this.loggingService = loggingService;
            this.log = loggingService.GetCurrentClassLogger();
            this.configuration = config;
            this.connectionDetails = new ConnectionDetails(config);
            this.serializer = serializer;
            ConnectToBroker(new ConnectionDetails(config));
        }


        private void ConnectToBroker(ConnectionDetails connectionDetails)
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = connectionDetails.Username;
            factory.Password = connectionDetails.Password;
            factory.VirtualHost = connectionDetails.Vpn;
            factory.HostName = connectionDetails.Server;

            this.rabbitMQ = factory.CreateConnection();
        }

        public void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {
            var model = rabbitMQ.CreateModel();
            var props = model.CreateBasicProperties();
            props.AppId = connectionDetails.ClientId;
            props.CorrelationId = correlationId ?? Guid.NewGuid().ToString();
            props.Type = typeof(TMessage).Name;

            model.BasicPublish(new PublicationAddress(ExchangeType.Topic, topicName, ""), props, serializer.SerializeToArray(messageContent));
        }

        public Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {
            return Task.Run(() => SendMessageToTopic(topicName, messageContent, correlationId));
        }

        public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionName=null, SubscriptionOptions options=null)
        {
            
            var sub = new RabbitMQSubscription<TContent>(topicName, subscriptionName, options, rabbitMQ, serializer);
            return sub;
        }

        public void Dispose()
        {
            
        }
    }
}
