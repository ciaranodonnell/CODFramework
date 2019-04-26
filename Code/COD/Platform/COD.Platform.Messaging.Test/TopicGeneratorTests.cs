using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD.Platform.Messaging.Test
{
    [TestClass]
    public class TopicGeneratorTests
    {
        [TestMethod]
        public void TestNoWork()
        {
            Assert.AreEqual("test", TopicGenerator.GenerateTopicForObject("test", new object()), "TopicGenerator Should return topics without params in them");
        }


        [TestMethod]
        public void TestDoesntAllowNullTopic()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TopicGenerator.GenerateTopicForObject(null, new object()), "TopicGenerator shouldnt allow null topics");
        }

        [TestMethod]
        public void TestDoesntAllowNullMessage()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TopicGenerator.GenerateTopicForObject("test",null), "TopicGenerator shouldnt allow null messages");
        }


        [TestMethod]
        public void SimpleProp()
        {
            Assert.AreEqual("test/test", TopicGenerator.GenerateTopicForObject("test/{prop}", new { prop = "test" }), "TopicGenerator should have swapped in a property");
        }


        [TestMethod]
        public void TextCanDoTwo()
        {
            Assert.AreEqual("MenuChangeSuspected/1/2", 
                TopicGenerator.GenerateTopicForObject("MenuChangeSuspected/{PropertyId}/{RevenueCenterId}", new { PropertyId= "1", RevenueCenterId=2 }), "TopicGenerator should have swapped in a property");
            
            
        }


        [TestMethod]
        public void TextCanDoNested()
        {
            Assert.AreEqual("MenuChangeSuspected/2",
                TopicGenerator.GenerateTopicForObject("MenuChangeSuspected/{PropertyId.RevenueCenterId}", new { PropertyId = new { RevenueCenterId = 2 } }), "TopicGenerator should have swapped in a property");


        }



        [TestMethod]
        public void GetPropertyFromObject()
        {
            Assert.AreEqual("FOOBAR",
                TopicGenerator.GetPropertyFromObject(new { PropertyId = "FOOBAR" }, "PropertyId"));

        }
        [TestMethod]
        public void GetPropertyFromSubObjectWithDot()
        {
            Assert.AreEqual("FOOBAR",
                TopicGenerator.GetPropertyFromObject(new { Main = new { Sub = "FOOBAR" } }, "Main.Sub"));

        }

    }
}
