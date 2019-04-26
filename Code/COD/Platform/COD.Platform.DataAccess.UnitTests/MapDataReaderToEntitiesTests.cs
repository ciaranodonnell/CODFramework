using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace COD.Platform.DataAccess.UnitTests
{
    [TestClass]
    public class MapDataReaderToEntitiesTests
    {


        class TheTestClass
        {
            public string TheString { get; set; }
            public double TheDouble{ get; set; }
            public DateTime TheDateTime { get; set; }
            public long TheLong { get; set; }
            public int TheInt { get; set; }
        }



        [TestMethod]
        public void TestBasicStringMap()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("TheString", typeof(string)));
            dt.Rows.Add("string value");


            var result = DataReaderExtensions.MapDataToEntities<TheTestClass>(new DataTableReader(dt));

            Assert.AreEqual(result.Count, 1, "Should have one result");

            Assert.AreEqual(result[0].TheString, "string value", "Should match the string value from the datatable");
        }


        [TestMethod]
        public void TestBasicIntMap()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("TheInt", typeof(int)));
            dt.Rows.Add(42);


            var result = DataReaderExtensions.MapDataToEntities<TheTestClass>(new DataTableReader(dt));

            Assert.AreEqual(result.Count, 1, "Should have one result");

            Assert.AreEqual(result[0].TheInt, 42, "Should match the int value from the datatable");
        }
    }
}
