using CommonUtil.Log.Implement;
using CommonUtil.Log.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace MSTests
{
    [TestClass]
    public class LogTests
    {
        private string _logDir;

        [TestInitialize]
        public void TestInitialize()
        {
            _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (Directory.Exists(_logDir))
            {
                Directory.Delete(_logDir, true);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // 清理日志目录
            // 注意：由于日志文件可能被占用，这里可能无法完全删除，仅作尝试
        }

        [TestMethod]
        public void UserLog_LogWrite_CreatesLogFile()
        {
            var log = UserLogImpl.Instance;
            log.LogWrite(LogLevel.INFO, "Test Message");

            // 等待文件写入
            Thread.Sleep(100);

            string expectedFile = Path.Combine(_logDir, "INFO", DateTime.Now.ToString("yyyyMMdd") + ".log");
            Assert.IsTrue(File.Exists(expectedFile), "日志文件未创建");
            
            string content = File.ReadAllText(expectedFile);
            Assert.IsTrue(content.Contains("Test Message"), "日志内容不包含预期消息");
        }

        [TestMethod]
        public void BZLog_LogWrite_CreatesLogFile()
        {
            var log = BZLogImpl.Instance;
            log.LogPrefix = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BZLogTest.log");
            // BZLogImpl 需要手动初始化
            log.LogInit();
            log.LogWrite(LogLevel.INFO, "Test Message");

            // 等待异步写入
            Thread.Sleep(200);

            Assert.IsTrue(File.Exists(log.LogPrefix), "日志文件未创建");

            string content = File.ReadAllText(log.LogPrefix);
            Assert.IsTrue(content.Contains("Test Message"), "日志内容不包含预期消息");
        }
    }
}