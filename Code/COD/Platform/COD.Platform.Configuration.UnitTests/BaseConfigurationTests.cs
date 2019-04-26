using COD.Platform.Configuration.Basic;
using COD.Platform.Configuration.Core;
using COD.Platform.Logging.MockLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COD.Platform.Configuration.UnitTests
{
    [TestClass]
    public class BaseConfigurationTests
    {

        public IConfiguration GetConfig() {

            return new BasicConfiguration(new MockLoggingService());
        }



        [TestMethod]
        public void TestBasicGetString()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "WORLD");
            Assert.AreEqual("WORLD", config.GetString("HELLO"), "Didnt get the string back from config");
        }

        [TestMethod]
        public void TestBasicGetStringMiss()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "WORLD");
            Assert.AreEqual("MISSED", config.GetString("HELLO2", "MISSED"), "Didnt get the default string back from config miss");
        }


        [TestMethod]
        public void TestGetStringOrError()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "WORLD");
            Assert.ThrowsException<ConfigurationException>(() => config.GetStringOrError("NOT HELLO"), "Didnt get the exception when missing config");
        }

        [TestMethod]
        public void TestGetInt32()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "1");
            Assert.AreEqual(1, config.GetInt32("HELLO"), "Didnt get the right number back from config");
        }
        [TestMethod]
        public void TestGetInt32Negative()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "-1");
            Assert.AreEqual(-1, config.GetInt32("HELLO"), "Didnt get the right number back from config");
        }


        [TestMethod]
        public void TestGetInt32Miss()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "-1");
            Assert.AreEqual(11, config.GetInt32("HELLO2", 11), "Didnt get the right number back from config miss");
        }


        [TestMethod]
        public void TestGetInt32WithDecimal()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "-1.5");
            Assert.ThrowsException<ConfigurationException>(()=> config.GetInt32("HELLO", 11), "Didnt get the right exception for decimal instead of int");
        }



        [TestMethod]
        public void TestGetBool_True()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "True");
            Assert.AreEqual(true, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }

        [TestMethod]
        public void TestGetBool_NotABool()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "not a bool");
            Assert.ThrowsException<ConfigurationException>(()=> config.GetBool("HELLO"));
        }

        [TestMethod]
        public void TestGetBool_true()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "true");
            Assert.AreEqual(true, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }
        [TestMethod]
        public void TestGetBool_Y()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "Y");
            Assert.AreEqual(true, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }
        [TestMethod]
        public void TestGetBool_y()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "y");
            Assert.AreEqual(true, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }
        [TestMethod]
        public void TestGetBool_1()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "1");
            Assert.AreEqual(true, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }
        [TestMethod]
        public void TestGetBool_False()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "False");
            Assert.AreEqual(false, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }


        [TestMethod]
        public void TestGetBool_false()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "false");
            Assert.AreEqual(false, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }


        [TestMethod]
        public void TestGetBool_n()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "n");
            Assert.AreEqual(false, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }


        [TestMethod]
        public void TestGetBool_N()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "N");
            Assert.AreEqual(false, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }

        [TestMethod]
        public void TestGetBool_0()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "0");
            Assert.AreEqual(false, config.GetBool("HELLO"), "Didnt get the right boolean back");
        }


        [TestMethod]
        public void TestGetBoolMiss()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "true");
            Assert.AreEqual(false, config.GetBool("HELLO2", false), "Didnt get the right boolean back");
        }
        [TestMethod]
        public void TestGetBoolMissTrue()
        {
            var config = GetConfig() as BasicConfiguration;
            config.AddConfigurationOption("HELLO", "true");
            Assert.AreEqual(true, config.GetBool("HELLO2", true), "Didnt get the right boolean back");
        }



    }
}
