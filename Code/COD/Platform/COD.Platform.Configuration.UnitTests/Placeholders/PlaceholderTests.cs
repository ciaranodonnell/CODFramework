using COD.Platform.Configuration.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Configuration.UnitTests.Placeholders
{
    [TestClass]
    public class PlaceholderTests
    {

        [TestMethod]
        public void ResolveSinglePlaceHolder_WholeValue()
        {

            var newName = PlaceholderResolver.ResolveItem("${NewName}", new BasicConfiguration(null));
            Assert.AreEqual("NewName", newName, "String should be NewName");

        }


    }
}
