using CommonUtil.JSON.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonUtil.JSON.Implement
{

    [Serializable]
    public class TextJSON : IJson
    {
        public T DeserializeObject<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"反序列化 JSON 字符串时发生错误: {ex.Message}");
                return default(T);
            }
        }

        public string GetValueFromJson(string json, string key)
        {
            throw new NotImplementedException();
        }

        public T ReadFromFile<T>(string filePath)
        {
            throw new NotImplementedException();
        }

        public T ReadFromFileAndKey<T>(string filePath, string key)
        {
            throw new NotImplementedException();
        }

        public string SerializeObject<T>(T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"序列化对象时发生错误: {ex.Message}");
                return null;
            }
        }

        public void WriteToFile<T>(string filePath, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
