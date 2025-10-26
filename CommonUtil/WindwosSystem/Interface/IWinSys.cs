using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * windwos系统的一些操作
 * 在 C# 中获取 CPU 使用率可以通过 System.Diagnostics 命名空间下的 PerformanceCounter 类实现，
 * 该类能访问系统性能计数器（如 CPU 使用率、内存占用等）。
 * 以下是具体实现方法及说明：
 */
namespace CommonUtil.WindwosSystem.Interface
{
    
    public interface IWinSys
    {
        /// <summary>
        /// 获取当前进程的内存使用情况，单位为字节
        /// </summary>
        /// <returns>前进程的内存使用情况</returns>
        long GetCurrentProcessMemoryUsage();
        /// <summary>
        /// 获取系统总内存和可用内存，单位为字节
        /// </summary>
        /// <returns></returns>
        (long totalMemory, long freeMemory) GetSystemMemoryInfo();
        /// <summary>
        //// 获取 CPU 使用率（0-100 的整数），这里使用异步方法以避免阻塞调用线程
        /// 调用时注意使用 await 关键字，否则调用改方法的页面会卡死。
        /// </summary>
        /// <returns></returns>
        Task<int> GetCpuUsage();
    }
}
