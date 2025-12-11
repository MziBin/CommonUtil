using CommonUtil.JSON.Interface;
using System;
using System.IO;

namespace CommonUtil.JSON.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 从 JSON 文件中读取数据并反序列化为指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonSerializer"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadFromFile<T>(this IJson jsonSerializer, string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("JSON file not found.", filePath);
            }
            string json = File.ReadAllText(filePath);
            return jsonSerializer.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将对象序列化为 JSON 字符串并写入到指定文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonSerializer"></param>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        public static void WriteToFile<T>(this IJson jsonSerializer, string filePath, T obj)
        {
            string json = jsonSerializer.SerializeObject(obj);
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(filePath, json);
        }
    }
}