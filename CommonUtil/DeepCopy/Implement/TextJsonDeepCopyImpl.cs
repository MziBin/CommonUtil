using CommonUtil.DeepCopy.Interface;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonUtil.DeepCopy.Implement
{
    public class TextJsonDeepCopyImpl : IDeepCopy
    {
        /// <summary>
        /// 通过JSON实现深拷贝，两个对象互不影响，支持包含复杂类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要拷贝的对象</param>
        /// <returns>拷贝后的新对象</returns>
        public T DeepCopy<T>(T obj)
        {
            if (obj == null)
            {
                // 如果输入对象为null，直接返回默认值
                return default(T);
            }
            // 配置序列化选项，支持复杂类型和深层嵌套
            var options = new JsonSerializerOptions
            {
                // 处理循环引用
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                // 允许深层嵌套的复杂类型
                MaxDepth = 64,
                // 保留字段名称大小写
                PropertyNameCaseInsensitive = false,
                // 包含字段
                IncludeFields = true,
                // 不格式化输出，提高性能
                WriteIndented = false
            };

            // 使用 System.Text.Json 进行序列化和反序列化
            var jsonString = JsonSerializer.Serialize(obj, options);
            return JsonSerializer.Deserialize<T>(jsonString, options);
        }
    }
}