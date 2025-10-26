using CommonUtil.WindwosSystem.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinSysDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 动态订阅Loaded事件
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 500; // 设置时间间隔为1秒
            timer.Elapsed += (o, r) => { GetWinSysData(); };
            timer.Start();
        }

        private void GetWinSysData()
        {

            // 将 Action 改为 async Action，允许内部使用 await
            CpuUsage.Dispatcher.BeginInvoke(new Action(async () =>
            {
                // 用 await 异步等待 GetCpuUsage()，不阻塞 UI 线程
                int cpuUsage = await WinSysImpl.Instance.GetCpuUsage();
                CpuUsage.Text = cpuUsage.ToString() + " %";
            }));
            freeMemory.Dispatcher.BeginInvoke(new Action(() =>
            {
                freeMemory.Text = (WinSysImpl.Instance.GetSystemMemoryInfo().freeMemory / (1024 * 1024)).ToString() + " MB";
            }));
            totalMemory.Dispatcher.BeginInvoke(new Action(() =>
            {
                totalMemory.Text = (WinSysImpl.Instance.GetSystemMemoryInfo().totalMemory / (1024 * 1024)).ToString() + " MB";
            }));
            appMemory.Dispatcher.BeginInvoke(new Action(() =>
            {
                appMemory.Text = (WinSysImpl.Instance.GetCurrentProcessMemoryUsage() / (1024 * 1024)).ToString() + " MB";
            }));
        }
    }
}
