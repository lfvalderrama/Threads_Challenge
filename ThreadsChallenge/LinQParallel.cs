using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadsChallenge
{
    class LinQParallel : RunMode, IRunMode
    {
        public LinQParallel(int threads)
        {
            this._threads = threads;
        }

        public void RunConcurrentQueue(ConcurrentQueue<int> data)
        {
            data.AsParallel().WithDegreeOfParallelism(_threads).ForAll(
                (num) =>
                {
                    _calculus.Calculate(num);
                }
                );
        }

        public void RunList(List<int> data)
        {
            data.AsParallel().WithDegreeOfParallelism(_threads).ForAll(
               (num) =>
               {
                   _calculus.Calculate(num);
               }
               );
        }
    }
}
