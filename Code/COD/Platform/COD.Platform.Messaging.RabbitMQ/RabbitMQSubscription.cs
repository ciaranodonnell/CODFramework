using COD.Platform.Messaging.Base;
using COD.Platform.Messaging.Core.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace COD.Platform.Messaging.Rabbit
{
    internal class RabbitMQSubscription<TContent> : ISubscriptionClient<TContent>
    {
        private IMessageSerializer serializer;
        private string topicName;
        private string subscriptionName;
        private SubscriptionOptions options;
        private SubscriptionType subscriptionType;
        private bool exclusive;
        private IConnection rabbitMQ;
        private IModel rabbitModel;
        private EventingBasicConsumer rabbitConsumer;
        private bool isSubscribed;

        public RabbitMQSubscription(string topicName, string subscriptionName, SubscriptionOptions options, IConnection rabbitMQ, IMessageSerializer serializer)
        {
            this.serializer = serializer;
            this.topicName = topicName;
            this.subscriptionName = subscriptionName;
            this.options = options;
            this.rabbitMQ = rabbitMQ;

            this.rabbitModel = rabbitMQ.CreateModel();
            rabbitModel.ExchangeDeclare(topicName, ExchangeType.Topic, options.Durable, false, null);
            rabbitModel.QueueDeclare(subscriptionName, options.Durable, exclusive, false);
            rabbitModel.QueueBind(subscriptionName, topicName, "");

            this.rabbitConsumer = new EventingBasicConsumer(rabbitModel);

            rabbitConsumer.Received += RabbitConsumer_Received;

            this.isSubscribed = true;

        }

        public string Topic => topicName;

        public event MessageReceivedDelegate<TContent> MessageReceived;
        public event MessageReceiveErrorDelegate<TContent> MessageReceiveError;

        public void Dispose()
        {
            if (this.isSubscribed)
                Unsubscribe();
        }

        public void Unsubscribe()
        {
            this.rabbitModel.Dispose();
            this.isSubscribed = false;
        }

        private void RabbitConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var message = serializer.Deserialize<TContent>(e.Body);

            MessageReceived?.Invoke(this, new MessagedReceivedArguments<TContent>
            {
                Message =
                new InboundRabbitMessage<TContent>(message, e.BasicProperties.CorrelationId, DateTimeOffset.Now, e.DeliveryTag.ToString(),
                () => rabbitConsumer.Model.BasicNack(e.DeliveryTag, false, true),
                () => rabbitConsumer.Model.BasicAck(e.DeliveryTag, false)
                                               )
            });

        }
    }
}