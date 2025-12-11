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
        /// 从 JSON 字符串中获取指定键的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValueFromJson(string json, string key);
    }
}
