using NUnit.Framework;
using System.Collections.Generic;
using UtilityExtensions.Core;
using UtilityExtensions.Extensions;

namespace UtilityExtensions_Test
{
    public class MapTest
    {
        private class MapConvertionTest
        {
            public string stringTest { get; set; } = "test";
            public int intTest { get; set; } = 42;
            public decimal decimalTest { get; set; } = 12.34m;
            public NestedClass nestedClassTest { get; set; } = new NestedClass();
            public List<decimal> listTest { get; set; } = new List<decimal> { 1.2m, 32 };

            [MapIgnore]
            public int ignore { get; set; }
        }

        private class NestedClass
        {
            public string nestedStringTest { get; set; } = "test2";
            public int nestedIntTest { get; set; } = 21;
            public decimal nestedDecimalTest { get; set; } = 100.1m;
            public List<decimal> nestedListTest { get; set; } = new List<decimal> { 1.2m, 32 };
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(0)]
        public void ConvertObjectToMap()
        {
            var obj = new MapConvertionTest();
            var convertMap = obj.ToMap();
            var checkMap = new Map() {
                { "stringTest" , "test" },
                { "intTest" , 42 },
                { "decimalTest" , 12.34m },
                { "listTest" , new List<decimal> { 1.2m, 32 } },
                { "nestedClassTest" ,  new Map(){
                    {"nestedStringTest", "test2" },
                    {"nestedIntTest", 21 },
                    {"nestedDecimalTest", 100.1m },
                    {"nestedListTest", new List<decimal> { 1.2m, 32 }},
                } }
            };

            Assert.IsTrue(convertMap == checkMap);
        }

        [Test]
        public void ConvertMapToObject()
        {
            var convertMap = new Map() {
                { "stringTest" , null},
                { "intTest" , 1 },
                { "decimalTest" , 2m },
                { "nestedClassTest" ,  new Map(){
                    {"nestedStringTest", "test" },
                    {"nestedIntTest", 3 },
                    {"nestedDecimalTest", 4m },
                } }
            };
            var obj = convertMap.FromMap<MapConvertionTest>();
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj is MapConvertionTest);
            Assert.IsNull(obj.stringTest);
            Assert.IsTrue(obj.intTest == 1);
            Assert.IsTrue(obj.decimalTest == 2);
            Assert.IsNotNull(obj.nestedClassTest);
            Assert.IsTrue(obj.nestedClassTest.nestedStringTest == "test");
            Assert.IsTrue(obj.nestedClassTest.nestedIntTest == 3);
            Assert.IsTrue(obj.nestedClassTest.nestedDecimalTest == 4);
        }

        [Test]
        public void MapToString()
        {
            var map = new Map()
            {
                {"field1", 10 },
                {"field2", null },
                {"field3", 134.12 },
                {"field4", "test" },
                {"field5", new List<int>{1, 2} },
                {"field6", new Map(){
                    {"field1", 3 }
                } },
            };
            string checkString = "{\"field1\":10,\"field2\":null,\"field3\":134.12,\"field4\":\"test\",\"field5\":[1,2],\"field6\":{\"field1\":3}}";
            string mapString = map.ToString();

            Assert.IsTrue(checkString == mapString);
        }

        [Test]
        public void CompareMaps_DifferentFieldValue_ReturnsFalse()
        {
            var map1 = new Map()
            {
                {"field1", 10 },
                {"field2", null },
            };
            var map2 = new Map()
            {
                {"field1", 10 },
                {"field2", "test" },
            };

            Assert.IsFalse(map1 == map2);
        }

        [Test]
        public void CompareMaps_SameFieldValues_ReturnsTrue()
        {
            var map1 = new Map()
            {
                {"field1", 10 },
                {"field2", "test" },
            };
            var map2 = new Map()
            {
                {"field1", 10 },
                {"field2", "test" },
            };

            Assert.IsTrue(map1 == map2);
        }

        [Test]
        public void CompareMaps_DifferentFields_ReturnsFalse()
        {
            var map1 = new Map()
            {
                {"field1", 10 },
                {"field2", "test" },
            };
            var map2 = new Map()
            {
                {"field1", 10 },
                {"field3", "test" },
            };

            Assert.IsFalse(map1 == map2);
        }

        [Test]
        public void CompareMaps_MissingFields_ReturnsFalse()
        {
            var map1 = new Map()
            {
                {"field1", 10 },
            };
            var map2 = new Map()
            {
                {"field1", 10 },
                {"field2", "test" },
            };

            Assert.IsFalse(map1 == map2);
        }
    }
}