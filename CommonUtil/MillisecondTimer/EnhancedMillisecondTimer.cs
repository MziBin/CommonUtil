using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CommonUtil.MillisecondTimer
{
    /// <summary>
    /// 支持嵌套计时的增强型毫秒计时器
    /// 每次Stop调用会返回当前层级的计时结果
    /// 
    /// 使用方法：
    /// 1.创建计时器对象
    /// 2.调用start开始，结束时调用stop即可。
    /// 3.想测量一个方法耗时时间，也可以调用MeasureSegment方法，里面传递委托。
    /// 
    /// 
    /// 功能特性：
    /// 1. 支持多层嵌套计时，精确测量不同层级代码块的执行时间
    /// 2. 提供多种计时方式：手动Start/Stop、Action委托测量、嵌套计时
    /// 3. 实时获取计时状态：运行状态、嵌套深度、不同层级的已用时间
    /// 4. 支持多个完整分段计时，可累计所有分段的总时间
    /// 5. 实现IDisposable接口，确保资源正确释放
    /// 
    /// 使用示例：
    /// <code>
    /// // 基本使用方式
    /// using (var timer = new EnhancedMillisecondTimer())
    /// {
    ///     timer.Start();
    ///     // 执行需要计时的操作
    ///     System.Threading.Thread.Sleep(100);
    ///     long elapsed = timer.Stop();
    ///     Console.WriteLine($"操作耗时: {elapsed}ms");
    /// }
    /// 
    /// // 嵌套计时使用方式
    /// using (var timer = new EnhancedMillisecondTimer())
    /// {
    ///     timer.Start(); // 外层Start
    ///     System.Threading.Thread.Sleep(50);
    ///     
    ///     timer.Start(); // 内层Start
    ///     System.Threading.Thread.Sleep(100);
    ///     long innerElapsed = timer.Stop(); // 返回内层耗时（约100ms）
    ///     
    ///     System.Threading.Thread.Sleep(50);
    ///     long outerElapsed = timer.Stop(); // 返回外层耗时（约200ms）
    ///     
    ///     Console.WriteLine($"内层操作耗时: {innerElapsed}ms");
    ///     Console.WriteLine($"外层操作耗时: {outerElapsed}ms");
    /// }
    /// 
    /// // 使用MeasureSegment方法
    /// using (var timer = new EnhancedMillisecondTimer())
    /// {
    ///     long elapsed = timer.MeasureSegment(() =>
    ///     {
    ///         // 执行需要计时的操作
    ///         System.Threading.Thread.Sleep(150);
    ///     });
    ///     Console.WriteLine($"操作耗时: {elapsed}ms");
    /// }
    /// </code>
    /// 
    /// 适用场景：
    /// - 性能分析：测量代码块的执行时间
    /// - 嵌套操作计时：如多层方法调用、递归操作的计时
    /// - 多次重复操作的累计计时
    /// - 实时监控长时间运行操作的执行时间
    /// </summary>
    public class EnhancedMillisecondTimer : IDisposable
    {
        // 分段计时相关成员
        private readonly Stopwatch _segmentStopwatch;
        private long _lastFullSegmentMs;  // 记录完整分段（最外层）的时间
        private long _totalFullSegmentsMs; // 累计所有完整分段的时间
        private int _segmentNestingDepth;  // 嵌套深度计数器
        private readonly Stack<long> _nestedStartTimes;  // 存储各层嵌套的开始时间

        // 计时器标识
        private readonly string _timerId;
        private bool _disposed = false;

        /// <summary>
        /// 计时器唯一标识
        /// </summary>
        public string TimerId => _timerId;

        #region 分段计时属性
        /// <summary>
        /// 获取分段计时是否正在运行（是否有未关闭的嵌套层级）
        /// </summary>
        public bool IsSegmentRunning => _segmentNestingDepth > 0;

        /// <summary>
        /// 获取上一次完成的完整分段（最外层）计时的时间(毫秒)
        /// </summary>
        public long LastFullSegmentMs => _lastFullSegmentMs;

        /// <summary>
        /// 获取所有完成的完整分段计时的累计时间(毫秒)
        /// </summary>
        public long TotalFullSegmentsMs => _totalFullSegmentsMs;

        /// <summary>
        /// 获取当前分段计时的嵌套深度
        /// </summary>
        public int SegmentNestingDepth => _segmentNestingDepth;

        /// <summary>
        /// 获取当前嵌套分段从最外层Start到现在的累计时间(毫秒)
        /// </summary>
        public long CurrentSegmentElapsedMs => GetCurrentSegmentElapsedTime();

        /// <summary>
        /// 获取当前嵌套分段从最内层Start到现在的累计时间(毫秒)
        /// </summary>
        public long CurrentInnerSegmentElapsedMs => GetCurrentInnerSegmentElapsedTime();
        #endregion

        #region 计时器构造方法
        /// <summary>
        /// 初始化EnhancedMillisecondTimer实例
        /// </summary>
        public EnhancedMillisecondTimer() : this(Guid.NewGuid().ToString()) { }

        /// <summary>
        /// 初始化EnhancedMillisecondTimer实例，指定计时器ID
        /// </summary>
        /// <param name="timerId">计时器唯一标识</param>
        public EnhancedMillisecondTimer(string timerId)
        {
            _timerId = timerId;

            // 初始化分段计时器
            _segmentStopwatch = new Stopwatch();
            _lastFullSegmentMs = 0;
            _totalFullSegmentsMs = 0;
            _segmentNestingDepth = 0;
            _nestedStartTimes = new Stack<long>();
        }
        #endregion

        #region 分段计时方法（支持嵌套）
        /// <summary>
        /// 开始或增加分段计时的嵌套层级
        /// 多次调用会增加嵌套深度
        /// </summary>
        public void Start()
        {
            // 如果是第一次开始（嵌套深度为0），则启动计时器
            if (_segmentNestingDepth == 0)
            {
                _segmentStopwatch.Restart();
            }

            // 记录当前层级的开始时间（相对于最外层开始的毫秒数）
            _nestedStartTimes.Push(_segmentStopwatch.ElapsedMilliseconds);

            // 增加嵌套深度
            _segmentNestingDepth++;
        }

        /// <summary>
        /// 减少分段计时的嵌套层级或停止计时
        /// 返回当前层级从Start到Stop的时间(毫秒)
        /// </summary>
        /// <returns>当前层级的计时结果(毫秒)</returns>
        public long Stop()
        {
            // 如果没有正在运行的计时，直接返回0
            if (_segmentNestingDepth == 0 || _nestedStartTimes.Count == 0)
                return 0;

            // 获取当前层级的开始时间
            long layerStartTime = _nestedStartTimes.Pop();
            // 计算当前层级的结束时间（相对于最外层）
            long currentTotalTime = _segmentStopwatch.ElapsedMilliseconds;
            // 当前层级的耗时
            long layerElapsed = currentTotalTime - layerStartTime;

            // 减少嵌套深度
            _segmentNestingDepth--;

            // 如果是最外层计时结束，更新完整分段记录
            if (_segmentNestingDepth == 0)
            {
                _segmentStopwatch.Stop();
                _lastFullSegmentMs = currentTotalTime;
                _totalFullSegmentsMs += currentTotalTime;
            }

            // 返回当前层级的计时结果
            return layerElapsed;
        }

        /// <summary>
        /// 获取当前嵌套分段从最外层Start到现在的累计时间（不停止计时）
        /// </summary>
        /// <returns>已流逝的毫秒数，如果未在计时则返回0</returns>
        public long GetCurrentSegmentElapsedTime()
        {
            if (IsSegmentRunning)
            {
                return _segmentStopwatch.ElapsedMilliseconds;
            }
            return 0;
        }

        /// <summary>
        /// 获取当前嵌套分段从最内层Start到现在的累计时间（不停止计时）
        /// </summary>
        /// <returns>已流逝的毫秒数，如果未在计时则返回0</returns>
        public long GetCurrentInnerSegmentElapsedTime()
        {
            if (IsSegmentRunning && _nestedStartTimes.Count > 0)
            {
                long innerStartTime = _nestedStartTimes.Peek();
                long currentTotalTime = _segmentStopwatch.ElapsedMilliseconds;
                return currentTotalTime - innerStartTime;
            }
            return 0;
        }

        /// <summary>
        /// 重置分段计时数据，清除所有嵌套状态
        /// </summary>
        public void ResetSegments()
        {
            _segmentStopwatch.Reset();
            _segmentNestingDepth = 0;
            _lastFullSegmentMs = 0;
            _totalFullSegmentsMs = 0;
            _nestedStartTimes.Clear();
        }
        #endregion

        /// <summary>
        /// 重置所有计时数据
        /// </summary>
        public void ResetAll()
        {
            ResetSegments();
        }

        /// <summary>
        /// 测量一个操作的执行时间(毫秒)，使用分段计时
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns>操作执行的时间间隔(毫秒)</returns>
        public long MeasureSegment(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            long result = 0;
            Start();
            try
            {
                action.Invoke();
            }
            finally
            {
                result = Stop();
            }
            return result;
        }

        /// <summary>
        /// 获取当前计时器所有状态的字符串表示
        /// </summary>
        /// <returns>包含所有计时信息的字符串</returns>
        public override string ToString()
        {
            return $"计时器 {_timerId}:\n" +
                   $"  分段计时: 运行中={IsSegmentRunning}, " +
                   $"嵌套深度={_segmentNestingDepth}, " +
                   $"从最外层开始={GetCurrentSegmentElapsedTime()}ms, " +
                   $"从最内层开始={GetCurrentInnerSegmentElapsedTime()}ms, " +
                   $"上次完整分段={_lastFullSegmentMs}ms, " +
                   $"累计完整分段={_totalFullSegmentsMs}ms";
        }

        #region 释放资源

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源的实际实现
        /// </summary>
        /// <param name="disposing">是否手动释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 停止所有正在运行的计时，清除嵌套状态
                _segmentNestingDepth = 0;
                _segmentStopwatch.Stop();
                _nestedStartTimes.Clear();
            }

            _disposed = true;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~EnhancedMillisecondTimer()
        {
            Dispose(false);
        }
        #endregion
    }
}
