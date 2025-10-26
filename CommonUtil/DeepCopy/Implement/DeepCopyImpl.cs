using CommonUtil.DeepCopy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonUtil.DeepCopy.Implement
{
    public class DeepCopyImpl : IDeepCopy
    {
        /// <summary>
        /// 通过JSON实现深拷贝，两个对象互不影响
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopyObjectByTextJson<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            // 使用 System.Text.Json 进行序列化和反序列化
            var jsonString = System.Text.Json.JsonSerializer.Serialize(obj);
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// 使用Newtonsoft.Json实现的深拷贝
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopyObjectByNewtonsoftJson<T>(T obj)
        {
            if (obj == null) return default;

            try
            {
                // 序列化时包含类型信息
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

        /// <summary>
        /// 通过二进制序列化实现深拷贝，两个对象互不影响
        /// 需要对象标记为[Serializable]，并且不能有非序列化的字段。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopyObjectByBinary<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }


    }
}
