using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtil.MillisecondTimer;

namespace EnhancedMillisecondTimerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 基本计时测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBasicTest_Click(object sender, EventArgs e)
        {
            ClearResult();
            AppendResult("=== 基本计时测试 ===");

            using (var timer = new EnhancedMillisecondTimer("基本测试计时器"))
            {
                // 显示初始状态
                AppendResult(timer.ToString());

                // 开始计时
                AppendResult("\n开始计时...");
                timer.Start();

                // 执行需要计时的操作
                System.Threading.Thread.Sleep(100);

                // 显示中间状态
                AppendResult($"\n计时中状态: {timer.ToString()}");

                // 停止计时
                long elapsed = timer.Stop();
                AppendResult($"\n计时结束，耗时: {elapsed}ms");

                // 显示最终状态
                AppendResult($"\n最终状态: {timer.ToString()}");

                // 显示各属性值
                AppendResult($"\n上一次完整分段时间: {timer.LastFullSegmentMs}ms");
                AppendResult($"累计完整分段时间: {timer.TotalFullSegmentsMs}ms");
                AppendResult($"是否正在运行: {timer.IsSegmentRunning}");
                AppendResult($"嵌套深度: {timer.SegmentNestingDepth}");
            }

            AppendResult("\n=== 基本计时测试完成 ===\n");
        }

        /// <summary>
        /// 嵌套计时测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNestedTest_Click(object sender, EventArgs e)
        {
            ClearResult();
            AppendResult("=== 嵌套计时测试 ===");

            using (var timer = new EnhancedMillisecondTimer("嵌套测试计时器"))
            {
                // 开始外层计时
                AppendResult("\n开始外层计时...");
                timer.Start();
                System.Threading.Thread.Sleep(500);

                // 开始内层计时
                AppendResult("\n开始内层计时...");
                timer.Start();
                System.Threading.Thread.Sleep(300);

                // 停止内层计时
                long innerElapsed = timer.Stop();
                AppendResult($"\n内层计时结束，耗时: {innerElapsed}ms");
                AppendResult($"当前状态: {timer.ToString()}");

                // 继续外层操作
                System.Threading.Thread.Sleep(500);

                // 停止外层计时
                long outerElapsed = timer.Stop();
                AppendResult($"\n外层计时结束，耗时: {outerElapsed}ms");
                AppendResult($"当前状态: {timer.ToString()}");

                // 显示各属性值
                AppendResult($"\n上一次完整分段时间: {timer.LastFullSegmentMs}ms");
                AppendResult($"累计完整分段时间: {timer.TotalFullSegmentsMs}ms");
                AppendResult($"是否正在运行: {timer.IsSegmentRunning}");
                AppendResult($"嵌套深度: {timer.SegmentNestingDepth}");

                // 再次进行一轮完整计时
                AppendResult("\n\n开始第二轮完整计时...");
                timer.Start();
                System.Threading.Thread.Sleep(150);
                long secondElapsed = timer.Stop();
                AppendResult($"\n第二轮计时结束，耗时: {secondElapsed}ms");
                AppendResult($"当前状态: {timer.ToString()}");

                // 显示累计时间
                AppendResult($"\n上一次完整分段时间: {timer.LastFullSegmentMs}ms");
                AppendResult($"\n累计完整分段时间: {timer.TotalFullSegmentsMs}ms");
            }

            AppendResult("\n=== 嵌套计时测试完成 ===\n");
        }

        /// <summary>
        /// MeasureSegment测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMeasureTest_Click(object sender, EventArgs e)
        {
            ClearResult();
            AppendResult("=== MeasureSegment测试 ===");

            using (var timer = new EnhancedMillisecondTimer("Measure测试计时器"))
            {
                // 使用MeasureSegment方法测量简单操作
                long elapsed1 = timer.MeasureSegment(() =>
                {
                    System.Threading.Thread.Sleep(100);
                });
                AppendResult($"\n简单操作耗时: {elapsed1}ms");

                // 使用MeasureSegment方法测量复杂操作
                long elapsed2 = timer.MeasureSegment(() =>
                {
                    // 模拟复杂计算
                    int sum = 0;
                    for (int i = 0; i < 1000000; i++)
                    {
                        sum += i;
                    }
                    System.Threading.Thread.Sleep(50);
                });
                AppendResult($"\n复杂操作耗时: {elapsed2}ms");

                // 显示累计时间
                AppendResult($"\n累计完整分段时间: {timer.TotalFullSegmentsMs}ms");

                // 显示当前状态
                AppendResult($"\n当前状态: {timer.ToString()}");
            }

            AppendResult("\n=== MeasureSegment测试完成 ===\n");
        }

        /// <summary>
        /// 清空结果显示
        /// </summary>
        private void ClearResult()
        {
            txtResult.Clear();
        }

        /// <summary>
        /// 追加结果到显示区域
        /// </summary>
        /// <param name="text">要显示的文本</param>
        private void AppendResult(string text)
        {
            txtResult.AppendText(text + Environment.NewLine);
            txtResult.ScrollToCaret();
        }
    }
}
