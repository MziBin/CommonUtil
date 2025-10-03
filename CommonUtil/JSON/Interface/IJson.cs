using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.JSON.Interface
{
    //这个用text.json和Newtonsoft.Json实现

    public interface IJson
    {
        /// <summary>
        /// 对象序列化为JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string SerializeObject<T>(T obj);

        /// <summary>
        /// JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T DeserializeObject<T>(string json);

        /// <summary>
        /// 从 JSON 文件中读取数据并反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        T ReadFromFile<T>(string filePath);

        /// <summary>
        /// 将对象序列化为 JSON 字符串并写入到指定文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        void WriteToFile<T>(string filePath, T obj);

        /// <summary>
        /// 从 JSON 字符串中获取指定键的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValueFromJson(string json, string key);

        /// <summary>
        /// 从 JSON 文件中读取数据，反序列化为字典对象，并根据指定的键获取对应的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        T ReadFromFileAndKey<T>(string filePath, string key);
    }
}
