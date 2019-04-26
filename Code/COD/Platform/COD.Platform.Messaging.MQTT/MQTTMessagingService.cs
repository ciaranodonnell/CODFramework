using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.MQTT
{
    public class MQTTMessagingService : MessagingService
    {
        private IManagedMqttClient msgClient;
        private IConfiguration config;
        private ILoggingService logService;
        private ILog log;


        public MQTTMessagingService(ILoggingService loggingService, IConfiguration config, ConnectionDetails connectionDetails)
        {
            this.config = config;
            this.logService = loggingService;
            this.log = loggingService.GetCurrentClassLogger();
            CreateMQTTClient(connectionDetails);

        }

        private void CreateMQTTClient(ConnectionDetails connectionDetails)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateManagedMqttClient();

            var options = new ManagedMqttClientOptionsBuilder().WithAutoReconnectDelay(TimeSpan.FromSeconds(connectionDetails.AutoReconnectTime))
                .WithClientOptions(new MqttClientOptionsBuilder()
                .WithClientId(connectionDetails.ClientId)
                .WithTcpServer(connectionDetails.Server)
                .WithCredentials(connectionDetails.Username, connectionDetails.Password)
                .WithTls()
                .WithCleanSession()
                .Build())
                .Build();

            //mqttClient.PublishAsync()
            //mqttClient.ConnectAsync(options).Wait();
            //mqttClient.Disconnected += async (s, e) =>
            //{

            //    this.IsConnected = false;

            //    log.Warn("### DISCONNECTED FROM SERVER ###");
            //    await Task.Delay(TimeSpan.FromSeconds(2));

            //    try
            //    {
            //        await mqttClient.ConnectAsync(options);
            //        this.IsConnected = true;
            //    }
            //    catch
            //    {
            //        log.Error("### RECONNECTING FAILED ###");
            //    }
            //};
            msgClient = mqttClient;
        }

        public bool IsConnected { get; private set; }


        public override void SendMessageToTopic<TContent>(string topicName, TContent messageContent, string correlationId = null)
        {


            var message = new MqttApplicationMessageBuilder()
      .WithTopic(topicName)
      .WithPayload(Newtonsoft.Json.JsonConvert.SerializeObject(messageContent))
      .WithAtLeastOnceQoS()
      .WithRetainFlag()
      .Build();

            msgClient.PublishAsync(message).Wait();
        }

        public override Task SendMessageToTopicAsync<TContent>(string topicName, TContent messageContent, string correlationId = null)
        {
            return Task.Run(() => SendMessageToTopic(topicName, messageContent, correlationId));
        }


        public override ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionName=null, SubscriptionOptions options=null)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            this.msgClient?.Dispose();
        }
    }
}
