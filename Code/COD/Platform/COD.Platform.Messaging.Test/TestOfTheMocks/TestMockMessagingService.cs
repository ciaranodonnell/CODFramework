using COD.Platform.Messaging.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Test.TestOfTheMocks
{
    [TestClass]
    public class TestMockMessagingService
    {
        private const string TestTopic1 = "testtopic1";

        [TestMethod]
        public void TestSendMessageWorks()
        {
            var msgService = TestingHelpers.GetMessaingService();
            string correlationId = Guid.NewGuid().ToString();

            var subscription = msgService.SubscribeToTopic<TestingMessage>(TestTopic1);

            var message = new TestingMessage { MessageString = "hello, it's me" };
            TestingMessage receivedMsg = null;
            string receivedCorId = null;

            subscription.MessageReceived += (sender, args) => { receivedMsg = args.Message.Content; receivedCorId = args.Message.CorrelationId; };

            msgService.SendAMessageToSelf(TestTopic1, message, correlationId);
            
            
            Assert.IsNotNull(receivedMsg, "Didnt receive the message");
            Assert.IsNotNull(receivedCorId, "Didnt receive the correlation id sent with the message");

            Assert.AreEqual(message.MessageString, receivedMsg.MessageString, "The message contents werent the same");

            //Assert.AreEqual(1, msgService.GetSentMessages(TestTopic1).Count, "Didnt return sent message");

        }
    }
}
