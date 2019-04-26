using COD.Platform.Configuration.Basic;
using COD.Platform.Configuration.Core;
using COD.Platform.Logging.MockLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COD.Platform.Configuration.UnitTests
{
    [TestClass]
    public class EnvironmentTests
    {

        private MockLoggingService LoggingService() { return new MockLoggingService(); }


     

        [TestMethod]
        public void TestCanGetPath()
        {
            EnvironmentConfiguration config = new EnvironmentConfiguration(LoggingService());

            Assert.AreEqual(System.Environment.ExpandEnvironmentVariables("%PATH%"), config.GetString("PATH"));

        }

    }
}
