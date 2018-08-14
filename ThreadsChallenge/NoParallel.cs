using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ThreadsChallenge
{
    class NoParallel : RunMode, IRunMode
    {
        public void RunConcurrentQueue(ConcurrentQueue<int> data)
        {
            while (data.Count > 0)
            {
                data.TryDequeue(out int num);
                _calculus.Calculate(num);
            }
        }

        public void RunList(List<int> data)
        {
            foreach (int num in data)
            {
                _calculus.Calculate(num);
            }
        }
    }
}
