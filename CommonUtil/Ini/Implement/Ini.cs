using CommonUtil.Ini.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.Ini.Implement
{
    public class Ini : IIni
    {
        #region API引入
        //GetPrivateProfileString这个方法是在外部别人用c++写好的，不是通过.net编写的，我们只需要声明，我们可以通过DllImport引用，告诉这个方法是在kernel32.dll中
        // 声明 Windows API 函数，用于读取 INI 文件
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // 声明 Windows API 函数，用于写入 INI 文件
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, Byte[] retVal, int size, string filePath);

        #endregion

        #region 字段

        private string filePath;

        #endregion

        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        /// <summary>
        /// 读取指定section中的指定key的值。如果section或key不存在，则返回默认值defaultValue。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string ReadValue(string section, string key, string defaultValue = "")
        {
            StringBuilder temp = new StringBuilder(255);
            int result = GetPrivateProfileString(section, key, defaultValue, temp, 255, filePath);
            return temp.ToString();
        }

        /// <summary>
        /// 写入指定section中的指定key的值。如果section或key不存在，则创建它们。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
        }

        /// <summary>
        /// 读取INI文件中指定section中的所有key的名称列表。如果section不存在，则返回一个空列表。
        /// </summary>
        /// <param name="SectionName"></param>
        /// <returns></returns>
        public List<string> ReadKeys(string SectionName)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, filePath);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        /// <summary>
        /// 读取指定section中的所有key及其对应的值，返回一个字典对象。如果section不存在，则返回一个空字典。
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<string, string> ReadSection(string section)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            int bufferSize = 1024; // 初始缓冲区大小，可根据需要调整
            List<String> rawKeys = ReadKeys(section);


            // 使用正确的分割方法，移除空条目
            string[] keys = rawKeys.ToArray();

            // 读取每个键的值
            foreach (string key in keys)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    string value = ReadValue(section, key);
                    result[key] = value; // 使用索引器处理重复键（如果存在）
                }
            }

            return result;
        }

        /// <summary>
        /// 删除指定section中的指定key。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        public void DeleteKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, filePath);
        }

        /// <summary>
        /// 删除指定的section以及其包含的所有key。
        /// </summary>
        /// <param name="section"></param>
        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, filePath);
        }

        /// <summary>
        /// 判断指定的section是否存在。
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool HasSection(string section)
        {
            List<string> list = ReadKeys(section);

            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断指定的section中是否存在指定的key。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool HasKey(string section, string key)
        {
            string v = ReadValue(section, key, null);
            if (v == null || v == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
