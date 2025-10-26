using CommonUtil.JSON.Interface;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void WriteToFile<T>(string filePath, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
