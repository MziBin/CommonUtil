using CommonUtil.JSON.Interface;
using CommonUtil.JSON.Implement;

namespace CommonUtil.JSON
{
    /// <summary>
    /// JSON操作助手类，提供静态方法简化JSON操作
    /// </summary>
    public static class JsonHelper
    {
        private static readonly IJson _jsonHandler = new TextJSONImpl();

        /// <summary>
        /// 对象序列化为JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            return _jsonHandler.SerializeObject(obj);
        }

        /// <summary>
        /// JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
            return _jsonHandler.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 从 JSON 字符串中获取指定键的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueFromJson(string json, string key)
        {
            return _jsonHandler.GetValueFromJson(json, key);
        }
    }
}