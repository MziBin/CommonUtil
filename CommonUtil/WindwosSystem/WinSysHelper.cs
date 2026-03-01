using CommonUtil.WindwosSystem.Interface;
using CommonUtil.WindwosSystem.Implement;

namespace CommonUtil.WindwosSystem
{
    /// <summary>
    /// Windows系统操作助手类，提供静态方法简化Windows系统操作
    /// </summary>
    public static class WinSysHelper
    {
        private static readonly IWinSys _winSysHandler = WinSysImpl.Instance;

        /// <summary>
        /// 获取当前进程的内存使用情况，单位为字节
        /// </summary>
        /// <returns>前进程的内存使用情况</returns>
        public static long GetCurrentProcessMemoryUsage()
        {
            return _winSysHandler.GetCurrentProcessMemoryUsage();
        }

        /// <summary>
        /// 获取系统总内存和可用内存，单位为字节
        /// </summary>
        /// <returns></returns>
        public static (long totalMemory, long freeMemory) GetSystemMemoryInfo()
        {
            return _winSysHandler.GetSystemMemoryInfo();
        }

        /// <summary>
        /// 获取 CPU 使用率（0-100 的整数），这里使用异步方法以避免阻塞调用线程
        /// 调用时注意使用 await 关键字，否则调用改方法的页面会卡死。
        /// </summary>
        /// <returns></returns>
        public static System.Threading.Tasks.Task<int> GetCpuUsage()
        {
            return _winSysHandler.GetCpuUsage();
        }
    }
}