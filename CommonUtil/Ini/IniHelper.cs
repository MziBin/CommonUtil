using CommonUtil.Ini.Interface;
using CommonUtil.Ini.Implement;
using System.Collections.Generic;

namespace CommonUtil.Ini
{
    /// <summary>
    /// INI文件操作助手类，提供静态方法简化INI文件操作
    /// </summary>
    public static class IniHelper
    {
        private static readonly IIni _iniHandler = new IniImpl();

        /// <summary>
        /// 获取指定section中的指定key的值。如果section或key不存在，则返回默认值defaultValue。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ReadValue(string filePath, string section, string key, string defaultValue = "")
        {
            return _iniHandler.ReadValue(filePath, section, key, defaultValue);
        }

        /// <summary>
        /// 设置指定section中的指定key的值。如果section或key不存在，则创建它们。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteValue(string filePath, string section, string key, string value)
        {
            _iniHandler.WriteValue(filePath, section, key, value);
        }

        /// <summary>
        /// 获取INI文件中指定section中的所有key的名称列表。如果section不存在，则返回一个空列表。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public static List<string> ReadKeys(string filePath, string SectionName)
        {
            return _iniHandler.ReadKeys(filePath, SectionName);
        }

        /// <summary>
        /// 读取指定section中的所有key及其对应的值，返回一个字典对象。如果section不存在，则返回一个空字典。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadSection(string filePath, string section)
        {
            return _iniHandler.ReadSection(filePath, section);
        }

        /// <summary>
        /// 删除指定section中的指定key。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        public static void DeleteKey(string filePath, string section, string key)
        {
            _iniHandler.DeleteKey(filePath, section, key);
        }

        /// <summary>
        /// 删除指定的section以及其包含的所有key。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section"></param>
        public static void DeleteSection(string filePath, string section)
        {
            _iniHandler.DeleteSection(filePath, section);
        }

        /// <summary>
        /// 确定指定的section是否存在。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section">查询的名称，不能为空</param>
        /// <returns>true表示存在，否则false</returns>
        public static bool HasSection(string filePath, string section)
        {
            return _iniHandler.HasSection(filePath, section);
        }

        /// <summary>
        /// 确定指定的section是否包含给定的key。
        /// </summary>
        /// <param name="filePath">INI文件路径</param>
        /// <param name="section">要查询的部分，不能为空</param>
        /// <param name="key">要查询的key，不能为空</param>
        /// <returns>true 表示有指定的key，否则false</returns>
        public static bool HasKey(string filePath, string section, string key)
        {
            return _iniHandler.HasKey(filePath, section, key);
        }
    }
}