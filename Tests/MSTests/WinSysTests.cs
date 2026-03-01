using CommonUtil.WindwosSystem.Implement;
using CommonUtil.WindwosSystem.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace MSTests
{
    [TestClass]
    public class WinSysTests
    {
        private IWinSys _winSys;

        [TestInitialize]
        public void TestInitialize()
        {
            _winSys = WinSysImpl.Instance;
        }

        [TestMethod]
        public void GetCurrentProcessMemoryUsage_ReturnsPositiveValue()
        {
            // 执行：获取当前进程内存使用情况
            long memoryUsage = _winSys.GetCurrentProcessMemoryUsage();

            // 断言：内存使用情况应为正数
            Assert.IsTrue(memoryUsage >= 0, "当前进程内存使用情况应为正数");
        }

        [TestMethod]
        public void GetSystemMemoryInfo_ReturnsPositiveValues()
        {
            // 执行：获取系统内存信息
            var (totalMemory, freeMemory) = _winSys.GetSystemMemoryInfo();

            // 断言：总内存和可用内存应为正数
            Assert.IsTrue(totalMemory > 0, "系统总内存应为正数");
            Assert.IsTrue(freeMemory >= 0, "系统可用内存应为正数");
            Assert.IsTrue(freeMemory <= totalMemory, "系统可用内存应小于或等于总内存");
        }

        [TestMethod]
        public async Task GetCpuUsage_ReturnsValidValue()
        {
            // 执行：获取CPU使用率
            int cpuUsage = await _winSys.GetCpuUsage();

            // 断言：CPU使用率应在0-100之间
            Assert.IsTrue(cpuUsage >= 0 && cpuUsage <= 100, "CPU使用率应在0-100之间");
        }
    }
}