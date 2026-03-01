using CommonUtil.XML.Implement;
using CommonUtil.XML.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSTests
{
    [TestClass]
    public class XmlTests
    {
        private IXML _xml;
        private string _tempXmlPath;

        [TestInitialize]
        public void TestInitialize()
        {
            _xml = new XMLHandlerToLINQImpl();
            _tempXmlPath = Path.GetTempFileName() + ".xml";
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_tempXmlPath))
            {
                File.Delete(_tempXmlPath);
            }
        }

        [TestMethod]
        public void SerializeObjectToXml_And_DeserializeObjectFromXml_ReturnsOriginalObject()
        {
            // 准备：创建测试对象
            var testObj = new TestObject { Id = 1, Name = "Test" };

            // 执行：序列化对象到XML文件
            _xml.SerializeObjectToXML(_tempXmlPath, testObj);

            // 执行：从XML文件反序列化对象
            var resultObj = _xml.DeserializeObjectFromXML<TestObject>(_tempXmlPath);

            // 断言：反序列化后的对象应与原始对象相同
            Assert.AreEqual(testObj.Id, resultObj.Id, "反序列化后的ID与原始ID不一致");
            Assert.AreEqual(testObj.Name, resultObj.Name, "反序列化后的名称与原始名称不一致");
        }

        [TestMethod]
        public void GetRootNodeName_ReturnsCorrectName()
        {
            // 准备：创建测试对象并序列化到XML文件
            var testObj = new TestObject { Id = 1, Name = "Test" };
            _xml.SerializeObjectToXML(_tempXmlPath, testObj);

            // 执行：获取根节点名称
            string rootNodeName = _xml.GetRootNodeName(_tempXmlPath);

            // 断言：根节点名称应与对象类型名称一致
            Assert.AreEqual(typeof(TestObject).Name, rootNodeName, "根节点名称不正确");
        }

        [TestMethod]
        public void GetChildNodeNames_ReturnsExpectedNames()
        {
            // 准备：创建测试对象并序列化到XML文件
            var testObj = new TestObject { Id = 1, Name = "Test" };
            _xml.SerializeObjectToXML(_tempXmlPath, testObj);

            // 执行：获取子节点名称列表
            List<string> childNodeNames = _xml.GetChildNodeNames(_tempXmlPath, $"/{typeof(TestObject).Name}");

            // 断言：子节点名称列表应包含Id和Name
            Assert.IsTrue(childNodeNames.Contains("Id"), "子节点名称列表应包含Id");
            Assert.IsTrue(childNodeNames.Contains("Name"), "子节点名称列表应包含Name");
        }

        [TestMethod]
        public void GetNodeAttributeValue_ReturnsExpectedValue()
        {
            // 准备：创建包含属性的XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root Attribute1=""Value1"" Attribute2=""Value2"">
  <ChildNode Attribute3=""Value3"">Content</ChildNode>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：获取根节点的Attribute1属性值
            string attributeValue = _xml.GetNodeAttributeValue(_tempXmlPath, "/Root", "Attribute1");

            // 断言：属性值应为Value1
            Assert.AreEqual("Value1", attributeValue, "根节点Attribute1属性值不正确");
        }

        [TestMethod]
        public void SetNodeAttributeValue_UpdatesAttribute()
        {
            // 准备：创建包含属性的XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root Attribute1=""Value1"">
  <ChildNode>Content</ChildNode>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：设置根节点的Attribute1属性值
            _xml.SetNodeAttributeValue(_tempXmlPath, "/Root", "Attribute1", "UpdatedValue");

            // 执行：再次获取根节点的Attribute1属性值
            string updatedAttributeValue = _xml.GetNodeAttributeValue(_tempXmlPath, "/Root", "Attribute1");

            // 断言：属性值应已更新为UpdatedValue
            Assert.AreEqual("UpdatedValue", updatedAttributeValue, "根节点Attribute1属性值未更新");
        }

        [TestMethod]
        public void GetNodeText_ReturnsExpectedText()
        {
            // 准备：创建包含文本内容的XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ChildNode>TestContent</ChildNode>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：获取ChildNode的文本内容
            string nodeText = _xml.GetNodeText(_tempXmlPath, "/Root/ChildNode");

            // 断言：文本内容应为TestContent
            Assert.AreEqual("TestContent", nodeText, "ChildNode文本内容不正确");
        }

        [TestMethod]
        public void SetNodeText_UpdatesText()
        {
            // 准备：创建包含文本内容的XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ChildNode>OriginalContent</ChildNode>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：设置ChildNode的文本内容
            _xml.SetNodeText(_tempXmlPath, "/Root/ChildNode", "UpdatedContent");

            // 执行：再次获取ChildNode的文本内容
            string updatedNodeText = _xml.GetNodeText(_tempXmlPath, "/Root/ChildNode");

            // 断言：文本内容应已更新为UpdatedContent
            Assert.AreEqual("UpdatedContent", updatedNodeText, "ChildNode文本内容未更新");
        }

        [TestMethod]
        public void AddChildNode_AddsNewNode()
        {
            // 准备：创建XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ExistingChild>Content</ExistingChild>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：添加新的子节点
            _xml.AddChildNode(_tempXmlPath, "/Root", "NewChild");

            // 执行：获取子节点名称列表
            List<string> childNodeNames = _xml.GetChildNodeNames(_tempXmlPath, "/Root");

            // 断言：子节点名称列表应包含NewChild
            Assert.IsTrue(childNodeNames.Contains("NewChild"), "新子节点未添加成功");
        }

        [TestMethod]
        public void DeleteNode_RemovesNode()
        {
            // 准备：创建XML文件
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ChildNode1>Content1</ChildNode1>
  <ChildNode2>Content2</ChildNode2>
</Root>";
            File.WriteAllText(_tempXmlPath, xmlContent);

            // 执行：删除ChildNode1
            _xml.DeleteNode(_tempXmlPath, "/Root/ChildNode1");

            // 执行：获取子节点名称列表
            List<string> childNodeNames = _xml.GetChildNodeNames(_tempXmlPath, "/Root");

            // 断言：子节点名称列表不应包含ChildNode1，应包含ChildNode2
            Assert.IsFalse(childNodeNames.Contains("ChildNode1"), "ChildNode1未删除成功");
            Assert.IsTrue(childNodeNames.Contains("ChildNode2"), "ChildNode2应存在");
        }

        [Serializable]
        public class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}