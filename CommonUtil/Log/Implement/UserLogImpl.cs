using CommonUtil.Log.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonUtil.Log.Implement
{
    public class UserLogImpl : ILog
    {
        // 私有构造函数，防止外部实例化
        private UserLogImpl()
        {
            // 初始化日志目录
            LogInit();
        }

        //使用Lazy<T>的延时加载单例模式，就是在创建该对象时才会实例化它，且是线程安全的。程序启动时不会立即创建实例，只有在第一次访问Instance属性时才会创建实例。
        private static readonly Lazy<UserLogImpl> _instance = new Lazy<UserLogImpl>(() => new UserLogImpl() { });
        public static UserLogImpl Instance => _instance.Value;

        #region 字段

        // 日志目录前缀，默认为应用程序根目录下的 Log 文件夹
        private string logPrefix = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";
        #endregion

        public string LogPrefix
        {
            get => logPrefix;
            set
            {
                if (value != null)
                {
                    logPrefix = value;
                }
            }

        }

        public string FATALDir { get; set; }
        public string ERRORDir { get; set; }
        public string WARNDir { get; set; }
        public string INFODir { get; set; }
        public string DEBUGDir { get; set; }
        public string TRACEDir { get; set; }
        public string ALLDir { get; set; }

        public void LogInit()
        {
            FATALDir = logPrefix + "FATAL\\";
            ERRORDir = logPrefix + "ERROR\\";
            WARNDir = logPrefix + "WARN\\";
            INFODir = logPrefix + "INFO\\";
            DEBUGDir = logPrefix + "DEBUG\\";
            TRACEDir = logPrefix + "TRACE\\";
            ALLDir = logPrefix + "ALL\\";
        }

        public void LogWrite(LogLevel level, string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd$&$HH:mm:ss:fff");
            int threadId = Thread.CurrentThread.ManagedThreadId;
            string threadName = Thread.CurrentThread.Name ?? "Unknown";
            string logContent = $"{timestamp}$&${level}$&${threadId}:{threadName}$&${message}{Environment.NewLine}";
            // 获取对应级别的日志文件路径
            string targetPath = GetLogPathByLevel(level);
            // 获取ALL级别的日志文件路径
            string allpath = GetLogPathByLevel(LogLevel.ALL);

            EnsureDirectoryExists(targetPath);

            try
            {
                // 写入日志到对应文件（追加模式）
                File.AppendAllText(targetPath, logContent);

                // 同时写入ALL日志（如果不是ALL级别本身）
                if (level != LogLevel.ALL)
                {
                    EnsureDirectoryExists(allpath);
                    File.AppendAllText(allpath, logContent);
                }
            }
            catch (Exception ex)
            {
                // 日志写入失败时可根据需求处理（如输出到控制台）
                Console.WriteLine($"日志写入失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 根据日志级别获取对应的文件路径
        /// </summary>
        private string GetLogPathByLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.FATAL:
                    return FATALDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                case LogLevel.ERROR:
                    return ERRORDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                case LogLevel.WARN:
                    return WARNDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                case LogLevel.INFO:
                    return INFODir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                case LogLevel.DEBUG:
                    return DEBUGDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                case LogLevel.TRACE:
                    return TRACEDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
                default:
                    return ALLDir + DateTime.Now.ToString("yyyyMMdd") + ".log";
            }
        }

        /// <summary>
        /// 确保日志文件所在目录存在，不存在则创建
        /// </summary>
        /// <param name="filePath">判断的文件路经</param>
        private void EnsureDirectoryExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            //获取文件所在目录
            string directory = Path.GetDirectoryName(filePath);
            //判断目录是否存在，不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Dispose()
        {
            
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~UserLogImpl()
        {
            Dispose();
        }
    }
}
