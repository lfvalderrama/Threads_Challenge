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
            string typeName = "";
            switch (type)
            {
                case ThreadsTypes.LinQ:
                    typeName = "Parallel LinQ";
                    break;
                case ThreadsTypes.ParallelForeach:
                    typeName = "Parallel Foreach";
                    break;
                case ThreadsTypes.NoParallel:
                    typeName = "No parallel";
                    break;
                case ThreadsTypes.Task:
                    typeName = "Parallel Task";
                    break;
            }
            var runMode = _runModeIndex[type];
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.RunConcurrentQueue(data, threads);
            watchParallel.Stop();
            return $"{typeName} Concurrentqueue: {watchParallel.ElapsedMilliseconds} ms";
        }

        public string GetRunTimeList(List<int> data, int threads, ThreadsTypes type)
        {
            string typeName="";
            switch (type)
            {
                case ThreadsTypes.LinQ:
                    typeName = "Parallel LinQ";
                    break;
                case ThreadsTypes.ParallelForeach:
                    typeName = "Parallel Foreach";
                    break;
                case ThreadsTypes.NoParallel:
                    typeName = "No parallel";
                    break;
                case ThreadsTypes.Task:
                    typeName = "Parallel Task";
                    break;
            }
            var runMode = _runModeIndex[type];
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            runMode.RunList(data, threads);
            watchParallel.Stop();
            return $"{typeName} List: {watchParallel.ElapsedMilliseconds} ms";
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
