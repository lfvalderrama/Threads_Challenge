using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Concurrent;
using ThreadsChallenge;
namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        IExcecutionTime _runParallel;
        Mock<IRunMode> _runMode;

        [TestInitialize]
        public void Initialize()
        {
            _runMode = new Mock<IRunMode>();
            _runParallel = new RunTimeParallelForEach(_runMode.Object);
        }
        [TestMethod]
        public void TestMethod1()
        {
            //Given
            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
            _runMode.Setup(r => r.RunConcurrentQueue(concurrentQueue, 3));

            //When
            _runParallel.GetRunTimeConqurrentQueue(concurrentQueue, 3);
        }
    }
}
