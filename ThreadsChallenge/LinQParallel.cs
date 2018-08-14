using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadsChallenge
{
    class LinQParallel : IRunMode
    {
        private readonly ICalculus _calculus;

        public LinQParallel(ICalculus calculus)
        {
            _calculus = calculus;
        }

        public void RunConcurrentQueue(ConcurrentQueue<int> data, int threads)
        {
            data.AsParallel().WithDegreeOfParallelism(threads).ForAll(
                (num) =>
                {
                    _calculus.Calculate(num);
                }
                );
        }

        public void RunList(List<int> data, int threads)
        {
            data.AsParallel().WithDegreeOfParallelism(threads).ForAll(
               (num) =>
               {
                   _calculus.Calculate(num);
               }
               );
        }
    }
}
