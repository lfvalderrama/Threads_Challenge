using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    class ParallelForEach : IRunMode
    {
        private readonly ICalculus _calculus;
        public ParallelForEach(ICalculus calculus)
        {
            _calculus = calculus;
        }

        public void RunConcurrentQueue(ConcurrentQueue<int> data, int threads)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = threads };
            Parallel.ForEach(data, options, (num) =>
            {
                _calculus.Calculate(num);
            }
            );
        }

        public void RunList(List<int> data, int threads)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = threads };
            Parallel.ForEach(data, options, (num) =>
            {
                _calculus.Calculate(num);
            }
            );
        }
    }
}
