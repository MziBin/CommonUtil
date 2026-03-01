using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonUtil
{
    /// <summary>
    /// 字符串处理工具类，提供常用的字符串操作方法
    /// </summary>
    public static class StringHelper
    {
        #region 字符串格式化

        /// <summary>
        /// 将字符串首字母大写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string FirstLetterToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// 将字符串首字母小写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>首字母小写的字符串</returns>
        public static string FirstLetterToLower(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// 将字符串转换为驼峰命名
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="delimiters">分隔符数组，默认为下划线、破折号和空格</param>
        /// <returns>驼峰命名的字符串</returns>
        public static string ToCamelCase(string input, params char[] delimiters)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (delimiters == null || delimiters.Length == 0)
                delimiters = new[] { '_', '-', ' ' };

            string[] parts = input.Split(delimiters);
            if (parts.Length == 1)
                return FirstLetterToLower(input);

            StringBuilder result = new StringBuilder(FirstLetterToLower(parts[0]));
            for (int i = 1; i < parts.Length; i++)
            {
                result.Append(FirstLetterToUpper(parts[i]));
            }

            return result.ToString();
        }

        /// <summary>
        /// 将字符串转换为帕斯卡命名（大驼峰命名）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="delimiters">分隔符数组，默认为下划线、破折号和空格</param>
        /// <returns>帕斯卡命名的字符串</returns>
        public static string ToPascalCase(string input, params char[] delimiters)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (delimiters == null || delimiters.Length == 0)
                delimiters = new[] { '_', '-', ' ' };

            string[] parts = input.Split(delimiters);
            StringBuilder result = new StringBuilder();
            foreach (string part in parts)
            {
                result.Append(FirstLetterToUpper(part));
            }

            return result.ToString();
        }

        /// <summary>
        /// 将字符串转换为下划线命名
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>下划线命名的字符串</returns>
        public static string ToUnderlineCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && i > 0)
                {
                    result.Append('_');
                }
                result.Append(char.ToLower(input[i]));
            }

            return result.ToString();
        }

        #endregion

        #region 字符串验证

        /// <summary>
        /// 检查字符串是否为null或空字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串为null或空字符串则返回true，否则返回false</returns>
        public static bool IsNullOrEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 检查字符串是否为null、空字符串或仅包含空白字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串为null、空字符串或仅包含空白字符则返回true，否则返回false</returns>
        public static bool IsNullOrWhiteSpace(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 检查字符串是否只包含字母
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串只包含字母则返回true，否则返回false</returns>
        public static bool IsLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

        /// <summary>
        /// 检查字符串是否为有效的邮箱地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>如果是有效的邮箱地址则返回true，否则返回false</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 检查字符串是否只包含数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string input)
        {
            // 空字符串直接返回false
            if (string.IsNullOrEmpty(input))
                return false;

            // 遍历每个字符，只要有一个不是数字就返回false
            foreach (char c in input)
            {
                if (!char.IsDigit(c)) // char.IsDigit() 判断字符是否为0-9的数字
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 检查字符串是否只包含字母和数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(string input)
        {
            // 空字符串直接返回false（若需将空字符串视为合法，可修改此逻辑）
            if (string.IsNullOrEmpty(input))
                return false;

            // 遍历每个字符，只要有一个不是字母/数字就返回false
            foreach (char c in input)
            {
                // char.IsLetterOrDigit()：判断字符是否为字母（任意语言）或数字
                // 注：若需仅限制英文字母，需额外判断 c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z'
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }

        #endregion

        #region 字符串操作

        /// <summary>
        /// 截断字符串到指定长度
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">截断后添加的后缀，默认为"..."</param>
        /// <returns>截断后的字符串</returns>
        public static string Truncate(string input, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;

            return input.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// 去除字符串中的所有空白字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>去除空白字符后的字符串</returns>
        public static string TrimAllWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return Regex.Replace(input, @"\s+", string.Empty);
        }

        /// <summary>
        /// 重复字符串指定次数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(string input, int count)
        {
            if (string.IsNullOrEmpty(input) || count <= 0)
                return string.Empty;

            StringBuilder result = new StringBuilder(input.Length * count);
            for (int i = 0; i < count; i++)
            {
                result.Append(input);
            }

            return result.ToString();
        }

        /// <summary>
        /// 将字符串转换为标题格式
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>标题格式的字符串</returns>
        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        #endregion
    }
}