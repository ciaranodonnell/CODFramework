using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework.UnitTest
{
    [TestClass]
    public class StringManipulatorTests
    {

        [TestMethod]
        public void Test_StringManipulatorAddString()
        {
            StringManipulator m = new StringManipulator();
            m.Add("hello");

            Assert.AreEqual("hello", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulatorAddTwoStrings()
        {
            StringManipulator m = new StringManipulator();
            m.Add("hello");
            m.Add(" world");

            Assert.AreEqual("hello world", m.ToString());
        }



        [TestMethod]
        public void Test_StringManipulatorInsertString()
        {
            StringManipulator m = new StringManipulator();
            m.Add("hello");
            m.Add("  world");
            m.Insert("cruel", 6);
            Assert.AreEqual("hello cruel world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossTwoSections()
        {
            StringManipulator m = new StringManipulator();
            m.Add("hello cr");
            m.Add("uel world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossThreeSections()
        {
            StringManipulator m = new StringManipulator();
            m.Add("hello cr");
            m.Add("u");
            m.Add("el world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }

    }
}
