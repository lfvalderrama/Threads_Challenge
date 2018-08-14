using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    public interface IRunMode
    {
        void RunList(List<int> data);
        void RunConcurrentQueue(ConcurrentQueue<int> data);
    }

    abstract class RunMode
    {
        protected ParallelOptions _options;
        protected int _threads;
        protected Calculus _calculus;
    }

}
