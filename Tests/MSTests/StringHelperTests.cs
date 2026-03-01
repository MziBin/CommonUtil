//using CommonUtil;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace MSTests
//{
//    [TestClass]
//    public class StringHelperTests
//    {
//        #region 字符串格式化测试

//        [TestMethod]
//        public void FirstLetterToUpper_ReturnsCorrectResult()
//        {
//            // 执行：将字符串首字母大写
//            string result = StringHelper.FirstLetterToUpper("hello");
//            string resultEmpty = StringHelper.FirstLetterToUpper(string.Empty);
//            string resultNull = StringHelper.FirstLetterToUpper(null);

//            // 断言：首字母大写的字符串应正确
//            Assert.AreEqual("Hello", result, "首字母大写失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串首字母大写失败");
//            Assert.IsNull(resultNull, "null字符串首字母大写失败");
//        }

//        [TestMethod]
//        public void FirstLetterToLower_ReturnsCorrectResult()
//        {
//            // 执行：将字符串首字母小写
//            string result = StringHelper.FirstLetterToLower("Hello");
//            string resultEmpty = StringHelper.FirstLetterToLower(string.Empty);
//            string resultNull = StringHelper.FirstLetterToLower(null);

//            // 断言：首字母小写的字符串应正确
//            Assert.AreEqual("hello", result, "首字母小写失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串首字母小写失败");
//            Assert.IsNull(resultNull, "null字符串首字母小写失败");
//        }

//        [TestMethod]
//        public void ToCamelCase_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为驼峰命名
//            string result1 = StringHelper.ToCamelCase("hello_world");
//            string result2 = StringHelper.ToCamelCase("HELLO-WORLD");
//            string result3 = StringHelper.ToCamelCase("Hello World");
//            string result4 = StringHelper.ToCamelCase("HelloWorld");
//            string resultEmpty = StringHelper.ToCamelCase(string.Empty);
//            string resultNull = StringHelper.ToCamelCase(null);

//            // 断言：驼峰命名的字符串应正确
//            Assert.AreEqual("helloWorld", result1, "下划线分隔的字符串转换为驼峰命名失败");
//            Assert.AreEqual("helloWorld", result2, "破折号分隔的字符串转换为驼峰命名失败");
//            Assert.AreEqual("helloWorld", result3, "空格分隔的字符串转换为驼峰命名失败");
//            Assert.AreEqual("helloWorld", result4, "已为驼峰命名的字符串转换失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串转换为驼峰命名失败");
//            Assert.IsNull(resultNull, "null字符串转换为驼峰命名失败");
//        }

//        [TestMethod]
//        public void ToPascalCase_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为帕斯卡命名
//            string result1 = StringHelper.ToPascalCase("hello_world");
//            string result2 = StringHelper.ToPascalCase("hello-world");
//            string result3 = StringHelper.ToPascalCase("hello world");
//            string result4 = StringHelper.ToPascalCase("helloWorld");
//            string resultEmpty = StringHelper.ToPascalCase(string.Empty);
//            string resultNull = StringHelper.ToPascalCase(null);

//            // 断言：帕斯卡命名的字符串应正确
//            Assert.AreEqual("HelloWorld", result1, "下划线分隔的字符串转换为帕斯卡命名失败");
//            Assert.AreEqual("HelloWorld", result2, "破折号分隔的字符串转换为帕斯卡命名失败");
//            Assert.AreEqual("HelloWorld", result3, "空格分隔的字符串转换为帕斯卡命名失败");
//            Assert.AreEqual("HelloWorld", result4, "驼峰命名的字符串转换为帕斯卡命名失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串转换为帕斯卡命名失败");
//            Assert.IsNull(resultNull, "null字符串转换为帕斯卡命名失败");
//        }

//        #endregion

//        #region 字符串验证测试

//        [TestMethod]
//        public void IsLetter_ReturnsCorrectResult()
//        {
//            // 执行：检查字符串是否只包含字母
//            bool result1 = StringHelper.IsLetter("hello");
//            bool result2 = StringHelper.IsLetter("Hello123");
//            bool result3 = StringHelper.IsLetter("hello world");
//            bool resultEmpty = StringHelper.IsLetter(string.Empty);
//            bool resultNull = StringHelper.IsLetter(null);

