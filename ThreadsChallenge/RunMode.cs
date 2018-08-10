using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class RunMode
    {
        private ParallelOptions options;
        private int threads;
        
        public RunMode(int threads)
        {
          this.threads = threads;
          this.options  = new ParallelOptions { MaxDegreeOfParallelism = threads };
        }

        private void calculate(int num)
        {
            double x;
            if (num % 2 == 0) x = Math.Sqrt(num);
            //Console.WriteLine($"Even found: {num} SQRT: {Math.Sqrt(num)}");
            else
            {
                BigInteger mult = 1;
                for (int i = 1; i <= num; i++) mult *= i;
                //Console.WriteLine($"Odd found: {num} Factorial: {mult}");
            }
        }
        #region ParallelForeach
        public void ParallelForeach(List<int> data) 
        {            
            Parallel.ForEach(data, options, (num) =>
            {
                calculate(num);
            }
            );
        }

        public void ParallelForeach(ConcurrentQueue<int> data)
        {
            Parallel.ForEach(data, options, (num) =>
            {
                calculate(num);
            }
            );
        }
        #endregion

        #region NoParallel
        public void NoParallel(List<int> data)
        {
            foreach (int num in data)
            {
                calculate(num);
            }            
        }
        #endregion

        #region Tasks
        public void Tasks(ConcurrentQueue<int> data)
        {
            Task[] tasks = new Task[threads];
            
            for (int i = 0; i < threads; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (data.Count > 0)
                    {
                        data.TryDequeue(out int num);
                        calculate(num);
                    }
                });
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }

        public void Tasks(List<int> data)
        {
            Task[] tasks = new Task[threads];
            var e = data.GetEnumerator();
            
            for (int i = 0; i < threads; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (e.MoveNext())
                    {
                        var num = e.Current;
                        calculate(num);
                    }
                });
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);
        }
        #endregion

        #region AsParallel
        public void AsParallel(List<int> data)
        {
            data.AsParallel().WithDegreeOfParallelism(threads).ForAll(
                (num) =>
                {
                    calculate(num);
                }
                );
        }
        public void AsParallel(ConcurrentQueue<int> data)
        {
            data.AsParallel().WithDegreeOfParallelism(threads).ForAll(
                (num) =>
                {
                    data.TryDequeue(out num);
                    double x;
                    calculate(num);
                }
                );
        }
        #endregion
    }
}
