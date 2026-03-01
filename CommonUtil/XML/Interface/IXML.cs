using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.XML.Interface
{
    public interface IXML
    {
        /// <summary>
        /// 获取XML文件的根节点名称
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <returns></returns>
        string GetRootNodeName(string filePath);
        /// <summary>
        /// 获取指定节点的所有子节点名称列表
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <returns></returns>
        List<string> GetChildNodeNames(string filePath, string nodePath);

        /// <summary>
        /// 获取指定节点的属性值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        string GetNodeAttributeValue(string filePath, string nodePath, string attributeName);
        /// <summary>
        /// 设置指定节点的属性值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        void SetNodeAttributeValue(string filePath, string nodePath, string attributeName, string attributeValue);

        /// <summary>
        /// 获取指定节点的文本内容
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <returns></returns>
        string GetNodeText(string filePath, string nodePath);
        /// <summary>
        /// 设置指定节点的文本内容
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="text">文本内容</param>
        void SetNodeText(string filePath, string nodePath, string text);

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="parentNodePath">父节点XPath路径</param>
        /// <param name="childNodeName">子节点名称</param>
        void AddChildNode(string filePath, string parentNodePath, string childNodeName);
        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        void DeleteNode(string filePath, string nodePath);

        /// <summary>
        /// 将对象序列化为XML并保存到文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="obj">要序列化的对象</param>
        void SerializeObjectToXML<T>(string path, T obj);
        /// <summary>
        /// 从XML文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="path">XML文件路径</param>
        /// <returns></returns>
        T DeserializeObjectFromXML<T>(string path);
    }
}
