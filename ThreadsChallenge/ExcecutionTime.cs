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
       // string Test<T>(T data, int threads, ThreadsTypes type);
    }

    public class ExcecutionTime : IExcecutionTime
    {
        private readonly IIndex<ThreadsTypes, IRunMode> _runModeIndex;

        public ExcecutionTime(IIndex<ThreadsTypes, IRunMode> runMode)
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

        //public string Test<T>(T data, int threads, ThreadsTypes type)
        //{
            
        //    var runMode = _runModeIndex[type];
        //    var watchParallel = System.Diagnostics.Stopwatch.StartNew();
        //    runMode.RunList(data, threads);
        //    watchParallel.Stop();
        //    return $"Parallel Linq List: {watchParallel.ElapsedMilliseconds} ms";
        //}
    }

}
