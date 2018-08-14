using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ThreadsChallenge
{
    class NoParallel :  IRunMode
    {
        private readonly ICalculus _calculus;

        public NoParallel(ICalculus calculus)
        {
            _calculus = calculus;
        }
        
        public void RunConcurrentQueue(ConcurrentQueue<int> data, int threads)
        {
            while (data.Count > 0)
            {
                data.TryDequeue(out int num);
                _calculus.Calculate(num);
            }
        }

        public void RunList(List<int> data, int threads)
        {
            foreach (int num in data)
            {
                _calculus.Calculate(num);
            }
        }
    }
}
