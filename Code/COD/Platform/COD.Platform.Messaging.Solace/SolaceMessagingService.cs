using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using COD.Platform.Messaging;
using COD.Platform.Messaging.Core.Serialization;
using SolaceSystems.Solclient.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Solace
{
    public class SolaceMessagingService : MessagingService
    {
        private IContext solaceContext;
        private ISession session;
        private IConfiguration config;
        private ILoggingService loggingService;
        private ILog log;
        private IMessageSerializer serializer;
        private string uniqueClientName;

        public SolaceMessagingService(ILoggingService loggingService, IConfiguration config, IMessageSerializer serializer, string uniqueClientName)
        {
            this.serializer = serializer;
            this.uniqueClientName = uniqueClientName;

            this.config = config;
            this.loggingService = loggingService;
            this.log = loggingService.GetCurrentClassLogger();

            ContextFactoryProperties cfp = new ContextFactoryProperties()
            {
                SolClientLogLevel = SolLogLevel.Debug
            };
            
            //Direct logging to our logger
         //   cfp.LogDelegate = (s) => log.Debug($"Solace log: {s.LogLevel}, {s.LoggerName}, {s.LogException}, {s.LogMessage}");

            ContextFactory.Instance.Init(cfp);
            var configBasedConnectionDetails = new ConnectionDetails(config);

            if (!string.IsNullOrEmpty(configBasedConnectionDetails.Server))
            {
                Init(configBasedConnectionDetails);
            }
        }

        /// <summary>
        /// Connect to a Solace Router described by the connection details
        /// </summary>
        /// <param name="connectionDetails"></param>
        private void Init(ConnectionDetails connectionDetails)
        {
            SessionProperties sessionProps = new SessionProperties()
            {
                Host = connectionDetails.Server,
                VPNName = connectionDetails.Vpn,
                UserName = connectionDetails.Username,
                Password = connectionDetails.Password,
                ReconnectRetries = connectionDetails.DefaultReconnectRetries
            };

            Console.WriteLine("Connecting to Solace as as {0}@{1} on {2}...", connectionDetails.Username, connectionDetails.Vpn, connectionDetails.Server);

            var context = ContextFactory.Instance.CreateContext(new ContextProperties(), null);

            var session = context.CreateSession(sessionProps,null, null);

            ReturnCode returnCode = session.Connect();

            if (returnCode == ReturnCode.SOLCLIENT_OK)
            {
                this.solaceContext = context;
                this.session = session;
            }
            else
            {
                throw new MessagingFailureException($"Solace Session.Connect() returned {returnCode}");
            }

        }


        private void HandleMessage(object source, MessageEventArgs args)
        {
            Console.WriteLine("Received published message.");
            // Received a message
            using (IMessage message = args.Message)
            {
                // Expecting the message content as a binary attachment
                Console.WriteLine("Message content: {0}", Encoding.ASCII.GetString(message.BinaryAttachment));
                // finish the program
            }
        }

        public override void SendMessageToTopic<TContent>(string topicName, TContent messageContent, string correlationId = null)
        {
            try
            {
                using (var msg = ContextFactory.Instance.CreateMessage())
                {
                    msg.CorrelationId = correlationId;
                    msg.DeliveryMode = MessageDeliveryMode.Persistent;
                    msg.Destination = ContextFactory.Instance.CreateTopic(topicName);
                    msg.SetBinaryAttachment((sbyte[])(Array)serializer.SerializeToArray(messageContent));
                    var returnCode = session.Send(msg);

                    if (returnCode != ReturnCode.SOLCLIENT_OK)
                    {
                        throw new SendFailedException($"Response from Send Function = {returnCode}");
                    }
                }
            }catch(SolaceSystems.Solclient.Messaging.OperationErrorException ex)
            {
                log.Error(ex, $"Error sending a message. Error Info:{ex.ErrorInfo}");
                throw;
            }
        }

        public override Task SendMessageToTopicAsync<TContent>(string topicName, TContent messageContent, string correlationId = null)
        {
            return Task.Run(() => SendMessageToTopic(topicName, messageContent, correlationId));
        }


        public override ISubscriptionClient<TContent> SubscribeToTopic<TContent>(string topicName, string subscriptionName = null, SubscriptionOptions options = null)
        {

            var subscription = new SolaceTopicSubscription<TContent>(this.loggingService, this.config, this.session,
                this.solaceContext, serializer, topicName, options ?? new SubscriptionOptions(), this.uniqueClientName);

            this.subs.Add(subscription);

            return subscription;


        }

        bool isDisposed;
        List<IDisposable> subs = new List<IDisposable>();
        public override void Dispose()
        {
            if (subs != null)
            {
                foreach (var sub in subs) sub.Dispose();
            }
            if (session != null)
            {
                try
                {
                    session.Disconnect();
                }catch(Exception ex)
                {
                    log.Debug(ex, "Ignorable Exception disconnecting solace session");
                }
                try{
                    session.Dispose();
                }
                catch (Exception ex)
                {
                    log.Debug(ex, "Ignorable Exception disposing solace session");
                }
            }
        }

        ~SolaceMessagingService()
        {
            if (!isDisposed) Dispose();
        }



    }
}
