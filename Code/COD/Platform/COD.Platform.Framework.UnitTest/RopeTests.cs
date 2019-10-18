using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace COD.Platform.Framework.UnitTest
{
    [TestClass]
    public class RopeTests
    {
        [TestInitialize]
        public void TestInit()
        {
            //remove assembly loading from test timing
            Rope m = new Rope();

        }



        [TestMethod]
        public void Test_Rope_ConstructedWithString()
        {
            Rope m = new Rope("hello");

            Assert.AreEqual("hello", m.ToString());
        }



        [TestMethod]
        public void Test_Rope_RemoveStartLength_SingleAppend()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Remove(1, 2);
            Assert.AreEqual("hlo", m.ToString());
        }

        [TestMethod]
        public void Test_Rope_RemoveStartLength_AcrossTwoAppends()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Append(" world");
            m.Remove(1, 6);
            Assert.AreEqual("horld", m.ToString());
        }
        [TestMethod]
        public void Test_Rope_RemoveStartLength_BeginningOfSection()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Remove(0, 2);
            Assert.AreEqual("llo", m.ToString());
        }
        [TestMethod]
        public void Test_Rope_RemoveStartLength_EndOfSection()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Remove(3, 2);
            Assert.AreEqual("hel", m.ToString());
        }



        [TestMethod]
        public void Test_StringManipulator_AddString()
        {
            Rope m = new Rope();
            m.Append("hello");

            Assert.AreEqual("hello", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_AddTwoStrings()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Append(" world");

            Assert.AreEqual("hello world", m.ToString());
        }
        [TestMethod]
        public void Test_StringManipulator_AddThreeStrings()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Append(" cruel");
            m.Append(" world");

            Assert.AreEqual("hello cruel world", m.ToString());
            Assert.AreEqual("hello cruel world".Length, m.Length);
        }



        [TestMethod]
        public void Test_StringManipulator_InsertString()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Append("  world");
            m.Insert("cruel", 6);
            Assert.AreEqual("hello cruel world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeFirstSection()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("world");
            m.Remove("hello ");

            Assert.AreEqual("world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeMidSection()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Remove("cruel ");

            Assert.AreEqual("hello world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeLastSection()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Remove("world");

            Assert.AreEqual("hello cruel ", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeLastSectionAndReplace()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Replace("world", "suzy");

            Assert.AreEqual("hello cruel suzy", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeFirstSectionAndReplace()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("world");
            m.Replace("hello ", "bonjour ");

            Assert.AreEqual("bonjour world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveWholeMidSectionAndReplace()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append("cruel ");
            m.Append("world");
            m.Replace("cruel ", "big ");

            Assert.AreEqual("hello big world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveMidTextAndReplace()
        {
            Rope m = new Rope();
            m.Append("hello");
            m.Append(" cruel ");
            m.Append("world");
            m.Replace(" cruel ", " big ");

            Assert.AreEqual("hello big world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_InsertStringInBetweenSections()
        {
            Rope m = new Rope();
            m.Append("hello ");
            m.Append(" world");
            m.Insert("cruel", 6);
            Assert.AreEqual("hello cruel world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossTwoSections()
        {
            Rope m = new Rope();
            m.Append("hello cr");
            m.Append("uel world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringInsideSection()
        {
            Rope m = new Rope();
            m.Append("hello cruel world");
            m.Remove("cruel");

            Assert.AreEqual("hello  world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_RemoveTwoStringsInsideSection()
        {
            Rope m = new Rope();
            m.Append("hello cruel world");
            m.Remove("l");

            Assert.AreEqual("heo crue word", m.ToString());
        }

        [TestMethod]
        public void Test_StringManipulator_RemoveSingleStringAcrossThreeSections()
        {
            Rope m = new Rope();
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
            Rope m = new Rope();
            m.Append("hello cruel world");
           Assert.AreEqual("cruel", m.Substring(6, 5));
        }

        [TestMethod]
        public void Test_StringManipulator_SubstringMultipleSection()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("cruel", m.Substring(6, 5));
        }


        [TestMethod]
        public void Test_StringManipulator_IndexOfSingleSection()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello".IndexOf('l'), m.IndexOf("l"));
        }



        [TestMethod]
        public void Test_StringManipulator_IndexOfSingleLetter()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello".IndexOf('l'), m.IndexOf("l"));
        }

        [TestMethod]
        public void Test_StringManipulator_IndexOfString()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("cru"), m.IndexOf("cru"));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringAcrossSections()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("uel"), m.IndexOf("uel"));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringInSecondSections()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("rld"), m.IndexOf("rld"));
        }


        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndex()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("l", 3), m.IndexOf("l", 3));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndexInLaterSection()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("l", 7), m.IndexOf("l", 7));
        }
        [TestMethod]
        public void Test_StringManipulator_IndexOfStringStartIndexInLaterSectionNotFound()
        {
            Rope m = new Rope();
            m.Append("hello cru");
            m.Append("el world");

            Assert.AreEqual("hello cruel world".IndexOf("k", 7), m.IndexOf("k", 7));
        }

        [TestMethod]
        public void Test_StringManipulator_ReplaceSingle()
        {
            Rope m = new Rope();
            m.Append("hello cruel world");
            m.Replace("cruel", "big");

            Assert.AreEqual("hello big world", m.ToString());
        }


        [TestMethod]
        public void Test_StringManipulator_ReplaceMany()
        {
            Rope m = new Rope();
            m.Append("hello cruel world");
            m.Replace("l", "L");

            Assert.AreEqual("heLLo crueL worLd", m.ToString());
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

            var sb = new Rope();
            for (int x = 0; x < 100; x++)
            {
                sb.Append("Hello ");
            }

            for (int x = 0; x < 100; x++)
            {
                sb.Insert("World", 50);
            }

            for (int x = 0; x < 100; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Assert.IsFalse(true, $"{sw.ElapsedMilliseconds}");

        }

        [TestMethod]
        public void TestTimingSMDebug()
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sb = new Rope();
            for (int x = 0; x < 4; x++)
            {
                sb.Append("Hello ");
            }

            Assert.AreEqual("Hello Hello Hello Hello ", sb.ToString());

            for (int x = 0; x < 2; x++)
            {
                sb.Insert("World", 10);
            }

            Assert.AreEqual("Hello HellWorldWorldo Hello Hello ", sb.ToString());


            for (int x = 0; x < 2; x++)
            {
                sb.Replace("Hello ", "Hey ");
            }
            sw.Stop();

            Assert.AreEqual("Hey HellWorldWorldo Hey Hey ", sb.ToString());

          //  Assert.IsFalse(true, $"{sw.ElapsedMilliseconds}");

        }


    }
}
