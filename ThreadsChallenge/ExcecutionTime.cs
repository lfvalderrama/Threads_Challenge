using Autofac.Features.Indexed;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ThreadsChallenge
{
    public interface IExcecutionTime
    {
        string GetRunTimeList(List<int> data, int threads, ThreadsTypes type);
        string GetRunTimeConqurrentQueue(ConcurrentQueue<int> data, int threads, ThreadsTypes type);
    }


    //public class RunTimeParallelForEach : IExcecutionTime
    //{
    //    private readonly IRunMode _runMode;

    //    public RunTimeParallelForEach(IIndex<ThreadsTypes, IRunMode> runMode)
    //    {
    //        _runMode = runMode[ThreadsTypes.ParallelForeach];
    //    }

    //    public string GetRunTimeConqurrentQueue(ConcurrentQueue<int> data, int threads)
    //    {
    //        var watchParallel = System.Diagnostics.Stopwatch.StartNew();
    //        _runMode.RunConcurrentQueue(data, threads);
    //        watchParallel.Stop();
    //        return $"Parallel ForEach Concurrentqueue: {watchParallel.ElapsedMilliseconds} ms";
    //    }

    //    public string GetRunTimeList(List<int> data, int threads)
    //    {
    //        var watchParallel = System.Diagnostics.Stopwatch.StartNew();
    //        _runMode.RunList(data, threads);
    //        watchParallel.Stop();
    //        return $"Parallel ForEach List: {watchParallel.ElapsedMilliseconds} ms";
    //    }
    //}

    public class RunTimeParallelLinQ : IExcecutionTime
    {
        private readonly IIndex<ThreadsTypes, IRunMode> _runModeIndex;

        public RunTimeParallelLinQ(IIndex<ThreadsTypes, IRunMode> runMode)
        {
            _runModeIndex = runMode;
        }

        public string GetRunTimeConqurrentQueue(ConcurrentQueue<int> data, int threads, ThreadsTypes type)
        {
            var runMode = _runModeIndex[type];
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.RunConcurrentQueue(data, threads);
            watchParallel.Stop();
            return $"Parallel Linq Concurrentqueue: {watchParallel.ElapsedMilliseconds} ms";
        }

        public string GetRunTimeList(List<int> data, int threads, ThreadsTypes type)
        {
            var runMode = _runModeIndex[type];
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.RunList(data, threads);
            watchParallel.Stop();
            return $"Parallel Linq List: {watchParallel.ElapsedMilliseconds} ms";
        }
    }

    //public class RunTimeTask : IExcecutionTime
    //{
    //    private readonly IRunMode _runMode;

    //    public RunTimeTask(IIndex<ThreadsTypes, IRunMode> runMode)
    //    {
    //        _runMode = runMode[ThreadsTypes.Task];
    //    }

    //    public string GetRunTimeConqurrentQueue(ConcurrentQueue<int> data, int threads)
    //    {
    //        var watchParallel = System.Diagnostics.Stopwatch.StartNew();
    //        _runMode.RunConcurrentQueue(data, threads);
    //        watchParallel.Stop();
    //        return $"Parallel Linq Concurrentqueue: {watchParallel.ElapsedMilliseconds} ms";
    //    }

    //    public string GetRunTimeList(List<int> data, int threads)
    //    {
    //        var watchParallel = System.Diagnostics.Stopwatch.StartNew();
    //        _runMode.RunList(data, threads);
    //        watchParallel.Stop();
    //        return $"Parallel Linq List: {watchParallel.ElapsedMilliseconds} ms";
    //    }
    //}

}
