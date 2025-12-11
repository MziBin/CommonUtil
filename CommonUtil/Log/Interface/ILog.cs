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
