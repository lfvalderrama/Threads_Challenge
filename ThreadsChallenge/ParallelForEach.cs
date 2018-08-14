using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class ParallelForEach : RunMode, IRunMode
    {
        public ParallelForEach(int threads)
        {
            this._threads = threads;
            this._options = new ParallelOptions { MaxDegreeOfParallelism = threads };

        }

        public void RunConcurrentQueue(ConcurrentQueue<int> data)
        {
            Parallel.ForEach(data, _options, (num) =>
            {
                _calculus.Calculate(num);
            }
            );
        }

        public void RunList(List<int> data)
        {
            Parallel.ForEach(data, _options, (num) =>
            {
                _calculus.Calculate(num);
            }
            );
        }
    }
}
