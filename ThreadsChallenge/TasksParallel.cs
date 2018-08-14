using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class TaskParallel : RunMode, IRunMode
    {
        public TaskParallel(int threads)
        {
            this._threads = threads;
        }

        public void RunConcurrentQueue(ConcurrentQueue<int> data)
        {
            Task[] tasks = new Task[_threads];

            for (int i = 0; i < _threads; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (data.Count > 0)
                    {
                        data.TryDequeue(out int num);
                        _calculus.Calculate(num);
                    }
                });
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }

        public void RunList(List<int> data)
        {
            Task[] tasks = new Task[_threads];
            var e = data.GetEnumerator();

            for (int i = 0; i < _threads; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (e.MoveNext())
                    {
                        var num = e.Current;
                        _calculus.Calculate(num);
                    }
                });
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }
    }
}
