using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadsChallenge
{
    public interface IRunMode
    {
        void RunList(List<int> data, int threads);
        void RunConcurrentQueue(ConcurrentQueue<int> data, int threads);
    }

}
