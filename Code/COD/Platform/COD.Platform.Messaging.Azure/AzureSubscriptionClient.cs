//using COD.Platform.Messaging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Azure.ServiceBus;

//namespace COD.Platform.Messaging.AzureServiceBus
//{
//    public class AzureSubscriptionClient<TContent> : SubscriptionClient<TContent>
//    {
//        private Microsoft.Azure.ServiceBus.SubscriptionClient asbSubscription;
//        private bool durable;
//        private AzureServiceBusMessagingService asbService;
//        private SubscriptionType subscriptionType;

//        public AzureSubscriptionClient(Microsoft.Azure.ServiceBus.SubscriptionClient asbSubscription, bool durable, AzureServiceBusMessagingService asbService, SubscriptionType subscriptionType)
//        {
//            this.subscriptionType = subscriptionType;
//            this.asbSubscription = asbSubscription;
//            this.durable = durable;
//            this.asbService = asbService;
//            this.subscriptionType = subscriptionType;

//            asbSubscription.RegisterMessageHandler(HandleMessage, new MessageHandlerOptions(HandleMessageException) { AutoComplete = false, MaxConcurrentCalls = 1 });
//            //new MessageHandlerOptions( { AutoComplete = false, MaxConcurrentCalls = 1 });
//        }

//        async Task HandleMessage(Message message, CancellationToken token)
//        {

//            try
//            {
//                await Task.Run(() => OnMessageReceived(this, new MessagedReceivedArguments<TContent>
//                {
//                    Message = AzureInboundMessage<TContent>.LoadFromBrokeredMessage(message, this.subscriptionType),
//                    ReceivedTimestamp = DateTimeOffset.UtcNow
//                }));
//            }
//            catch (Exception ex)
//            {
//                await Task.Run(() => OnMessageReceiveError(ex, "asbSubscription.OnMessage"));
//            }
//        }

//        Task HandleMessageException(ExceptionReceivedEventArgs message)
//        {
//            return Task.Run(() => OnMessageReceiveError(message.Exception, "Error Received from ASB: " + message.ExceptionReceivedContext.ToString()));
//        }



//        public override void Unsubscribe()
//        {
//            if (!asbSubscription.IsClosedOrClosing)
//            {
//                asbSubscription.CloseAsync();

//                if (!durable)
//                    asbService.DeleteSubscription(asbSubscription.SubscriptionName, asbSubscription.TopicPath);
//            }
//        }

//        public override void Dispose()
//        {
//            Unsubscribe();
//            base.Dispose();

//        }
//    }
//}
