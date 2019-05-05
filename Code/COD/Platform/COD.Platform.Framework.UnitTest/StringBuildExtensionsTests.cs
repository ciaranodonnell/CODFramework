using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework.UnitTest
{
    [TestClass]
  public  class StringBuildExtensionsTests
    {

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_ExactMatch()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(0, sb.IndexOf(SampleText));
        }


        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleCharAtStart()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(0, sb.IndexOf("S"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleCharInMiddle()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(5, sb.IndexOf("T"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleWordInMiddle()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(2, sb.IndexOf("ME"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleWordAtEnd()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(6, sb.IndexOf("EXT"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_NoMatch()
        {
            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(-1, sb.IndexOf("NOT FOUND"));
        }



        string SampleText = "SOME TEXT SOME TEXT NO TEXT";

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleCharAtStart_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.IndexOf("E",10), sb.IndexOf("E",10));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleCharInMiddle_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.IndexOf("T", 3), sb.IndexOf("T",3));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleWordInMiddle_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.IndexOf("TEX", 2), sb.IndexOf("TEX",2));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_SingleWordAtEnd_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.IndexOf("EXT",3), sb.IndexOf("EXT",3));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_IndexOf_NoMatch_WithStartIndex()
        {
            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(-1, sb.IndexOf("NOT FOUND",4));
        }



        #region LastIndexOf



        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleCharAtStart_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("S", 10), sb.LastIndexOf("S", 10));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleCharInMiddle_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("T", 20), sb.LastIndexOf("T", 20));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleWordInMiddle_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("TEX", 20), sb.LastIndexOf("TEX", 20));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleWordAtEnd_WithStartIndex()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("EXT", 20), sb.LastIndexOf("EXT", 20));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_NoMatch_WithStartIndex()
        {
            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(-1, sb.LastIndexOf("NOT FOUND", 20));
        }


        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleCharAtStart()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("S"), sb.LastIndexOf("S"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleCharInMiddle()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("T"), sb.LastIndexOf("T"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleWordInMiddle()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("TEX"), sb.LastIndexOf("TEX"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_SingleWordAtEnd()
        {

            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(SampleText.LastIndexOf("EXT"), sb.LastIndexOf("EXT"));
        }

        [TestMethod]
        public void Test_StringBuilderExtensions_LastIndexOf_NoMatch()
        {
            StringBuilder sb = new StringBuilder(SampleText);
            Assert.AreEqual(-1, sb.LastIndexOf("NOT FOUND"));
        }

        #endregion
    }
}
