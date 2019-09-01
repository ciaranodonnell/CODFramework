using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Kinesis
{
    public class KinesisMessagingService : COD.Platform.Messaging.IMessagingService
    {
        private AmazonKinesisConfig config;
        private IMessageSerializer serializer;
        private ILog log;
        private ILoggingService logger;

        public KinesisMessagingService(AmazonKinesisConfig config, IMessageSerializer serializer, ILoggingService logger)
        {
            this.config = config;
            this.serializer = serializer;

            this.logger = logger;
            this.log = this.logger.GetCurrentClassLogger();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {


            AmazonKinesisClient kinesisClient = new AmazonKinesisClient(config);
            String kinesisStreamName = topicName;



            using (var memoryStream = new MemoryStream())
            {

                try
                {
                    serializer.SerializeToStream(memoryStream, messageContent);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var requestRecord = new PutRecordRequest();
                    requestRecord.StreamName = kinesisStreamName;
                    requestRecord.PartitionKey = "url-response-times";
                    requestRecord.Data = memoryStream;

                    var responseRecord = await kinesisClient.PutRecordAsync(requestRecord);
                    log.Debug($"Successfully sent record to Kinesis topic {topicName} with CorrelationId {correlationId}");
                }
                catch (Exception ex)
                {
                    log.Error("Failed to send record to Kinesis topic {topicName} with CorrelationId {correlationId}", ex.Message);
                }
            }
        }


        public void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
        {
            SendMessageToTopicAsync(topicName, messageContent, correlationId).Wait();
        }

        public ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionQueueName = null, SubscriptionOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
