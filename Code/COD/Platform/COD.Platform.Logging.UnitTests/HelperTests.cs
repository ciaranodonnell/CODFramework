using COD.Platform.Logging.MockLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace COD.Platform.Logging.UnitTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void TestGetCurrentClassLogger()
        {

            MockLoggingService service = new MockLoggingService();
            var log = service.GetCurrentClassLogger() as MockLogger;
            Assert.AreEqual(typeof(HelperTests).FullName, log.LoggerName);

        }


        [TestMethod]
        public void TestGetCurrentClassLoggerAction()
        {
            HelperTestSub sub = new HelperTestSub();
            sub.theAction = () =>
             {
                 MockLoggingService service = new MockLoggingService();
                 var log = service.GetCurrentClassLogger() as MockLogger;
                 Assert.AreEqual(typeof(HelperTests).FullName, log.LoggerName);
             };
            sub.CallTheAction();


        }


        [TestMethod]
        public void TestGetCurrentClassLoggerContainedClass()
        {
            HelperTestSub sub = new HelperTestSub();
            Assert.AreEqual(typeof(HelperTestSub).FullName, sub.GetLoggerName());
        }

        class HelperTestSub
        {
            public Action theAction;
            public void CallTheAction()
            {
                theAction();
            }
            public string GetLoggerName()
            {
                MockLoggingService service = new MockLoggingService();
                var log = service.GetCurrentClassLogger() as MockLogger;
                return log.LoggerName;
            }

        }

    }
}
