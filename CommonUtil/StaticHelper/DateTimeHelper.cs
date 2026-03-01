using System;
using System.Globalization;

namespace CommonUtil
{
    /// <summary>
    /// 日期时间处理工具类，提供常用的日期时间操作方法
    /// </summary>
    public static class DateTimeHelper
    {
        #region 日期格式化

        /// <summary>
        /// 将日期时间格式化为指定格式的字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="format">格式化字符串，默认为"yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>格式化后的日期时间字符串</returns>
        public static string Format(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将日期格式化为指定格式的字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="format">格式化字符串，默认为"yyyy-MM-dd"</param>
        /// <returns>格式化后的日期字符串</returns>
        public static string FormatDate(DateTime dateTime, string format = "yyyy-MM-dd")
        {
            return Format(dateTime, format);
        }

        /// <summary>
        /// 将时间格式化为指定格式的字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="format">格式化字符串，默认为"HH:mm:ss"</param>
        /// <returns>格式化后的时间字符串</returns>
        public static string FormatTime(DateTime dateTime, string format = "HH:mm:ss")
        {
            return Format(dateTime, format);
        }

        #endregion

        #region 日期计算

        /// <summary>
        /// 向日期时间添加指定天数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="days">要添加的天数</param>
        /// <returns>添加天数后的日期时间</returns>
        public static DateTime AddDays(DateTime dateTime, int days)
        {
            return dateTime.AddDays(days);
        }

        /// <summary>
        /// 向日期时间添加指定月数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="months">要添加的月数</param>
        /// <returns>添加月数后的日期时间</returns>
        public static DateTime AddMonths(DateTime dateTime, int months)
        {
            return dateTime.AddMonths(months);
        }

        /// <summary>
        /// 向日期时间添加指定年数
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="years">要添加的年数</param>
        /// <returns>添加年数后的日期时间</returns>
        public static DateTime AddYears(DateTime dateTime, int years)
        {
            return dateTime.AddYears(years);
        }

        /// <summary>
        /// 计算两个日期之间的天数差
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>天数差</returns>
        public static int GetDaysDifference(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays;
        }

        /// <summary>
        /// 计算两个日期之间的月数差
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>月数差</returns>
        public static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
        }

        /// <summary>
        /// 计算两个日期之间的年数差
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>年数差</returns>
        public static int GetYearsDifference(DateTime startDate, DateTime endDate)
        {
            return endDate.Year - startDate.Year;
        }

        #endregion

        #region 日期比较

        /// <summary>
        /// 判断日期是否是今天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是今天则返回true，否则返回false</returns>
        public static bool IsToday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }

        /// <summary>
        /// 判断日期是否是昨天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是昨天则返回true，否则返回false</returns>
        public static bool IsYesterday(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// 判断日期是否是明天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是明天则返回true，否则返回false</returns>
        public static bool IsTomorrow(DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// 判断日期是否是本周
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是本周则返回true，否则返回false</returns>
        public static bool IsThisWeek(DateTime dateTime)
        {
            DateTime today = DateTime.Today;
            int daysSinceSunday = (int)today.DayOfWeek;
            DateTime thisMonday = today.AddDays(-daysSinceSunday + 1);
            DateTime nextMonday = thisMonday.AddDays(7);

            return dateTime >= thisMonday && dateTime < nextMonday;
        }

        /// <summary>
        /// 判断日期是否是本月
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是本月则返回true，否则返回false</returns>
        public static bool IsThisMonth(DateTime dateTime)
        {
            return dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month;
        }

        /// <summary>
        /// 判断日期是否是本年
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>如果是本年则返回true，否则返回false</returns>
        public static bool IsThisYear(DateTime dateTime)
        {
            return dateTime.Year == DateTime.Now.Year;
        }

        #endregion

        #region 日期转换

        /// <summary>
        /// 将字符串转换为日期时间
        /// </summary>
        /// <param name="dateString">日期时间字符串</param>
        /// <param name="format">格式化字符串，默认为"yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>转换后的日期时间</returns>
        public static DateTime Parse(string dateString, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 将字符串转换为日期时间，转换失败时返回默认值
        /// </summary>
        /// <param name="dateString">日期时间字符串</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <param name="format">格式化字符串，默认为"yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>转换后的日期时间或默认值</returns>
        public static DateTime Parse(string dateString, DateTime defaultValue, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将日期时间转换为时间戳（秒）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间戳（秒）</returns>
        public static long ToTimestamp(DateTime dateTime)
        {
            DateTimeOffset offset = new DateTimeOffset(dateTime);
            return offset.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 将日期时间转换为时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>时间戳（毫秒）</returns>
        public static long ToTimestampMilliseconds(DateTime dateTime)
        {
            DateTimeOffset offset = new DateTimeOffset(dateTime);
            return offset.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 将时间戳（秒）转换为日期时间
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns>转换后的日期时间</returns>
        public static DateTime FromTimestamp(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
        }

        /// <summary>
        /// 将时间戳（毫秒）转换为日期时间
        /// </summary>
        /// <param name="timestamp">时间戳（毫秒）</param>
        /// <returns>转换后的日期时间</returns>
        public static DateTime FromTimestampMilliseconds(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
        }

        #endregion

        #region 其他常用功能

        /// <summary>
        /// 获取本周一的日期
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>本周一的日期</returns>
        public static DateTime GetMondayOfCurrentWeek(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            int daysSinceSunday = (int)dateTime.DayOfWeek;
            return dateTime.AddDays(-daysSinceSunday + 1).Date;
        }

        /// <summary>
        /// 获取本月第一天的日期
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>本月第一天的日期</returns>
        public static DateTime GetFirstDayOfCurrentMonth(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// 获取本月最后一天的日期
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>本月最后一天的日期</returns>
        public static DateTime GetLastDayOfCurrentMonth(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取本年第一天的日期
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>本年第一天的日期</returns>
        public static DateTime GetFirstDayOfCurrentYear(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            return new DateTime(dateTime.Year, 1, 1);
        }

        /// <summary>
        /// 获取本年最后一天的日期
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>本年最后一天的日期</returns>
        public static DateTime GetLastDayOfCurrentYear(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            return new DateTime(dateTime.Year, 12, 31);
        }

        /// <summary>
        /// 获取星期几的中文名称
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>星期几的中文名称</returns>
        public static string GetChineseDayOfWeek(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            string[] chineseDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return chineseDays[(int)dateTime.DayOfWeek];
        }

        /// <summary>
        /// 获取月份的中文名称
        /// </summary>
        /// <param name="dateTime">日期时间，默认为当前时间</param>
        /// <returns>月份的中文名称</returns>
        public static string GetChineseMonth(DateTime dateTime = default(DateTime))
        {
            if (dateTime == default(DateTime))
                dateTime = DateTime.Now;

            string[] chineseMonths = { "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };
            return chineseMonths[dateTime.Month - 1];
        }

        #endregion
    }
}