//            // 断言：检查结果应正确
//            Assert.IsTrue(result1, "纯字母字符串检查失败");
//            Assert.IsFalse(result2, "包含数字的字符串检查失败");
//            Assert.IsFalse(result3, "包含空格的字符串检查失败");
//            Assert.IsFalse(resultEmpty, "空字符串检查失败");
//            Assert.IsFalse(resultNull, "null字符串检查失败");
//        }

//        [TestMethod]
//        public void IsNumber_ReturnsCorrectResult()
//        {
//            // 执行：检查字符串是否只包含数字
//            bool result1 = StringHelper.IsNumber("123456");
//            bool result2 = StringHelper.IsNumber("123abc");
//            bool result3 = StringHelper.IsNumber("123 456");
//            bool resultEmpty = StringHelper.IsNumber(string.Empty);
//            bool resultNull = StringHelper.IsNumber(null);

//            // 断言：检查结果应正确
//            Assert.IsTrue(result1, "纯数字字符串检查失败");
//            Assert.IsFalse(result2, "包含字母的字符串检查失败");
//            Assert.IsFalse(result3, "包含空格的字符串检查失败");
//            Assert.IsFalse(resultEmpty, "空字符串检查失败");
//            Assert.IsFalse(resultNull, "null字符串检查失败");
//        }

//        [TestMethod]
//        public void IsLetterOrNumber_ReturnsCorrectResult()
//        {
//            // 执行：检查字符串是否只包含字母和数字
//            bool result1 = StringHelper.IsLetterOrNumber("hello123");
//            bool result2 = StringHelper.IsLetterOrNumber("hello_123");
//            bool result3 = StringHelper.IsLetterOrNumber("hello 123");
//            bool resultEmpty = StringHelper.IsLetterOrNumber(string.Empty);
//            bool resultNull = StringHelper.IsLetterOrNumber(null);

//            // 断言：检查结果应正确
//            Assert.IsTrue(result1, "字母和数字组成的字符串检查失败");
//            Assert.IsFalse(result2, "包含下划线的字符串检查失败");
//            Assert.IsFalse(result3, "包含空格的字符串检查失败");
//            Assert.IsFalse(resultEmpty, "空字符串检查失败");
//            Assert.IsFalse(resultNull, "null字符串检查失败");
//        }

//        [TestMethod]
//        public void IsValidEmail_ReturnsCorrectResult()
//        {
//            // 执行：检查字符串是否为有效的电子邮件地址
//            bool result1 = StringHelper.IsValidEmail("test@example.com");
//            bool result2 = StringHelper.IsValidEmail("invalid-email");
//            bool result3 = StringHelper.IsValidEmail("test@");
//            bool resultEmpty = StringHelper.IsValidEmail(string.Empty);
//            bool resultNull = StringHelper.IsValidEmail(null);

//            // 断言：检查结果应正确
//            Assert.IsTrue(result1, "有效的电子邮件地址检查失败");
//            Assert.IsFalse(result2, "无效的电子邮件地址检查失败");
//            Assert.IsFalse(result3, "不完整的电子邮件地址检查失败");
//            Assert.IsFalse(resultEmpty, "空字符串电子邮件地址检查失败");
//            Assert.IsFalse(resultNull, "null字符串电子邮件地址检查失败");
//        }

//        [TestMethod]
//        public void IsValidPhoneNumber_ReturnsCorrectResult()
//        {
//            // 执行：检查字符串是否为有效的手机号码
//            bool result1 = StringHelper.IsValidPhoneNumber("13812345678");
//            bool result2 = StringHelper.IsValidPhoneNumber("12345678901");
//            bool result3 = StringHelper.IsValidPhoneNumber("1381234567");
//            bool resultEmpty = StringHelper.IsValidPhoneNumber(string.Empty);
//            bool resultNull = StringHelper.IsValidPhoneNumber(null);

