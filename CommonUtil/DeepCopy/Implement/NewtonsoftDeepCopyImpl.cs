using CommonUtil.DeepCopy.Interface;
using Newtonsoft.Json;
using System;

namespace CommonUtil.DeepCopy.Implement
{
    public class NewtonsoftDeepCopyImpl : IDeepCopy
    {
        /// <summary>
        /// 使用Newtonsoft.Json实现的深拷贝
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopy<T>(T obj)
        {
            if (obj == null) return default;

            try
            {
                // 配置序列化选项,序列化时包含类型信息
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

                string json = JsonConvert.SerializeObject(obj, settings);

                // 反序列化时使用相同的设置
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON 深拷贝出错: {ex.Message}");
                return default;
            }
        }
    }
}