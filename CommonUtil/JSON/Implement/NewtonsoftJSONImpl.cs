using CommonUtil.JSON.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.JSON.Implement
{
    [Serializable]
    public class NewtonsoftJSONImpl : IJson
    {
        public T DeserializeObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"反序列化 JSON 字符串时发生错误: {ex.Message}");
                return default(T);
            }
        }

        public string GetValueFromJson(string json, string key)
        {
            try
            {
                JObject jsonObject = JObject.Parse(json);
                JToken token = jsonObject.SelectToken(key);
                return token?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"从 JSON 获取值时发生错误: {ex.Message}");
                return null;
            }
        }

        public string SerializeObject<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"序列化对象时发生错误: {ex.Message}");
                return null;
            }
        }

    }
}
