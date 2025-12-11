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
        /// 获取或设置XML文件的路径。
        /// </summary>
        String FilePath { get; set; }

        //获取XML文件的根节点名称
        string GetRootNodeName();
        //获取指定节点的所有子节点名称列表
        List<string> GetChildNodeNames(string nodePath);

        //获取指定节点的属性值
        string GetNodeAttributeValue(string nodePath, string attributeName);
        //设置指定节点的属性值
        void SetNodeAttributeValue(string nodePath, string attributeName, string attributeValue);

        //获取指定节点的文本内容
        string GetNodeText(string nodePath);
        //设置指定节点的文本内容
        void SetNodeText(string nodePath, string text);

        //添加子节点
        void AddChildNode(string parentNodePath, string childNodeName);
        //删除指定节点
        void DeleteNode(string nodePath);

        //将对象序列化为XML并保存到文件
        void SerializeObjectToXML<T>(string path, T obj);
        //从XML文件反序列化为对象
        T DeserializeObjectFromXML<T>(string path);

    }
}
