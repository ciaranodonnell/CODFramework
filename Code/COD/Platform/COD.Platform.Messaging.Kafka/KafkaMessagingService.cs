using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using System;
using System.Threading.Tasks;
using Confluent.Kafka;


namespace COD.Platform.Messaging.Kafka
{
	public class KafkaMessagingService : IMessagingService
	{

		private IConfiguration config;
		private ILoggingService loggingService;
		private ILog log;
		private ConnectionDetails configBasedConnectionDetails;
		private IMessageSerializer serializer;
		private string uniqueClientName;

		public KafkaMessagingService(ILoggingService loggingService, IConfiguration config, IMessageSerializer serializer, string uniqueClientName)
		{
			this.serializer = serializer;
			this.uniqueClientName = uniqueClientName;
			this.config = config;
			this.loggingService = loggingService;
			this.log = loggingService.GetCurrentClassLogger();

			this.configBasedConnectionDetails = new ConnectionDetails(config);


		}

		public void Dispose()
		{
			
		}

		public void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
		{
			SendMessageToTopicAsync(topicName, messageContent, correlationId).Wait();
		}

		public async Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
		{
			var config = new ProducerConfig { BootstrapServers = configBasedConnectionDetails.Server , ClientId=uniqueClientName};

			// A Producer for sending messages with null keys and UTF-8 encoded values.
			using (var p = new Producer<Null, string>(config))
			{
				try
				{
					var dr = await p.ProduceAsync(topicName, new Message<Null, string> {  Value=serializer.SerializeToString(messageContent) });
					log.Debug($"Sent '{dr.Value}' to '{dr.TopicPartitionOffset}'");
				}
				catch (KafkaException e)
				{
					log.Error(e, $"Delivery failed: {e.Error.Reason}");
					await Task.FromException(e);	
				}
			}
		}

		public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionQueueName = null, SubscriptionOptions options = null)
		{
			throw new NotImplementedException();
		}
	}
}
