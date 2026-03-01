using CommonUtil;
using CommonUtil.CSVOrDataTable;
using CommonUtil.Ini;
using CommonUtil.XML;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace AllInOneDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CommonUtil All In One Demo ===\n");

            // 1. 字符串工具演示
            DemoStringHelper();

            // 2. 日期时间工具演示
            DemoDateTimeHelper();

            // 3. 加密解密工具演示
            DemoCryptoHelper();

            // 4. 文件操作工具演示
            DemoFileHelper();

            // 5. 验证工具演示
            DemoValidationHelper();

            // 6. CSV操作演示
            DemoCSVHelper();

            // 7. XML操作演示
            DemoXmlHelper();

            // 8. INI操作演示
            DemoIniHelper();

            Console.WriteLine("\n=== Demo Complete ===");
            Console.ReadLine();
        }

        #region 字符串工具演示
        private static void DemoStringHelper()
        {
            Console.WriteLine("1. 字符串工具演示");
            Console.WriteLine("-------------------");

            string testString = "hello, world!";
            Console.WriteLine($"原始字符串: {testString}");
            Console.WriteLine($"首字母大写: {StringHelper.FirstLetterToUpper(testString)}");
            Console.WriteLine($"驼峰命名: {StringHelper.ToCamelCase("hello_world")}");
            Console.WriteLine($"下划线命名: {StringHelper.ToUnderlineCase("HelloWorld")}");
            Console.WriteLine($"是否有效邮箱: {StringHelper.IsValidEmail("test@example.com")}");
            Console.WriteLine($"截断字符串: {StringHelper.Truncate("This is a long string", 10)}");
            Console.WriteLine($"去除空白: '{StringHelper.TrimAllWhitespace("  a  b  c  ")}'");
            Console.WriteLine($"重复字符串: {StringHelper.Repeat("AB", 3)}");
            Console.WriteLine();
        }
        #endregion

        #region 日期时间工具演示
        private static void DemoDateTimeHelper()
        {
            Console.WriteLine("2. 日期时间工具演示");
            Console.WriteLine("-------------------");

            DateTime now = DateTime.Now;
            Console.WriteLine($"当前时间: {now}");
            Console.WriteLine($"格式化日期: {DateTimeHelper.FormatDate(now)}");
            Console.WriteLine($"格式化时间: {DateTimeHelper.FormatTime(now)}");
            Console.WriteLine($"添加5天: {DateTimeHelper.AddDays(now, 5)}");
            Console.WriteLine($"是否今天: {DateTimeHelper.IsToday(now)}");
            Console.WriteLine($"时间戳: {DateTimeHelper.ToTimestamp(now)}");
            Console.WriteLine($"本周一: {DateTimeHelper.GetMondayOfCurrentWeek()}");
            Console.WriteLine($"本月第一天: {DateTimeHelper.GetFirstDayOfCurrentMonth()}");
            Console.WriteLine($"中文星期: {DateTimeHelper.GetChineseDayOfWeek()}");
            Console.WriteLine();
        }
        #endregion

        #region 加密解密工具演示
        private static void DemoCryptoHelper()
        {
            Console.WriteLine("3. 加密解密工具演示");
            Console.WriteLine("-------------------");

            string testString = "Hello, CommonUtil!";
            Console.WriteLine($"原始字符串: {testString}");
            
            // MD5加密
            string md5Hash = CryptoHelper.Md5(testString);
            Console.WriteLine($"MD5哈希: {md5Hash}");

            // SHA256加密
            string sha256Hash = CryptoHelper.Sha256(testString);
            Console.WriteLine($"SHA256哈希: {sha256Hash}");

            // Base64编码解码
            string base64 = CryptoHelper.Base64Encode(testString);
            string base64Decoded = CryptoHelper.Base64Decode(base64);
            Console.WriteLine($"Base64编码: {base64}");
            Console.WriteLine($"Base64解码: {base64Decoded}");

            // AES加密解密
            string aesKey = "1234567890123456";
            string aesIv = "1234567890123456";
            string encrypted = CryptoHelper.AesEncrypt(testString, aesKey, aesIv);
            string decrypted = CryptoHelper.AesDecrypt(encrypted, aesKey, aesIv);
            Console.WriteLine($"AES加密: {encrypted}");
            Console.WriteLine($"AES解密: {decrypted}");
            Console.WriteLine();
        }
        #endregion

        #region 文件操作工具演示
        private static void DemoFileHelper()
        {
            Console.WriteLine("4. 文件操作工具演示");
            Console.WriteLine("-------------------");

            string tempDir = Path.Combine(Path.GetTempPath(), "CommonUtilDemo");
            string tempFile = Path.Combine(tempDir, "test.txt");
            string testContent = "Hello, FileHelper!\nThis is a test file.";

            // 创建目录
            FileHelper.CreateDirectory(tempDir);
            Console.WriteLine($"创建目录: {tempDir}");

            // 写入文件
            FileHelper.WriteText(tempFile, testContent);
            Console.WriteLine($"写入文件: {tempFile}");

            // 读取文件
            string content = FileHelper.ReadText(tempFile);
            Console.WriteLine($"读取文件内容: {content.Replace("\n", " | ")}");

            // 获取文件大小
            long size = FileHelper.GetFileSize(tempFile);
            Console.WriteLine($"文件大小: {size} 字节");

            // 追加内容
            FileHelper.AppendText(tempFile, "\nAppended content.");
            Console.WriteLine("追加内容到文件");

            // 读取所有行
            var lines = FileHelper.ReadAllLines(tempFile);
            Console.WriteLine($"文件行数: {lines.Count}");

            // 删除文件和目录
            FileHelper.DeleteFile(tempFile);
            FileHelper.DeleteDirectory(tempDir);
            Console.WriteLine("清理临时文件和目录");
            Console.WriteLine();
        }
        #endregion

        #region 验证工具演示
        private static void DemoValidationHelper()
        {
            Console.WriteLine("5. 验证工具演示");
            Console.WriteLine("-------------------");

            // 邮箱验证
            Console.WriteLine($"邮箱验证 - test@example.com: {ValidationHelper.IsValidEmail("test@example.com")}");
            Console.WriteLine($"邮箱验证 - test@example: {ValidationHelper.IsValidEmail("test@example")}");

            // 手机号验证
            Console.WriteLine($"手机号验证 - 13812345678: {ValidationHelper.IsValidPhone("13812345678")}");
            Console.WriteLine($"手机号验证 - 12345678901: {ValidationHelper.IsValidPhone("12345678901")}");

            // 身份证验证
            Console.WriteLine($"身份证验证 - 110101199001011234: {ValidationHelper.IsValidIdCard("110101199001011234")}");

            // 密码强度验证
            Console.WriteLine($"弱密码 - '123': {ValidationHelper.IsWeakPassword("123")}");
            Console.WriteLine($"中等密码 - 'Test123': {ValidationHelper.IsMediumPassword("Test123")}");
            Console.WriteLine($"强密码 - 'Test@123456789': {ValidationHelper.IsStrongPassword("Test@123456789")}");

            // 数字验证
            Console.WriteLine($"有效数字 - '123.45': {ValidationHelper.IsValidNumber("123.45")}");
            Console.WriteLine();
        }
        #endregion

        #region CSV操作演示
        private static void DemoCSVHelper()
        {
            Console.WriteLine("6. CSV操作演示");
            Console.WriteLine("-------------------");

            string csvPath = Path.Combine(Path.GetTempPath(), "test.csv");
            
            // 创建测试数据
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Age");
            dt.Columns.Add("City");
            dt.Rows.Add("张三", 25, "北京");
            dt.Rows.Add("李四", 30, "上海");
            dt.Rows.Add("王五", 35, "广州");

            // 写入CSV文件
            CsvOrDataTableHelper.WriteDataTableToCsv(csvPath, dt);
            Console.WriteLine($"写入CSV文件: {csvPath}");

            // 读取CSV文件
            DataTable readDt = CsvOrDataTableHelper.ReadCsvToDataTable(csvPath);
            Console.WriteLine($"读取CSV文件，行数: {readDt.Rows.Count}");
            
            // 显示数据
            foreach (DataRow row in readDt.Rows)
            {
                Console.WriteLine($"  {row["Name"]} | {row["Age"]} | {row["City"]}");
            }

            // 追加数据
            DataTable appendDt = new DataTable();
            appendDt.Columns.Add("Name");
            appendDt.Columns.Add("Age");
            appendDt.Columns.Add("City");
            appendDt.Rows.Add("赵六", 40, "深圳");
            CsvOrDataTableHelper.AppendDataTableToCsv(csvPath, appendDt);
            Console.WriteLine("追加一行数据到CSV文件");

            // 清理临时文件
            File.Delete(csvPath);
            Console.WriteLine();
        }
        #endregion

        #region XML操作演示
        private static void DemoXmlHelper()
        {
            Console.WriteLine("7. XML操作演示");
            Console.WriteLine("-------------------");

            string xmlPath = Path.Combine(Path.GetTempPath(), "test.xml");
            
            // 创建测试XML
            string xmlContent = @"<root>
  <person id=""1"">
    <name>张三</name>
    <age>25</age>
  </person>
  <person id=""2"">
    <name>李四</name>
    <age>30</age>
  </person>
</root>";

            File.WriteAllText(xmlPath, xmlContent, Encoding.UTF8);
            Console.WriteLine($"创建XML文件: {xmlPath}");

            // 读取XML节点
            string name = XmlHelper.GetNodeText(xmlPath, "/root/person[@id='1']/name");
            Console.WriteLine($"读取节点值: /root/person[@id='1']/name = {name}");

            // 修改XML节点
            XmlHelper.SetNodeText(xmlPath, "/root/person[@id='1']/age", "26");
            string newAge = XmlHelper.GetNodeText(xmlPath, "/root/person[@id='1']/age");
            Console.WriteLine($"修改节点值: /root/person[@id='1']/age = {newAge}");

            // 清理临时文件
            File.Delete(xmlPath);
            Console.WriteLine();
        }
        #endregion

        #region INI操作演示
        private static void DemoIniHelper()
        {
            Console.WriteLine("8. INI操作演示");
            Console.WriteLine("-------------------");

            string iniPath = Path.Combine(Path.GetTempPath(), "test.ini");
            
            // 写入INI文件
            IniHelper.WriteValue(iniPath, "Section1", "Key1", "Value1");
            IniHelper.WriteValue(iniPath, "Section1", "Key2", "123");
            IniHelper.WriteValue(iniPath, "Section2", "Key1", "ValueA");
            Console.WriteLine($"写入INI文件: {iniPath}");

            // 读取INI值
            string value1 = IniHelper.ReadValue(iniPath, "Section1", "Key1");
            string value2 = IniHelper.ReadValue(iniPath, "Section1", "Key2");
            Console.WriteLine($"读取Section1\\Key1: {value1}");
            Console.WriteLine($"读取Section1\\Key2: {value2}");
            Console.WriteLine($"读取不存在的键: '{IniHelper.ReadValue(iniPath, "Section1", "KeyNotExist", "Default")}'");

            // 删除键
            IniHelper.DeleteKey(iniPath, "Section1", "Key2");
            Console.WriteLine("删除Section1\\Key2");

            // 读取删除后的键
            string deletedValue = IniHelper.ReadValue(iniPath, "Section1", "Key2", "Not Found");
            Console.WriteLine($"读取已删除的键: '{deletedValue}'");

            // 清理临时文件
            File.Delete(iniPath);
            Console.WriteLine();
        }
        #endregion
    }
}