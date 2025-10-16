using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.Log.Interface
{
    /// <summary>
    /// 用于定义日志级别的枚举类型
    /// </summary>
    public enum LogLevel
    {
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG,
        TRACE,
        ALL
    }

    public interface ILog
    {
        /// <summary>
        /// 通用日志目录前缀
        /// </summary>
        string LogPrefix { get; set; }

        /// <summary>
        /// 用于初始化日志系统的方法
        /// </summary>
        void LogInit();

        /// <summary>
        /// 严重错误，导致程序无法继续运行，的路径
        /// </summary>
        string FATALDir { get; set; }
        /// <summary>
        /// 错误，程序运行中出现的问题，的路径
        /// </summary>
        string ERRORDir { get; set; }
        /// <summary>
        /// 警告，可能会导致问题的情况，的路径
        /// </summary>
        string WARNDir { get; set; }
        /// <summary>
        /// 信息，程序运行的常规信息，的路径
        /// </summary>
        string INFODir { get; set; }
        /// <summary>
        /// 调试，调试程序时使用的信息，的路径
        /// </summary>
        string DEBUGDir { get; set; }
        /// <summary>
        /// 追踪，最详细的信息，一般只在开发时使用，的路径
        /// </summary>
        string TRACEDir { get; set; }
        /// <summary>
        /// 记录所有级别的日志的路径
        /// </summary>
        string ALLDir { get; set; }

        /// <summary>
        /// 用于写入日志信息的方法
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        void LogWrite(LogLevel level, string message);

        /// <summary>
        /// 用于释放日志系统资源的方法
        /// </summary>
        void Dispose();
    }
}
