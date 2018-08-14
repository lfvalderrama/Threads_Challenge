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
        string GetRunTime<T>(T data, int threads, ThreadsTypes type);
    }

    public class ExcecutionTime : IExcecutionTime
    {
        private readonly IIndex<ThreadsTypes, IRunMode> _runModeIndex;

        public ExcecutionTime(IIndex<ThreadsTypes, IRunMode> runMode)
        {
            _runModeIndex = runMode;
        }
        public string GetRunTime<T>(T data, int threads, ThreadsTypes type)
        {
            string typeName = getMessage(type);
            var runMode = _runModeIndex[type];                
            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            if (data.GetType() == typeof(List<int>))
            {
                var _data = data as List<int>;
                runMode.RunList(_data, threads);
            }
            else if (data.GetType() == typeof(ConcurrentQueue<int>))
            {
                var _data = data as ConcurrentQueue<int>;
                runMode.RunConcurrentQueue(_data, threads);
            }
            watchParallel.Stop();
            return $"{typeName} List: {watchParallel.ElapsedMilliseconds} ms";         
        }

        private static string getMessage(ThreadsTypes type)
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

            return typeName;
        }

    }

}