//            // 断言：检查结果应正确
//            Assert.IsTrue(result1, "有效的手机号码检查失败");
//            Assert.IsFalse(result2, "无效的手机号码前缀检查失败");
//            Assert.IsFalse(result3, "位数不足的手机号码检查失败");
//            Assert.IsFalse(resultEmpty, "空字符串手机号码检查失败");
//            Assert.IsFalse(resultNull, "null字符串手机号码检查失败");
//        }

//        #endregion

//        #region 字符串转换测试

//        [TestMethod]
//        public void ConvertTo_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为指定类型
//            int intResult = StringHelper.ConvertTo<int>("123");
//            double doubleResult = StringHelper.ConvertTo<double>("123.45");
//            bool boolResult = StringHelper.ConvertTo<bool>("true");
//            decimal decimalResult = StringHelper.ConvertTo<decimal>("123.45");
//            int intDefault = StringHelper.ConvertTo<int>(string.Empty);

//            // 断言：转换结果应正确
//            Assert.AreEqual(123, intResult, "字符串转int失败");
//            Assert.AreEqual(123.45, doubleResult, "字符串转double失败");
//            Assert.IsTrue(boolResult, "字符串转bool失败");
//            Assert.AreEqual(123.45m, decimalResult, "字符串转decimal失败");
//            Assert.AreEqual(0, intDefault, "空字符串转int默认值失败");
//        }

//        [TestMethod]
//        public void ConvertTo_WithDefaultValue_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为指定类型，转换失败时返回默认值
//            int intResult = StringHelper.ConvertTo<int>("123", 0);
//            int intDefault = StringHelper.ConvertTo<int>("abc", 999);
//            bool boolResult = StringHelper.ConvertTo<bool>("true", false);
//            bool boolDefault = StringHelper.ConvertTo<bool>("abc", false);

//            // 断言：转换结果应正确
//            Assert.AreEqual(123, intResult, "字符串转int失败");
//            Assert.AreEqual(999, intDefault, "转换失败时默认值返回失败");
//            Assert.IsTrue(boolResult, "字符串转bool失败");
//            Assert.IsFalse(boolDefault, "转换失败时bool默认值返回失败");
//        }

//        [TestMethod]
//        public void ToInt_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为int类型
//            int result1 = StringHelper.ToInt("123");
//            int result2 = StringHelper.ToInt(string.Empty);
//            int result3 = StringHelper.ToInt("abc", 999);

//            // 断言：转换结果应正确
//            Assert.AreEqual(123, result1, "字符串转int失败");
//            Assert.AreEqual(0, result2, "空字符串转int失败");
//            Assert.AreEqual(999, result3, "无效字符串转int默认值失败");
//        }

//        [TestMethod]
//        public void ToDouble_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为double类型
//            double result1 = StringHelper.ToDouble("123.45");
//            double result2 = StringHelper.ToDouble(string.Empty);
//            double result3 = StringHelper.ToDouble("abc", 999.99);

//            // 断言：转换结果应正确
//            Assert.AreEqual(123.45, result1, "字符串转double失败");
//            Assert.AreEqual(0, result2, "空字符串转double失败");
//            Assert.AreEqual(999.99, result3, "无效字符串转double默认值失败");
//        }

//        [TestMethod]
//        public void ToBoolean_ReturnsCorrectResult()
//        {
//            // 执行：将字符串转换为bool类型
//            bool result1 = StringHelper.ToBoolean("true");
//            bool result2 = StringHelper.ToBoolean("false");
//            bool result3 = StringHelper.ToBoolean(string.Empty);
//            bool result4 = StringHelper.ToBoolean("abc", true);

//            // 断言：转换结果应正确
//            Assert.IsTrue(result1, "字符串转bool(true)失败");
//            Assert.IsFalse(result2, "字符串转bool(false)失败");
//            Assert.IsFalse(result3, "空字符串转bool失败");
//            Assert.IsTrue(result4, "无效字符串转bool默认值失败");
//        }

//        #endregion

