﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace COD.Platform.Framework.UnitTest
{
    [TestClass]
    public class StringManipulatorTests
    {
        [TestInitialize]
        public void TestInit()
        {
            //remove assembly loading from test timing
            StringManipulator m = new StringManipulator();

        }


        [TestMethod]
        public void Test_StringManipulator_AddString()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello");

            Assert.AreEqual("hello", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_AddTwoStrings()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello");
            m.Append(" world");

            Assert.AreEqual("hello world", m.ToString());
        }



        [TestMethod]
        public void Test_StringManipulator_InsertString()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello");
            m.Append("  world");
            m.Insert("cruel", 6);
            Assert.AreEqual("hello cruel world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeFirstSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("world");
            m.Remove("hello ");

            Assert.AreEqual("world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeMidSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Remove("cruel ");

            Assert.AreEqual("hello world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeLastSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Remove("world");

            Assert.AreEqual("hello cruel ", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeLastSectionAndReplace()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Replace("world", "suzy");

            Assert.AreEqual("hello cruel suzy", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeFirstSectionAndReplace()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("world");
            m.Replace("hello ", "bonjour ");

            Assert.AreEqual("bonjour world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeMidSectionAndReplace()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Replace("cruel ", "big ");

            Assert.AreEqual("hello big world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveMidTextAndReplace()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello");
            m.Append(" cruel ");
            m.Append("world");
            m.Replace(" cruel ", " big ");

            Assert.AreEqual("hello big world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_InsertStringInBetweenSections()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello ");
            m.Append(" world");
            m.Insert("cruel", 6);
            Assert.AreEqual("hello cruel world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossTwoSections()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cr");
            m.Append("uel world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringInsideSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cruel world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_RemoveTwoStringsInsideSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cruel world");
            m.Remove("l");

            Assert.AreEqual("heo crue word", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossThreeSections()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cr");
            m.Append("u");
            m.Append("el world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
            Assert.AreEqual("hello  world".Length, m.Length, "The Lengths should be the same too");
        }


        [TestMethod]
        public void Test_StringManipulator_SubstringSingleSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cruel world");

            Assert.AreEqual("cruel", m.Substring(6, 5));
        }

        [TestMethod]
        public void Test_StringManipulator_SubstringMultipleSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("cruel", m.Substring(6, 5));
        }


        [TestMethod]
        public void Test_StringManipulator_IndexOfSingleSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello".IndexOf('l'), m.IndexOf("l"));
        }



        [TestMethod]
        public void Test_StringManipulator_IndexOfSingleLetter()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello".IndexOf('l'), m.IndexOf("l"));
        }

        [TestMethod]
        public void Test_StringManipulator_IndexOfString()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("cru"), m.IndexOf("cru"));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringAcrossSections()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("uel"), m.IndexOf("uel"));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringInSecondSections()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("rld"), m.IndexOf("rld"));
        }


        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndex()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("l", 3), m.IndexOf("l", 3));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndexInLaterSection()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("l", 7), m.IndexOf("l", 7));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndexInLaterSectionNotFound()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("k", 7), m.IndexOf("k", 7));
        }



        [TestMethod]
        public void TestTiming()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < 1000; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Insert(50, "World");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Assert.IsFalse(true, $"{sw.ElapsedMilliseconds}");

        }


        [TestMethod]
        public void TestTimingSM()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sb = new StringManipulator();
            for (int x = 0; x < 1000; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Insert("World",50);
            }

            for (int x = 0; x < 1000; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Assert.IsFalse(true, $"{sw.ElapsedMilliseconds}");

        }


        [TestMethod]
        public void Test_StringManipulator_ReplaceSingle()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cruel world");
            m.Replace("cruel", "big");

            Assert.AreEqual("hello big world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_ReplaceMany()
        {
            StringManipulator m = new StringManipulator();
            m.Append("hello cruel world");
            m.Replace("l","L");

            Assert.AreEqual("heLLo crueL worLd", m.ToString());
        }
    }
}
