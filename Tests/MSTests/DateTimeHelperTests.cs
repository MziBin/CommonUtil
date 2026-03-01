using CommonUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTests
{
    [TestClass]
    public class DateTimeHelperTests
    {
        #region 日期格式化测试

        [TestMethod]
        public void Format_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45);

            // 执行：格式化日期时间
            string result1 = DateTimeHelper.Format(testDateTime);
            string result2 = DateTimeHelper.Format(testDateTime, "yyyy/MM/dd HH:mm:ss");
            string result3 = DateTimeHelper.Format(testDateTime, "yyyy-MM-dd");
            string result4 = DateTimeHelper.Format(testDateTime, "HH:mm:ss");

            // 断言：格式化结果应正确
            Assert.AreEqual("2023-10-15 14:30:45", result1, "默认格式格式化失败");
            Assert.AreEqual("2023/10/15 14:30:45", result2, "自定义格式格式化失败");
            Assert.AreEqual("2023-10-15", result3, "日期格式格式化失败");
            Assert.AreEqual("14:30:45", result4, "时间格式格式化失败");
        }

        [TestMethod]
        public void FormatDate_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45);

            // 执行：格式化日期
            string result1 = DateTimeHelper.FormatDate(testDateTime);
            string result2 = DateTimeHelper.FormatDate(testDateTime, "yyyy/MM/dd");

            // 断言：格式化结果应正确
            Assert.AreEqual("2023-10-15", result1, "默认日期格式格式化失败");
            Assert.AreEqual("2023/10/15", result2, "自定义日期格式格式化失败");
        }

        [TestMethod]
        public void FormatTime_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45);

            // 执行：格式化时间
            string result1 = DateTimeHelper.FormatTime(testDateTime);
            string result2 = DateTimeHelper.FormatTime(testDateTime, "HH:mm");

            // 断言：格式化结果应正确
            Assert.AreEqual("14:30:45", result1, "默认时间格式格式化失败");
            Assert.AreEqual("14:30", result2, "自定义时间格式格式化失败");
        }

        #endregion

        #region 日期计算测试

        [TestMethod]
        public void AddDays_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15);

            // 执行：添加天数
            DateTime result1 = DateTimeHelper.AddDays(testDateTime, 5);
            DateTime result2 = DateTimeHelper.AddDays(testDateTime, -5);

            // 断言：添加天数后的日期应正确
            Assert.AreEqual(new DateTime(2023, 10, 20), result1, "添加正天数失败");
            Assert.AreEqual(new DateTime(2023, 10, 10), result2, "添加负天数失败");
        }

        [TestMethod]
        public void AddMonths_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15);

            // 执行：添加月数
            DateTime result1 = DateTimeHelper.AddMonths(testDateTime, 3);
            DateTime result2 = DateTimeHelper.AddMonths(testDateTime, -3);

            // 断言：添加月数后的日期应正确
            Assert.AreEqual(new DateTime(2024, 1, 15), result1, "添加正月数失败");
            Assert.AreEqual(new DateTime(2023, 7, 15), result2, "添加负月数失败");
        }

        [TestMethod]
        public void AddYears_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15);

            // 执行：添加年数
            DateTime result1 = DateTimeHelper.AddYears(testDateTime, 2);
            DateTime result2 = DateTimeHelper.AddYears(testDateTime, -2);

            // 断言：添加年数后的日期应正确
            Assert.AreEqual(new DateTime(2025, 10, 15), result1, "添加正年数失败");
            Assert.AreEqual(new DateTime(2021, 10, 15), result2, "添加负年数失败");
        }

        [TestMethod]
        public void GetDaysDifference_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime startDate = new DateTime(2023, 10, 15);
            DateTime endDate = new DateTime(2023, 10, 20);

            // 执行：计算天数差
            int result1 = DateTimeHelper.GetDaysDifference(startDate, endDate);
            int result2 = DateTimeHelper.GetDaysDifference(endDate, startDate);

            // 断言：天数差应正确
            Assert.AreEqual(5, result1, "正天数差计算失败");
            Assert.AreEqual(-5, result2, "负天数差计算失败");
        }

        [TestMethod]
        public void GetMonthsDifference_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime startDate = new DateTime(2023, 10, 15);
            DateTime endDate = new DateTime(2024, 1, 20);

            // 执行：计算月数差
            int result1 = DateTimeHelper.GetMonthsDifference(startDate, endDate);
            int result2 = DateTimeHelper.GetMonthsDifference(endDate, startDate);

            // 断言：月数差应正确
            Assert.AreEqual(3, result1, "正月数差计算失败");
            Assert.AreEqual(-3, result2, "负月数差计算失败");
        }

        [TestMethod]
        public void GetYearsDifference_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime startDate = new DateTime(2023, 10, 15);
            DateTime endDate = new DateTime(2025, 1, 20);

            // 执行：计算年数差
            int result1 = DateTimeHelper.GetYearsDifference(startDate, endDate);
            int result2 = DateTimeHelper.GetYearsDifference(endDate, startDate);

            // 断言：年数差应正确
            Assert.AreEqual(2, result1, "正年数差计算失败");
            Assert.AreEqual(-2, result2, "负年数差计算失败");
        }

        #endregion

        #region 日期比较测试

        [TestMethod]
        public void IsToday_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是今天
            bool result1 = DateTimeHelper.IsToday(DateTime.Today);
            bool result2 = DateTimeHelper.IsToday(DateTime.Today.AddDays(-1));
            bool result3 = DateTimeHelper.IsToday(DateTime.Today.AddDays(1));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "今天日期判断失败");
            Assert.IsFalse(result2, "昨天日期判断失败");
            Assert.IsFalse(result3, "明天日期判断失败");
        }

        [TestMethod]
        public void IsYesterday_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是昨天
            bool result1 = DateTimeHelper.IsYesterday(DateTime.Today.AddDays(-1));
            bool result2 = DateTimeHelper.IsYesterday(DateTime.Today);
            bool result3 = DateTimeHelper.IsYesterday(DateTime.Today.AddDays(1));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "昨天日期判断失败");
            Assert.IsFalse(result2, "今天日期判断失败");
            Assert.IsFalse(result3, "明天日期判断失败");
        }

        [TestMethod]
        public void IsTomorrow_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是明天
            bool result1 = DateTimeHelper.IsTomorrow(DateTime.Today.AddDays(1));
            bool result2 = DateTimeHelper.IsTomorrow(DateTime.Today);
            bool result3 = DateTimeHelper.IsTomorrow(DateTime.Today.AddDays(-1));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "明天日期判断失败");
            Assert.IsFalse(result2, "今天日期判断失败");
            Assert.IsFalse(result3, "昨天日期判断失败");
        }

        [TestMethod]
        public void IsThisWeek_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是本周
            bool result1 = DateTimeHelper.IsThisWeek(DateTime.Now);
            bool result2 = DateTimeHelper.IsThisWeek(DateTime.Now.AddDays(-7));
            bool result3 = DateTimeHelper.IsThisWeek(DateTime.Now.AddDays(7));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "本周日期判断失败");
            Assert.IsFalse(result2, "上周日期判断失败");
            Assert.IsFalse(result3, "下周日期判断失败");
        }

        [TestMethod]
        public void IsThisMonth_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是本月
            bool result1 = DateTimeHelper.IsThisMonth(DateTime.Now);
            bool result2 = DateTimeHelper.IsThisMonth(DateTime.Now.AddMonths(-1));
            bool result3 = DateTimeHelper.IsThisMonth(DateTime.Now.AddMonths(1));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "本月日期判断失败");
            Assert.IsFalse(result2, "上月日期判断失败");
            Assert.IsFalse(result3, "下月日期判断失败");
        }

        [TestMethod]
        public void IsThisYear_ReturnsCorrectResult()
        {
            // 执行：判断日期是否是本年
            bool result1 = DateTimeHelper.IsThisYear(DateTime.Now);
            bool result2 = DateTimeHelper.IsThisYear(DateTime.Now.AddYears(-1));
            bool result3 = DateTimeHelper.IsThisYear(DateTime.Now.AddYears(1));

            // 断言：判断结果应正确
            Assert.IsTrue(result1, "本年日期判断失败");
            Assert.IsFalse(result2, "去年日期判断失败");
            Assert.IsFalse(result3, "明年日期判断失败");
        }

        #endregion

        #region 日期转换测试

        [TestMethod]
        public void Parse_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间和字符串
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45);
            string dateString = "2023-10-15 14:30:45";

            // 执行：解析字符串为日期时间
            DateTime result = DateTimeHelper.Parse(dateString);

            // 断言：解析结果应正确
            Assert.AreEqual(testDateTime, result, "日期时间解析失败");
        }

        [TestMethod]
        public void Parse_WithDefaultValue_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间和字符串
            DateTime defaultValue = DateTime.Now;
            string validDateString = "2023-10-15 14:30:45";
            string invalidDateString = "invalid-date";

            // 执行：解析字符串为日期时间，带默认值
            DateTime result1 = DateTimeHelper.Parse(validDateString, defaultValue);
            DateTime result2 = DateTimeHelper.Parse(invalidDateString, defaultValue);

            // 断言：解析结果应正确
            Assert.AreEqual(new DateTime(2023, 10, 15, 14, 30, 45), result1, "有效日期字符串解析失败");
            Assert.AreEqual(defaultValue, result2, "无效日期字符串默认值返回失败");
        }

        [TestMethod]
        public void ToTimestamp_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45, DateTimeKind.Utc);
            long expectedTimestamp = 1697375445;

            // 执行：转换为时间戳
            long result = DateTimeHelper.ToTimestamp(testDateTime);

            // 断言：时间戳转换结果应正确
            Assert.AreEqual(expectedTimestamp, result, "时间戳转换失败");
        }

        [TestMethod]
        public void ToTimestampMilliseconds_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15, 14, 30, 45, 500, DateTimeKind.Utc);
            long expectedTimestamp = 1697365845500;

            // 执行：转换为毫秒时间戳
            long result = DateTimeHelper.ToTimestampMilliseconds(testDateTime);

            // 断言：毫秒时间戳转换结果应正确
            Assert.AreEqual(expectedTimestamp, result, "毫秒时间戳转换失败");
        }

        [TestMethod]
        public void FromTimestamp_ReturnsCorrectResult()
        {
            // 准备：创建测试时间戳
            long timestamp = 1697375445;
            DateTime expectedDateTime = new DateTime(2023, 10, 15, 14, 30, 45, DateTimeKind.Utc);

            // 执行：从时间戳转换为日期时间
            DateTime result = DateTimeHelper.FromTimestamp(timestamp);

            // 断言：日期时间转换结果应正确
            Assert.AreEqual(expectedDateTime, result, "从时间戳转换为日期时间失败");
        }

        [TestMethod]
        public void FromTimestampMilliseconds_ReturnsCorrectResult()
        {
            // 准备：创建测试毫秒时间戳
            long timestamp = 1697375445500;
            DateTime expectedDateTime = new DateTime(2023, 10, 15, 14, 30, 45, 500, DateTimeKind.Utc);

            // 执行：从毫秒时间戳转换为日期时间
            DateTime result = DateTimeHelper.FromTimestampMilliseconds(timestamp);

            // 断言：日期时间转换结果应正确
            Assert.AreEqual(expectedDateTime, result, "从毫秒时间戳转换为日期时间失败");
        }

        #endregion

        #region 其他常用功能测试

        [TestMethod]
        public void GetMondayOfCurrentWeek_ReturnsCorrectResult()
        {
            // 执行：获取本周一的日期
            DateTime result = DateTimeHelper.GetMondayOfCurrentWeek();

            // 断言：本周一的日期应正确
            Assert.AreEqual(DayOfWeek.Monday, result.DayOfWeek, "获取本周一失败");
        }

        [TestMethod]
        public void GetFirstDayOfCurrentMonth_ReturnsCorrectResult()
        {
            // 执行：获取本月第一天的日期
            DateTime result = DateTimeHelper.GetFirstDayOfCurrentMonth();

            // 断言：本月第一天的日期应正确
            Assert.AreEqual(1, result.Day, "获取本月第一天失败");
            Assert.AreEqual(DateTime.Now.Month, result.Month, "获取本月第一天失败");
            Assert.AreEqual(DateTime.Now.Year, result.Year, "获取本月第一天失败");
        }

        [TestMethod]
        public void GetLastDayOfCurrentMonth_ReturnsCorrectResult()
        {
            // 执行：获取本月最后一天的日期
            DateTime result = DateTimeHelper.GetLastDayOfCurrentMonth();

            // 断言：本月最后一天的日期应正确
            Assert.AreEqual(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), result.Day, "获取本月最后一天失败");
            Assert.AreEqual(DateTime.Now.Month, result.Month, "获取本月最后一天失败");
            Assert.AreEqual(DateTime.Now.Year, result.Year, "获取本月最后一天失败");
        }

        [TestMethod]
        public void GetFirstDayOfCurrentYear_ReturnsCorrectResult()
        {
            // 执行：获取本年第一天的日期
            DateTime result = DateTimeHelper.GetFirstDayOfCurrentYear();

            // 断言：本年第一天的日期应正确
            Assert.AreEqual(1, result.Day, "获取本年第一天失败");
            Assert.AreEqual(1, result.Month, "获取本年第一天失败");
            Assert.AreEqual(DateTime.Now.Year, result.Year, "获取本年第一天失败");
        }

        [TestMethod]
        public void GetLastDayOfCurrentYear_ReturnsCorrectResult()
        {
            // 执行：获取本年最后一天的日期
            DateTime result = DateTimeHelper.GetLastDayOfCurrentYear();

            // 断言：本年最后一天的日期应正确
            Assert.AreEqual(31, result.Day, "获取本年最后一天失败");
            Assert.AreEqual(12, result.Month, "获取本年最后一天失败");
            Assert.AreEqual(DateTime.Now.Year, result.Year, "获取本年最后一天失败");
        }

        [TestMethod]
        public void GetChineseDayOfWeek_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15); // 星期日

            // 执行：获取星期几的中文名称
            string result = DateTimeHelper.GetChineseDayOfWeek(testDateTime);

            // 断言：星期几的中文名称应正确
            Assert.AreEqual("星期日", result, "获取星期几的中文名称失败");
        }

        [TestMethod]
        public void GetChineseMonth_ReturnsCorrectResult()
        {
            // 准备：创建测试日期时间
            DateTime testDateTime = new DateTime(2023, 10, 15); // 十月

            // 执行：获取月份的中文名称
            string result = DateTimeHelper.GetChineseMonth(testDateTime);

            // 断言：月份的中文名称应正确
            Assert.AreEqual("十月", result, "获取月份的中文名称失败");
        }

        #endregion
    }
}