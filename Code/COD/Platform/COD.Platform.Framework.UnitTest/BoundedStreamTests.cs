using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace COD.Platform.Framework.UnitTest
{
    [TestClass]
    public class BoundedStreamTests
    {


        [TestMethod]
        public void TestReadingWorks()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 5, 3);

            var b = bs.ReadByte();
            Assert.AreEqual(5, b);

        }


        [TestMethod]
        public void TestSeekFromBegining()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Seek(2, SeekOrigin.Begin);

            var b = bs.ReadByte();
            Assert.AreEqual(3, b);

        }


        [TestMethod]
        public void TestSeekFromEnd()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Seek(-2, SeekOrigin.End);

            var b = bs.ReadByte();
            Assert.AreEqual(5, b);

        }

        [TestMethod]
        public void TestSeekForwardFromCurrent()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Position = 3;
            bs.Seek(1, SeekOrigin.Current);

            var b = bs.ReadByte();
            Assert.AreEqual(5, b);

        }


        [TestMethod]
        public void TestSeekBackFromCurrent()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Position = 3;
            bs.Seek(-1, SeekOrigin.Current);

            var b = bs.ReadByte();
            Assert.AreEqual(3, b);

        }



        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSeekBackFromBegin()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Seek(-1, SeekOrigin.Begin);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSeekForwardFromEnd()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);

            bs.Seek(1, SeekOrigin.End);

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSeekTooFarBackFromCurrent()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 7);
            bs.Position = 4;
            bs.Seek(-5, SeekOrigin.Current);

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSeekTooFarForwardFromCurrent()
        {
            var ms = GetPopulatedMemoryStream(10);

            var bs = new BoundedStream(ms, 1, 6);
            bs.Position = 3;
            bs.Seek(5, SeekOrigin.Current);

        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitialLengthValidation()
        {
            var ms = new MemoryStream();
            var bs = new BoundedStream(ms, 5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitialStartValidation()
        {
            var ms = new MemoryStream();
            var bs = new BoundedStream(ms, 0, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInitialBoundsValidation_InnerStreamNotNull()
        {
            var bs = new BoundedStream(null, 7, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitialBoundsValidation_StartIsInStream()
        {
            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 15, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInitialBoundsValidation_EndIsInStream()
        {
            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 0, 100);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSetPositionBeforeStartOfBoundedStream()
        {
            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 2, 5);
            bs.Position = -1;
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSetPositionBeforeStartOfInnerStream()
        {
            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 2, 5);
            bs.Position = -4;
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSetPositionAfterEndOfBoundedStream()
        {

            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 2, 5);
            bs.Position = 6;
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSetPositionAfterEndOfInnerStream()
        {
            var ms = GetPopulatedMemoryStream(10);
            var bs = new BoundedStream(ms, 2, 5);
            bs.Position = 20;
        }




        private static MemoryStream GetPopulatedMemoryStream(int length)
        {
            var ms = new MemoryStream();
            for (int x = 0; x < length; x++)
            {
                ms.WriteByte((byte)x);
            }
            ms.Position = 0;
            return ms;
        }



    }
}

