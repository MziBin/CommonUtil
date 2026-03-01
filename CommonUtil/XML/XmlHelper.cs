using CommonUtil.XML.Interface;
using CommonUtil.XML.Implement;
using System;
using System.Collections.Generic;

namespace CommonUtil.XML
{
    /// <summary>
    /// XML操作助手类，提供静态方法简化XML操作
    /// </summary>
    public static class XmlHelper
    {
        private static readonly IXML _xmlHandler = new XMLHandlerToLINQImpl();

        /// <summary>
        /// 获取XML文件的根节点名称
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <returns></returns>
        public static string GetRootNodeName(string filePath)
        {
            return _xmlHandler.GetRootNodeName(filePath);
        }

        /// <summary>
        /// 获取指定节点的所有子节点名称列表
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <returns></returns>
        public static List<string> GetChildNodeNames(string filePath, string nodePath)
        {
            return _xmlHandler.GetChildNodeNames(filePath, nodePath);
        }

        /// <summary>
        /// 获取指定节点的属性值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static string GetNodeAttributeValue(string filePath, string nodePath, string attributeName)
        {
            return _xmlHandler.GetNodeAttributeValue(filePath, nodePath, attributeName);
        }

        /// <summary>
        /// 设置指定节点的属性值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetNodeAttributeValue(string filePath, string nodePath, string attributeName, string attributeValue)
        {
            _xmlHandler.SetNodeAttributeValue(filePath, nodePath, attributeName, attributeValue);
        }

        /// <summary>
        /// 获取指定节点的文本内容
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <returns></returns>
        public static string GetNodeText(string filePath, string nodePath)
        {
            return _xmlHandler.GetNodeText(filePath, nodePath);
        }

        /// <summary>
        /// 设置指定节点的文本内容
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        /// <param name="text">文本内容</param>
        public static void SetNodeText(string filePath, string nodePath, string text)
        {
            _xmlHandler.SetNodeText(filePath, nodePath, text);
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="parentNodePath">父节点XPath路径</param>
        /// <param name="childNodeName">子节点名称</param>
        public static void AddChildNode(string filePath, string parentNodePath, string childNodeName)
        {
            _xmlHandler.AddChildNode(filePath, parentNodePath, childNodeName);
        }

        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点XPath路径</param>
        public static void DeleteNode(string filePath, string nodePath)
        {
            _xmlHandler.DeleteNode(filePath, nodePath);
        }

        /// <summary>
        /// 将对象序列化为XML并保存到文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="obj">要序列化的对象</param>
        public static void SerializeObjectToXML<T>(string path, T obj)
        {
            _xmlHandler.SerializeObjectToXML(path, obj);
        }

        /// <summary>
        /// 从XML文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="path">XML文件路径</param>
        /// <returns></returns>
        public static T DeserializeObjectFromXML<T>(string path)
        {
            return _xmlHandler.DeserializeObjectFromXML<T>(path);
        }
    }
}