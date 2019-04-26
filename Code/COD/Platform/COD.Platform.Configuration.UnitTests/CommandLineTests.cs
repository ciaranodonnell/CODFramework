using COD.Platform.Configuration.Basic;
using COD.Platform.Configuration.Core;
using COD.Platform.Logging.MockLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COD.Platform.Configuration.UnitTests
{
    [TestClass]
    public class CommandLineTests
    {

        private MockLoggingService LoggingService() { return new MockLoggingService(); }


        [TestMethod]
        public void TestSingleArgument()
        {
            string[] args = new string[] { "NAME=VALUE" };
            CommandLineConfig config = new CommandLineConfig(LoggingService(), args);

            Assert.AreEqual("VALUE", config.GetString("NAME"));

        }

        [TestMethod]
        public void TestTwoArguments()
        {
            string[] args = new string[] { "NAME=VALUE", "NAME2=VALUE2" };
            CommandLineConfig config = new CommandLineConfig(LoggingService(), args);

            Assert.AreEqual("VALUE", config.GetString("NAME"));
            Assert.AreEqual("VALUE2", config.GetString("NAME2"));

        }



        [TestMethod]
        public void TestCanGetInt()
        {
            string[] args = new string[] { "NAME=1" };
            CommandLineConfig config = new CommandLineConfig(LoggingService(), args);

            Assert.AreEqual(1, config.GetInt32("NAME"));

        }


        [TestMethod]
        public void TestCanGetBool()
        {
            string[] args = new string[] { "NAME=1" };
            CommandLineConfig config = new CommandLineConfig(LoggingService(), args);

            Assert.AreEqual(true, config.GetBool("NAME"));

        }

    }
}
