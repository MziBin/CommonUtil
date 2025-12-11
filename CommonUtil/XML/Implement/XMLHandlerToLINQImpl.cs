using CommonUtil.XML.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace CommonUtil.XML.Implement
{
    public class XMLHandlerToLINQImpl : IXML
    {
        private string _filePath;

        /// <summary>
        /// 获取或设置XML文件路径
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set
            {
                // 验证路径有效性
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("XML文件路径不能为空");
                _filePath = value;
            }
        }

        /// <summary>
        /// 构造函数（可选：指定初始文件路径）
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        public XMLHandlerToLINQImpl(string filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath))
                FilePath = filePath;
        }

        /// <summary>
        /// 获取XML文件的根节点名称
        /// </summary>
        /// <returns>根节点名称（如无文件则抛出异常）</returns>
        public string GetRootNodeName()
        {
            EnsureFileExists();
            XDocument doc = XDocument.Load(FilePath);
            
            return doc.Root?.Name.LocalName ?? throw new InvalidOperationException("XML文件没有根节点");
            
        }

        /// <summary>
        /// 获取指定节点的所有子节点名称列表
        /// </summary>
        /// <param name="nodePath">节点XPath路径（如"/book/chapters"）</param>
        /// <returns>子节点名称列表（如无节点则返回空列表）</returns>
        public List<string> GetChildNodeNames(string nodePath)
        {
            if (string.IsNullOrWhiteSpace(nodePath))
                throw new ArgumentException("节点路径不能为空");

            EnsureFileExists();
            var childNames = new List<string>();

            var doc = XDocument.Load(FilePath);
            
            var targetNode = doc.XPathSelectElement(nodePath);
            if (targetNode == null)
                return childNames; // 节点不存在，返回空列表

            foreach (var child in targetNode.Elements())
            {
                childNames.Add(child.Name.LocalName);
            }
            
            return childNames;
        }

        /// <summary>
        /// 获取指定节点的属性值
        /// </summary>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns>属性值（如节点或属性不存在则返回null）</returns>
        public string GetNodeAttributeValue(string nodePath, string attributeName)
        {
            if (string.IsNullOrWhiteSpace(nodePath) || string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("节点路径和属性名称不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            
            var targetNode = doc.XPathSelectElement(nodePath);
            return targetNode?.Attribute(attributeName)?.Value;
            
        }

        /// <summary>
        /// 设置指定节点的属性值（如属性不存在则新增）
        /// </summary>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public void SetNodeAttributeValue(string nodePath, string attributeName, string attributeValue)
        {
            if (string.IsNullOrWhiteSpace(nodePath) || string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("节点路径和属性名称不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            var targetNode = doc.XPathSelectElement(nodePath) ?? throw new KeyNotFoundException($"未找到路径为{nodePath}的节点");

            // 存在则修改，不存在则新增属性
            var attribute = targetNode.Attribute(attributeName);
            if (attribute != null)
                attribute.Value = attributeValue;
            else
                targetNode.Add(new XAttribute(attributeName, attributeValue));

            doc.Save(FilePath);
        }

        /// <summary>
        /// 获取指定节点的文本内容
        /// </summary>
        /// <param name="nodePath">节点XPath路径</param>
        /// <returns>节点文本（如节点不存在则返回null）</returns>
        public string GetNodeText(string nodePath)
        {
            if (string.IsNullOrWhiteSpace(nodePath))
                throw new ArgumentException("节点路径不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            
            var targetNode = doc.XPathSelectElement(nodePath);
            return targetNode?.Value;
            
        }

        /// <summary>
        /// 设置指定节点的文本内容
        /// </summary>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="text">要设置的文本</param>
        public void SetNodeText(string nodePath, string text)
        {
            if (string.IsNullOrWhiteSpace(nodePath))
                throw new ArgumentException("节点路径不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            var targetNode = doc.XPathSelectElement(nodePath) ?? throw new KeyNotFoundException($"未找到路径为{nodePath}的节点");

            targetNode.Value = text; // 覆盖节点文本
            doc.Save(FilePath);
        }

        /// <summary>
        /// 向指定父节点添加子节点
        /// </summary>
        /// <param name="parentNodePath">父节点XPath路径</param>
        /// <param name="childNodeName">子节点名称</param>
        public void AddChildNode(string parentNodePath, string childNodeName)
        {
            if (string.IsNullOrWhiteSpace(parentNodePath) || string.IsNullOrWhiteSpace(childNodeName))
                throw new ArgumentException("父节点路径和子节点名称不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            var parentNode = doc.XPathSelectElement(parentNodePath) ?? throw new KeyNotFoundException($"未找到父节点路径{parentNodePath}");

            // 添加新子节点（空内容）
            parentNode.Add(new XElement(childNodeName));
            doc.Save(FilePath);
        }

        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="nodePath">节点XPath路径</param>
        public void DeleteNode(string nodePath)
        {
            if (string.IsNullOrWhiteSpace(nodePath))
                throw new ArgumentException("节点路径不能为空");

            EnsureFileExists();
            var doc = XDocument.Load(FilePath);
            var targetNode = doc.XPathSelectElement(nodePath) ?? throw new KeyNotFoundException($"未找到路径为{nodePath}的节点");

            targetNode.Remove(); // 删除节点及其所有子节点
            doc.Save(FilePath);
        }

        /// <summary>
        /// 将对象序列化为XML并保存到文件
        /// </summary>
        /// <typeparam name="T">对象类型（需支持XML序列化）</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="obj">要序列化的对象</param>
        public void SerializeObjectToXML<T>(string path, T obj)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("保存路径不能为空");
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "序列化对象不能为null");

            // 确保目录存在
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, obj);
            }

            // 同步更新FilePath
            FilePath = path;
        }

        /// <summary>
        /// 从XML文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="path">XML文件路径</param>
        /// <returns>反序列化后的对象</returns>
        public T DeserializeObjectFromXML<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("文件路径不能为空");
            if (!File.Exists(path))
                throw new FileNotFoundException("XML文件不存在", path);

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(path))
            {
                var result = serializer.Deserialize(reader);
                return (T)result;
            }
        }

        /// <summary>
        /// 确保XML文件存在（如不存在则创建空XML，根节点为"Root"）
        /// </summary>
        private void EnsureFileExists()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new InvalidOperationException("请先设置XML文件路径（FilePath）");

            if (!File.Exists(FilePath))
            {
                // 创建目录（如果需要）
                var dir = Path.GetDirectoryName(FilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // 创建空XML（默认根节点为"Root"）
                var doc = new XDocument(new XElement("Root"));
                doc.Save(FilePath);
            }
        }
    }
}