//        #region 字符串操作测试

//        [TestMethod]
//        public void Truncate_ReturnsCorrectResult()
//        {
//            // 执行：截取字符串，超出长度时添加省略号
//            string result1 = StringHelper.Truncate("Hello World", 5);
//            string result2 = StringHelper.Truncate("Hello", 5);
//            string result3 = StringHelper.Truncate("Hello", 3, "..");
//            string resultEmpty = StringHelper.Truncate(string.Empty, 5);
//            string resultNull = StringHelper.Truncate(null, 5);

//            // 断言：截取结果应正确
//            Assert.AreEqual("He...", result1, "字符串截取失败");
//            Assert.AreEqual("Hello", result2, "未超出长度的字符串截取失败");
//            Assert.AreEqual("He..", result3, "自定义省略号的字符串截取失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串截取失败");
//            Assert.IsNull(resultNull, "null字符串截取失败");
//        }

//        [TestMethod]
//        public void RemoveWhiteSpace_ReturnsCorrectResult()
//        {
//            // 执行：移除字符串中的所有空白字符
//            string result1 = StringHelper.RemoveWhiteSpace("Hello World");
//            string result2 = StringHelper.RemoveWhiteSpace("  Hello  World  ");
//            string result3 = StringHelper.RemoveWhiteSpace("Hello\tWorld\n");
//            string resultEmpty = StringHelper.RemoveWhiteSpace(string.Empty);
//            string resultNull = StringHelper.RemoveWhiteSpace(null);

//            // 断言：移除空白字符后的字符串应正确
//            Assert.AreEqual("HelloWorld", result1, "移除空格失败");
//            Assert.AreEqual("HelloWorld", result2, "移除首尾空格失败");
//            Assert.AreEqual("HelloWorld", result3, "移除制表符和换行符失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串移除空白字符失败");
//            Assert.IsNull(resultNull, "null字符串移除空白字符失败");
//        }

//        [TestMethod]
//        public void ReplaceMultiple_ReturnsCorrectResult()
//        {
//            // 执行：替换字符串中的多个子字符串
//            string result1 = StringHelper.ReplaceMultiple("Hello World", ("Hello", "Hi"), ("World", "There"));
//            string result2 = StringHelper.ReplaceMultiple("Hello World", ("Hello", "Hi"));
//            string resultEmpty = StringHelper.ReplaceMultiple(string.Empty, ("Hello", "Hi"));
//            string resultNull = StringHelper.ReplaceMultiple(null, ("Hello", "Hi"));

//            // 断言：替换结果应正确
//            Assert.AreEqual("Hi There", result1, "多个子字符串替换失败");
//            Assert.AreEqual("Hi World", result2, "单个子字符串替换失败");
//            Assert.AreEqual(string.Empty, resultEmpty, "空字符串替换失败");
//            Assert.IsNull(resultNull, "null字符串替换失败");
//        }

//        [TestMethod]
//        public void CalculateSimilarity_ReturnsCorrectResult()
//        {
//            // 执行：计算两个字符串的相似度
//            double result1 = StringHelper.CalculateSimilarity("Hello", "Hello");
//            double result2 = StringHelper.CalculateSimilarity("Hello", "World");
//            double result3 = StringHelper.CalculateSimilarity("Hello", "Hello World");
//            double result4 = StringHelper.CalculateSimilarity("Hello", string.Empty);
//            double result5 = StringHelper.CalculateSimilarity(string.Empty, null);

//            // 断言：相似度计算结果应正确
//            Assert.AreEqual(1.0, result1, 0.01, "完全相同的字符串相似度计算失败");
//            Assert.IsTrue(result2 < 0.5, "完全不同的字符串相似度计算失败");
//            Assert.IsTrue(result3 > 0.5, "相似字符串相似度计算失败");
//            Assert.AreEqual(0.0, result4, 0.01, "与空字符串的相似度计算失败");
//            Assert.AreEqual(0.0, result5, 0.01, "空字符串与null的相似度计算失败");
//        }

//        #endregion
//    }
//}