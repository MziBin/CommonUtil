using System;
using System.Text.RegularExpressions;

namespace CommonUtil
{
    /// <summary>
    /// 验证工具类，提供常用的数据验证方法
    /// </summary>
    public static class ValidationHelper
    {
        #region 正则表达式常量

        private const string EmailRegex = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
        private const string UrlRegex = "^(https?:\\/\\/)?([\\da-z.-]+)\\.([a-z.]{2,6})([\\/\\w .-]*)*\\/?$";
        private const string PhoneRegex = "^1[3-9]\\d{9}$"; // 中国大陆手机号
        private const string IdCardRegex = "^\\d{17}[\\dXx]$"; // 中国大陆身份证号
        private const string NumberRegex = "^-?\\d+(\\.\\d+)?$";
        private const string IntegerRegex = "^-?\\d+$";
        private const string PositiveNumberRegex = "^\\d+(\\.\\d+)?$";
        private const string PositiveIntegerRegex = "^\\d+$";

        #endregion

        #region 字符串验证

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串为null或空字符串则返回true，否则返回false</returns>
        public static bool IsEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 判断字符串是否不为空
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串不为null且不为空字符串则返回true，否则返回false</returns>
        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 判断字符串是否为空白
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串为null、空字符串或仅包含空白字符则返回true，否则返回false</returns>
        public static bool IsBlank(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串是否不为空白
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果字符串不为null、空字符串且包含非空白字符则返回true，否则返回false</returns>
        public static bool IsNotBlank(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串长度是否在指定范围内
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>如果字符串长度在指定范围内则返回true，否则返回false</returns>
        public static bool IsLengthInRange(string input, int minLength, int maxLength)
        {
            if (input == null)
                return minLength <= 0;
            return input.Length >= minLength && input.Length <= maxLength;
        }

        #endregion

        #region 格式验证

        /// <summary>
        /// 判断是否为有效的邮箱地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>如果是有效的邮箱地址则返回true，否则返回false</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断是否为有效的URL地址
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>如果是有效的URL地址则返回true，否则返回false</returns>
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;
            return Regex.IsMatch(url, UrlRegex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断是否为有效的中国大陆手机号
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns>如果是有效的中国大陆手机号则返回true，否则返回false</returns>
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            return Regex.IsMatch(phone, PhoneRegex);
        }

        /// <summary>
        /// 判断是否为有效的中国大陆身份证号
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns>如果是有效的中国大陆身份证号则返回true，否则返回false</returns>
        public static bool IsValidIdCard(string idCard)
        {
            if (string.IsNullOrWhiteSpace(idCard))
                return false;
            
            // 验证格式
            if (!Regex.IsMatch(idCard, IdCardRegex))
                return false;
            
            // 验证校验码
            int[] weights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            char[] checkCodes = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += (idCard[i] - '0') * weights[i];
            }
            
            char expectedCheckCode = checkCodes[sum % 11];
            return char.ToUpper(idCard[17]) == expectedCheckCode;
        }

        #endregion

        #region 数字验证

        /// <summary>
        /// 判断是否为有效的数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果是有效的数字则返回true，否则返回false</returns>
        public static bool IsValidNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return Regex.IsMatch(input, NumberRegex);
        }

        /// <summary>
        /// 判断是否为有效的整数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果是有效的整数则返回true，否则返回false</returns>
        public static bool IsValidInteger(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return Regex.IsMatch(input, IntegerRegex);
        }

        /// <summary>
        /// 判断是否为有效的正数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果是有效的正数则返回true，否则返回false</returns>
        public static bool IsValidPositiveNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return Regex.IsMatch(input, PositiveNumberRegex);
        }

        /// <summary>
        /// 判断是否为有效的正整数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>如果是有效的正整数则返回true，否则返回false</returns>
        public static bool IsValidPositiveInteger(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            return Regex.IsMatch(input, PositiveIntegerRegex);
        }

        /// <summary>
        /// 判断数字是否在指定范围内
        /// </summary>
        /// <param name="number">数字</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>如果数字在指定范围内则返回true，否则返回false</returns>
        public static bool IsNumberInRange(double number, double min, double max)
        {
            return number >= min && number <= max;
        }

        /// <summary>
        /// 判断整数是否在指定范围内
        /// </summary>
        /// <param name="number">整数</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>如果整数在指定范围内则返回true，否则返回false</returns>
        public static bool IsIntegerInRange(long number, long min, long max)
        {
            return number >= min && number <= max;
        }

        #endregion

        #region 日期时间验证

        /// <summary>
        /// 判断是否为有效的日期时间字符串
        /// </summary>
        /// <param name="dateTimeString">日期时间字符串</param>
        /// <param name="format">格式化字符串，默认为"yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>如果是有效的日期时间字符串则返回true，否则返回false</returns>
        public static bool IsValidDateTime(string dateTimeString, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.TryParseExact(dateTimeString, format, System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, out _);
        }

        /// <summary>
        /// 判断日期是否在指定范围内
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>如果日期在指定范围内则返回true，否则返回false</returns>
        public static bool IsDateInRange(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        #endregion

        #region 密码强度验证

        /// <summary>
        /// 判断密码强度是否为弱
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>如果密码强度为弱则返回true，否则返回false</returns>
        public static bool IsWeakPassword(string password)
        {
            // 弱密码：长度小于6，或仅包含数字/字母
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return true;
            
            return IsValidNumber(password) || Regex.IsMatch(password, "^[a-zA-Z]+$");
        }

        /// <summary>
        /// 判断密码强度是否为中等
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>如果密码强度为中等则返回true，否则返回false</returns>
        public static bool IsMediumPassword(string password)
        {
            // 中等密码：长度6-12，包含字母和数字
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 12)
                return false;
            
            return Regex.IsMatch(password, "^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z\\d]+$");
        }

        /// <summary>
        /// 判断密码强度是否为强
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>如果密码强度为强则返回true，否则返回false</returns>
        public static bool IsStrongPassword(string password)
        {
            // 强密码：长度大于12，包含字母、数字和特殊字符
            if (string.IsNullOrWhiteSpace(password) || password.Length <= 12)
                return false;
            
            return Regex.IsMatch(password, "^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\\\"\\|,.<>\\/?]).+$");
        }
        #endregion
    }
}