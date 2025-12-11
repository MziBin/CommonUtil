using CommonUtil.Log.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.Log.Implement
{

    //通过多线程后台一直读取队列写入日志文件
    //每次的内容和文件路径都压入队列
    public class BZLogImpl : ILog
    {
        //锁
        private static readonly object logLock = new object();

        //日志队列
        //private static readonly Queue<(string filePath, string content)> logQueue = new Queue<(string, string)>();//可以用元组
        //这里用类更清晰
        private static readonly Queue<LogItem> logQueue = new Queue<LogItem>();

        private static BZLogImpl instance = null;

        private BZLogImpl() { }

        public static BZLogImpl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BZLogImpl();
                }
                return instance;
            }
        }

        //初始化方法，启动日志写入线程，程序启动时调用一次
        public void LogInit()
        {
            //启动日志写入线程
            Task.Run(() => LogWriter());
        }


        //日志消息加入的方法
        public static void EnqueueLog(string filePath, string content)
        {
            lock (logLock)
            {
                LogItem logItem = new LogItem(filePath, content);

                logQueue.Enqueue(logItem);
                //通知写入线程有新日志
                System.Threading.Monitor.Pulse(logLock);
            }
        }

        //日志写入线程
        public static void LogWriter()
        {
            while (true)
            {
                LogItem logItem;
                lock (logLock)
                {
                    while (logQueue.Count == 0)
                    {
                        //等待新日志
                        System.Threading.Monitor.Wait(logLock);
                    }
                    logItem = logQueue.Dequeue();
                }
                //写入日志文件
                try
                {
                    File.AppendAllText(logItem.FilePath, logItem.Content + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    //处理写入异常，例如记录到备用日志文件或控制台输出
                    Console.WriteLine($"日志写入失败: {ex.Message}");
                }
            }
        }

        //日志对象，方便写入队列存储
        class LogItem
        {
            public string FilePath { get; set; }
            public string Content { get; set; }
            public LogItem(string filePath, string content)
            {
                FilePath = filePath;
                Content = content;
            }
        }

        public string LogPrefix { get; set; }

        public void LogWrite(LogLevel level, string message)
        {
            EnqueueLog(LogPrefix, message);
        }

        public void Dispose()
        {
            // 实现IDisposable接口
        }
    }
}
