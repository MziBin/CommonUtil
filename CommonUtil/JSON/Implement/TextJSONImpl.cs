using CommonUtil.JSON.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonUtil.JSON.Implement
{

    [Serializable]
    public class TextJSONImpl : IJson
    {
        public T DeserializeObject<T>(string json)
        {
            try
            {
                //如果对象里面的属性包含复杂类型，确保这些类型也是可序列化的
                // 配置序列化选项，支持复杂类型和深层嵌套
                var options = new JsonSerializerOptions
                {
                    // 处理循环引用（如A包含B，B包含A的场景），避免序列化失败
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    // 允许深层嵌套的复杂类型（默认最大深度为64，可根据需求调整）
                    MaxDepth = 64,
                    // 保留字段名称大小写（默认会将PascalCase转为camelCase，如需保持原样需设置）
                    PropertyNameCaseInsensitive = false,
                    // 包含字段（默认只序列化属性，若复杂类型用字段存储数据需开启）
                    IncludeFields = true,
                    // 忽略循环引用时不抛异常（仅记录警告）
                    WriteIndented = false // 不格式化输出，提高性能
                };

                return JsonSerializer.Deserialize<T>(json, options);
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
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty(key, out JsonElement element))
                    {
                        return element.ToString();
                    }
                    return null;
                }
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
                //如果对象里面的属性包含复杂类型，确保这些类型也是可序列化的
                // 配置序列化选项，支持复杂类型和深层嵌套
                var options = new JsonSerializerOptions
                {
                    // 处理循环引用（如A包含B，B包含A的场景），避免序列化失败
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    // 允许深层嵌套的复杂类型（默认最大深度为64，可根据需求调整）
                    MaxDepth = 64,
                    // 保留字段名称大小写（默认会将PascalCase转为camelCase，如需保持原样需设置）
                    PropertyNameCaseInsensitive = false,
                    // 包含字段（默认只序列化属性，若复杂类型用字段存储数据需开启）
                    IncludeFields = true,
                    // 忽略循环引用时不抛异常（仅记录警告）
                    WriteIndented = false // 不格式化输出，提高性能
                };

                return JsonSerializer.Serialize(obj, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"序列化对象时发生错误: {ex.Message}");
                return null;
            }
        }

    }
}
