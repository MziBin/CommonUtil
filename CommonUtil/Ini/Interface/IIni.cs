using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.Ini.Interface
{
    public interface IIni
    {

        /// <summary>
        /// 获取或设置INI文件的路径。
        /// </summary>
        String FilePath { get; set; }
       
        /// <summary>
        /// 获取指定section中的指定key的值。如果section或key不存在，则返回默认值defaultValue。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string ReadValue(string section, string key, string defaultValue = "");
        /// <summary>
        /// 设置指定section中的指定key的值。如果section或key不存在，则创建它们。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void WriteValue(string section, string key, string value);

        /// <summary>
        /// 获取INI文件中指定section中的所有key的名称列表。如果section不存在，则返回一个空列表。
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        List<string> ReadKeys(string SectionName);

        /// <summary>
        /// 读取指定section中的所有key及其对应的值，返回一个字典对象。如果section不存在，则返回一个空字典。
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        Dictionary<string, string> ReadSection(string section);

        /// <summary>
        /// 删除指定section中的指定key。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        void DeleteKey(string section, string key);
        /// <summary>
        /// 删除指定的section以及其包含的所有key。
        /// </summary>
        /// <param name="section"></param>
        void DeleteSection(string section);
        /// <summary>
        /// 确定指定的section是否存在。
        /// </summary>
        /// <param name="section">查询的名称，不能为空</param>
        /// <returns>true表示存在，否则false</returns>
        bool HasSection(string section);
        /// <summary>
        /// 确定指定的section是否包含给定的key。
        /// </summary>
        /// <param name="section">要查询的部分，不能为空</param>
        /// <param name="key">要查询的key，不能为空</param>
        /// <returns>true 表示有指定的key，否则false</returns>
        bool HasKey(string section, string key);
    }
}
