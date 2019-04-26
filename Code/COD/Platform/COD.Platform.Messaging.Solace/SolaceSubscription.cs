using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging.Core.Serialization;
using SolaceSystems.Solclient.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace COD.Platform.Messaging.Solace
{
    public class SolaceTopicSubscription<TContent> : SubscriptionClient<TContent>
    {
        private IContext solaceContext;
        private ISession session;
        private IMessageSerializer serializer;
        private ILoggingService logFactory;
        private ILog log;
        private IConfiguration config;
        private IFlow flow;
        private string subscriptionQueueName;
        private Thread workerThread;

        internal SolaceTopicSubscription(ILoggingService logFactory, IConfiguration config, ISession session, IContext solaceContext,
           IMessageSerializer serializer, string topicName, SubscriptionOptions options, string uniqueClientName)
        {
            this.serializer = serializer;
            this.logFactory = logFactory;
            this.log = logFactory.GetCurrentClassLogger();
            this.config = config;
            this.session = session;
            this.solaceContext = solaceContext;

            SubscribeToTopic(session, topicName, uniqueClientName, options: options);
        }

        protected virtual void SubscribeToTopic(ISession session, string topicName, string clientName, string subscriptionQueueName = null, SubscriptionOptions options = null)
        {
            if (string.IsNullOrEmpty(subscriptionQueueName))
                subscriptionQueueName = $"{clientName}_{typeof(TContent)}";

            this.subscriptionQueueName = subscriptionQueueName;

            log.Debug("Subscribing to topic {0} on {1} endpoint named {2}", topicName, options.Durable ? "durable" : "non-durable", subscriptionQueueName);

            ITopicEndpoint endpoint = null;
            if (options.Durable)
            {

                endpoint = ContextFactory.Instance.CreateDurableTopicEndpointEx(subscriptionQueueName);

                session.Provision(endpoint, new EndpointProperties
                {
                    DiscardBehavior = EndpointProperties.EndpointDiscardBehavior.NotifySenderOn,
                    AccessType = options.Exclusive ? EndpointProperties.EndpointAccessType.Exclusive : EndpointProperties.EndpointAccessType.NonExclusive
                }, ProvisionFlag.WaitForConfirm | ProvisionFlag.IgnoreErrorIfEndpointAlreadyExists, null);

            }
            else
            {
                endpoint = session.CreateNonDurableTopicEndpoint(subscriptionQueueName);
            }

            try
            {
                this.flow = session.CreateFlow(
                                    new FlowProperties { AckMode = (options?.SubscriptionType ?? SubscriptionType.PeekAndLock) == SubscriptionType.PeekAndLock ? MessageAckMode.ClientAck : MessageAckMode.AutoAck },
                                    endpoint,
                                    ContextFactory.Instance.CreateTopic(topicName),
                                    HandleMessage,
                                    FlowEventHandler);

                flow.Start();

                this.workerThread = new Thread(new ThreadStart(RunMessageLoop));
                workerThread.Start();
            }
            catch (global::SolaceSystems.Solclient.Messaging.OperationErrorException ex)
            {
                log.Error($"Failed to subscribe to topic. ErrorInfo: {ex.ErrorInfo}");
                throw;
            }

        }

        void RunMessageLoop()
        {
            while (!isClosing)
            {
                waitHandle.WaitOne(2000);
                while (incomingMessages.TryDequeue(out var msg))
                {
                    log.Trace(() => "Deququed Message " + msg.ProcessMessageNumber.ToString() + ", raising event. Queue Delay is " + DateTimeOffset.UtcNow.Subtract(msg.ReceivedTimestamp).TotalMilliseconds.ToString());
                    base.OnMessageReceived(this, msg);
                }

            }
        }
        bool isClosing = false;
        ConcurrentQueue<MessagedReceivedArguments<TContent>> incomingMessages = new ConcurrentQueue<MessagedReceivedArguments<TContent>>();
        AutoResetEvent waitHandle = new AutoResetEvent(false);

        public override void Unsubscribe()
        {
            log.Debug($"Unsubscribe called on Subscription {this.subscriptionQueueName}. Disposing Flow");
            isClosing = true;
            this.flow.Dispose();
        }
        private void FlowEventHandler(object source, FlowEventArgs e)
        {
            if (e.Event == FlowEvent.ParentSessionDown)
            {
                log.Error("The flow has reported that the Session is down. Everything must die");
                this.OnMessageReceiveError(new SessionDisconnectedException(e.Info), "The flow has reported that the Session is down. Everything must die.");
                
            }
            else
            {
                log.Debug($"Flow Event handler - source: {source},Event={e.Event}, Info=\"{e.Info}\", ResponseCode={e.ResponseCode}, Properties={e.EventProperties}");
            }
        }


        long ProcessMessageNumber = 1;

        private void HandleMessage(object source, MessageEventArgs args)
        {

            ProcessMessageNumber++;

            log.Trace(() => $"Received Message from Solace - {ProcessMessageNumber}");

            // Received a message
            IMessage solMsg = args.Message;

            var messageId = args.Message.ADMessageId;



            SolaceMessage<TContent> msg = new SolaceMessage<TContent>(args.Message, serializer, () => flow.Ack(messageId), () => { });
            var qItem = new MessagedReceivedArguments<TContent> { Message = msg, ReceivedTimestamp = DateTimeOffset.Now, ProcessMessageNumber = ProcessMessageNumber};
            incomingMessages.Enqueue(qItem);

            waitHandle.Set();

        }


    }
}
