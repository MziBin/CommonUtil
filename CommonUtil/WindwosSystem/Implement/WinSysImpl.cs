using CommonUtil.WindwosSystem.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil.WindwosSystem.Implement
{
    /// <summary>
    /// Windows 系统信息获取实现类
    /// </summary>
    public class WinSysImpl : IWinSys
    {
        // 单例实例
        private static WinSysImpl instance;

        private WinSysImpl() { }

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static WinSysImpl Instance 
        {
            get
            {
                if (instance == null)
                {
                    return new WinSysImpl();
                }
                else
                {
                    return instance;
                }
            }
        } 

        // 用于获取系统内存信息的 Windows API 结构体
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MEMORYSTATUSEX
        {
            public uint dwLength;          // 结构体大小（必须初始化）
            public uint dwMemoryLoad;      // 内存使用率（0-100）
            public ulong ullTotalPhys;     // 总物理内存（字节）
            public ulong ullAvailPhys;     // 可用物理内存（字节）
            public ulong ullTotalPageFile; // 总页文件大小
            public ulong ullAvailPageFile; // 可用页文件大小
            public ulong ullTotalVirtual;  // 总虚拟内存
            public ulong ullAvailVirtual;  // 可用虚拟内存
            public ulong ullAvailExtendedVirtual; // 扩展虚拟内存
        }

        // 引入 Windows API：获取系统内存信息
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        // CPU 使用率计数器（延迟初始化，避免频繁创建）
        private PerformanceCounter _cpuCounter;


        /// <summary>
        /// 获取当前进程的内存使用情况（字节）
        /// </summary>
        public long GetCurrentProcessMemoryUsage()
        {
            // 获取当前进程实例
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                // WorkingSet64：当前进程使用的物理内存总量（字节）
                return currentProcess.WorkingSet64;
            }
        }


        /// <summary>
        /// 获取系统总内存和可用内存（字节）
        /// </summary>
        public (long totalMemory, long freeMemory) GetSystemMemoryInfo()
        {
            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
            memStatus.dwLength = (uint)Marshal.SizeOf(memStatus); // 必须初始化结构体大小

            // 调用 Windows API 获取内存信息
            if (GlobalMemoryStatusEx(ref memStatus))
            {
                // 转换为 long 类型（与返回值匹配）
                return (
                    totalMemory: (long)memStatus.ullTotalPhys,//总内存
                    freeMemory: (long)memStatus.ullAvailPhys//可用内存
                );
            }

            // 若 API 调用失败，返回默认值（实际环境中可根据需求抛异常）
            return (0, 0);
        }


        /// <summary>
        /// 获取 CPU 使用率（0-100 的整数），这里使用异步方法以避免阻塞调用线程
        /// 调用时注意使用 await 关键字，否则调用改方法的页面会卡死。
        /// 案例：用 await 异步等待 GetCpuUsage()，不阻塞 UI 线程
        /// int cpuUsage = await WinSysImpl.Instance.GetCpuUsage();
        /// CpuUsage.Text = cpuUsage.ToString() + " %";
        /// </summary>
        public async Task<int> GetCpuUsage()
        {
            // 延迟初始化 CPU 计数器（避免程序启动时的性能开销）
            if (_cpuCounter == null)
            {
                _cpuCounter = new PerformanceCounter(
                    "Processor",       // 计数器类别：处理器
                    "% Processor Time",// 计数器名称：CPU 使用率
                    "_Total"           // 实例：所有核心总和
                );
                // 首次调用 NextValue() 通常返回 0，需先触发一次
                _cpuCounter.NextValue();
            }

            // 等待计数器更新（200ms 足够获取准确值），这里是用了异步等待，避免阻塞调用线程
            await Task.Delay(200);

            // 获取使用率并转换为整数（限制在 0-100 之间）
            float usage = _cpuCounter.NextValue();
            //将CPU使用率转化为百分比整数
            return (int)Math.Min(Math.Max(usage, 0), 100);
        }
    }
}
