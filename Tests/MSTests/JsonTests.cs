using CommonUtil.JSON.Extensions;
using CommonUtil.JSON.Implement;
using CommonUtil.JSON.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MSTests
{
    [TestClass]
    public class JsonTests
    {
        private IJson _textJson;
        private IJson _newtonsoftJson;
        private string _tempJsonPath;

        [TestInitialize]
        public void TestInitialize()
        {
            _textJson = new TextJSONImpl();
            _newtonsoftJson = new NewtonsoftJSONImpl();
            _tempJsonPath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_tempJsonPath))
            {
                File.Delete(_tempJsonPath);
            }
        }

        [TestMethod]
        public void Serialize_And_Deserialize_TextJson_ReturnsOriginalObject()
        {
            var obj = new TestObject { Id = 1, Name = "Test" };
            string json = _textJson.SerializeObject(obj);
            var result = _textJson.DeserializeObject<TestObject>(json);

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
        }

        [TestMethod]
        public void Serialize_And_Deserialize_NewtonsoftJson_ReturnsOriginalObject()
        {
            var obj = new TestObject { Id = 1, Name = "Test" };
            string json = _newtonsoftJson.SerializeObject(obj);
            var result = _newtonsoftJson.DeserializeObject<TestObject>(json);

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
        }

        [TestMethod]
        public void WriteToFile_And_ReadFromFile_TextJson_ReturnsOriginalObject()
        {
            var obj = new TestObject { Id = 1, Name = "Test" };
            _textJson.WriteToFile(_tempJsonPath, obj);
            var result = _textJson.ReadFromFile<TestObject>(_tempJsonPath);

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
        }

        [TestMethod]
        public void WriteToFile_And_ReadFromFile_NewtonsoftJson_ReturnsOriginalObject()
        {
            var obj = new TestObject { Id = 1, Name = "Test" };
            _newtonsoftJson.WriteToFile(_tempJsonPath, obj);
            var result = _newtonsoftJson.ReadFromFile<TestObject>(_tempJsonPath);

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
        }

        [Serializable]
        public class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}