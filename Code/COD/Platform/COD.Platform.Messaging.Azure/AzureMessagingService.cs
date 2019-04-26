//using Microsoft.ServiceBus;
//using Microsoft.Azure.ServiceBus;
//using COD.Platform.Messaging;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using COD.Platform.Logging.Core;
//using COD.Platform.Configuration.Core;

//namespace COD.Platform.Messaging.AzureSB
//{
//    public class AzureMessagingService : MessagingService
//    {
//        private ILoggingService logFactory;
//        private ILog log;
//        private string keyName;
//        private string accessKey;
//        private string baseAddress;
//        private NamespaceManager nsManager;
//        private MessagingFactory messagingFactory;

//        private bool createTopicIfNotExists = true;

//        public AzureMessagingService(ILoggingService logFactory, IConfiguration config)
//            : this(logFactory, config.GetString("AZURESB_KEYNAME"), config.GetString("AZUREDB_ACCESSKEY"), config.GetString("AZUREDB_BASEADDRESS"))
//        {

//        }

//        public AzureMessagingService(ILoggingService logFactory, string keyName, string accessKey, string baseAddress)
//        {
//            this.logFactory = logFactory;
//            this.log = logFactory.GetCurrentClassLogger();

//            this.keyName = keyName;
//            this.accessKey = accessKey;
//            this.baseAddress = baseAddress;



//            this.nsManager = NamespaceManager.CreateFromConnectionString($"Endpoint={baseAddress}/;SharedAccessKeyName={keyName};SharedAccessKey={accessKey}");

//            TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(keyName, accessKey);
//            this.messagingFactory = MessagingFactory.Create(baseAddress, tokenProvider);
//        }

//        internal void DeleteSubscription(string name, string topicPath)
//        {
//            nsManager.DeleteSubscription(topicPath, name);
//        }

//        internal void DeleteQueue(string path)
//        {
//            nsManager.DeleteQueue(path);
//        }

//        public override void SendMessageToTopic<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
//        {
//            if (createTopicIfNotExists)
//            {
//                if (!nsManager.TopicExists(topicName))
//                {
//                    var td = new TopicDescription(topicName);
//                    nsManager.CreateTopic(td);
//                }
//            }

//            MessageSender sender = messagingFactory.CreateMessageSender(topicName);

//            var outboundMessage = new Message<TMessage>(messageContent, correlationId ?? Guid.NewGuid().ToString());

//            var message = new BrokeredMessage(outboundMessage);
//            message.TimeToLive = TimeSpan.FromSeconds(10);

//            sender.Send(message);
//        }
//        public override async Task SendMessageToTopicAsync<TMessage>(string topicName, TMessage messageContent, string correlationId = null)
//        {
//            if (createTopicIfNotExists)
//            {
//                if (!(await nsManager.TopicExistsAsync(topicName)))
//                {
//                    var td = new TopicDescription(topicName);
//                    await nsManager.CreateTopicAsync(td);
//                }
//            }

//            MessageSender sender = await messagingFactory.CreateMessageSenderAsync(topicName);

//            var outboundMessage = new Message<TMessage>(messageContent, correlationId ?? Guid.NewGuid().ToString());

//            var message = new BrokeredMessage(outboundMessage);
//            message.TimeToLive = TimeSpan.FromSeconds(10);

//            await sender.SendAsync(message);
//        }




//        public override ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionName = null, SubscriptionOptions options = null)
//        {
//            if (this.createTopicIfNotExists)
//            {

//                if (!nsManager.TopicExists(topicName))
//                {
//                    var td = new TopicDescription(topicName);
//                    nsManager.CreateTopic(td);
//                }

//                if (!nsManager.SubscriptionExists(topicName, subscriptionName)) nsManager.CreateSubscription(new SubscriptionDescription(topicName, subscriptionName));

//            }

//            var asbSubscription = messagingFactory.CreateSubscriptionClient(topicName, subscriptionName, options.SubscriptionType == SubscriptionType.PeekAndLock ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete);

//            return new AzureSubscriptionClient<TContent>(asbSubscription, options.Durable, this, options.SubscriptionType);
//        }

//        public override void Dispose()
//        {

//        }


//        //public override ISubscriptionClient<TContent> SubscribeToQueue<TContent>(string queueName, bool destroyOnClose, SubscriptionType subscriptionType)
//        //{


//        //    if (this.createTopicIfNotExists)
//        //    {
//        //        if (!nsManager.QueueExists(queueName)) nsManager.CreateQueue(queueName);
//        //    }

//        //    var queueClient = messagingFactory.CreateQueueClient(queueName, subscriptionType == SubscriptionType.PeekAndLock ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete);

//        //    return new AzureQueueClient<TContent>(queueClient, false, this, subscriptionType);

//        //}

//        //public override void SendMessageToQueue<TMessage>(string queueName, TMessage messageContent, string correlationId = null)
//        //{
//        //    if (this.createTopicIfNotExists)
//        //    {
//        //        if (!nsManager.QueueExists(queueName))
//        //        {
//        //            var td = new QueueDescription(queueName);
//        //            nsManager.CreateQueue(td);
//        //        }
//        //    }

//        //    MessageSender sender = messagingFactory.CreateMessageSender(queueName);

//        //    var outboundMessage = AzureInboundMessage<TMessage>.NewOutboundMessage(messageContent, correlationId ?? Guid.NewGuid().ToString());

//        //    var message = new BrokeredMessage(outboundMessage);

//        //    sender.Send(message);
//        //}


//        //public override async Task SendMessageToQueueAsync<TMessage>(string queueName, TMessage messageContent, string correlationId = null, bool createQueueIfNotExists = false)
//        //{
//        //    if (createQueueIfNotExists)
//        //    {
//        //        if (!(await nsManager.QueueExistsAsync(queueName)))
//        //        {
//        //            var td = new QueueDescription(queueName);
//        //            await nsManager.CreateQueueAsync(td);
//        //        }
//        //    }

//        //    MessageSender sender = await messagingFactory.CreateMessageSenderAsync(queueName);

//        //    var outboundMessage = AzureInboundMessage<TMessage>.NewOutboundMessage(messageContent, correlationId ?? Guid.NewGuid().ToString());

//        //    var message = new BrokeredMessage(outboundMessage);

//        //    await sender.SendAsync(message);
//        //}
//    }
//}
