using COD.Platform.Configuration.Basic;
using COD.Platform.Configuration.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.UnitTests.Placeholders
{
    [TestClass]
    public class PlaceholderTests
    {

        public IConfiguration GetConfig()
        {
            var config = new BasicConfiguration();
            config.AddConfigurationOption("NewName", "NewValue");
            config.AddConfigurationOption("ParameterizedValue", "${NewName}");
            return config;
        }

        [TestMethod]
        public void ResolveSinglePlaceHolder_WholeValue()
        {
            var resolver = new PlaceholderResolver(GetConfig());
            var thevalue = resolver.ResolveItem("ParameterizedValue", "FallBack Value");
            Assert.AreEqual("NewValue", thevalue, "String should be NewValue");

        }
        [TestMethod]
        public void Test_ContainsPlaceHolder_OnlyAPlaceholder()
        {
            Assert.IsTrue(PlaceholderResolver.ContainsPlaceholder(PlaceholderResolver.START + "NAME" + PlaceholderResolver.END));
        }


        [TestMethod]
        public void Test_ContainsPlaceHolder_PlaceholderAtEnd()
        {
            Assert.IsTrue(PlaceholderResolver.ContainsPlaceholder("some text" + PlaceholderResolver.START + "NAME" + PlaceholderResolver.END));
        }

        [TestMethod]
        public void Test_ContainsPlaceHolder_PlaceholderInMiddle()
        {
            Assert.IsTrue(PlaceholderResolver.ContainsPlaceholder("some text" + PlaceholderResolver.START + "NAME" + PlaceholderResolver.END + "some more text"));
        }
    }
}
