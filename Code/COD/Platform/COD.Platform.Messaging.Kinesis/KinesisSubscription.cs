using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using COD.Platform.Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Kinesis
{
    public class KinesisSubscription<TContent> : ISubscriptionClient<TContent>
    {
        private AmazonKinesisClient client;
        private ILoggingService loggingService;
        private ILog log;

        public string Topic { get; protected set; }

        public event MessageReceivedDelegate<TContent> MessageReceived;
        public event MessageReceiveErrorDelegate<TContent> MessageReceiveError;


        public KinesisSubscription(AmazonKinesisConfig config, string topicName, ILoggingService loggingService)
        {
            this.loggingService = loggingService;
            this.log = loggingService.GetCurrentClassLogger();
        }

        private async Task InitializeSubscription(AmazonKinesisConfig config, string topicName, string consumerName)
        {
            //create config that points to Kinesis region
                       
            //create new client object
            client = new AmazonKinesisClient(config);

            //Step #1 - describe stream to find out the shards it contains
            DescribeStreamRequest describeRequest = new DescribeStreamRequest();
            describeRequest.StreamName = topicName;

            DescribeStreamResponse describeResponse = await client.DescribeStreamAsync(describeRequest);
            var shards = describeResponse.StreamDescription.Shards;

            log.Trace($"Kinesis shard for {topicName}: " + string.Join(", ", shards.Select(s => s.ShardId))); 

            

            var result = await client.RegisterStreamConsumerAsync(new RegisterStreamConsumerRequest{ ConsumerName = consumerName, StreamARN = topicName });
            var consumer = result.Consumer;

            var r = await client.ListShardsAsync(new ListShardsRequest{ StreamName = topicName, );
            
            
            

            //grab the only shard ID in this stream
            string primaryShardId = shards[0].ShardId;

            //Step #2 - get iterator for this shard
            GetShardIteratorRequest iteratorRequest = new GetShardIteratorRequest();
            iteratorRequest.StreamName = "OrderStream";
            iteratorRequest.ShardId = primaryShardId;
            iteratorRequest.ShardIteratorType = ShardIteratorType.TRIM_HORIZON;

            GetShardIteratorResponse iteratorResponse = client.GetShardIterator(iteratorRequest);
            string iterator = iteratorResponse.ShardIterator;

            

            Console.WriteLine("Iterator: " + iterator);

            //Step #3 - get records in this iterator
            GetShardRecords(client, iterator);

            Console.WriteLine("All records read.");
            Console.ReadLine();


        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }
    }
}
