using Microsoft.VisualStudio.TestTools.UnitTesting;
using DequeRefactor;
using System.Threading;

namespace DequeTests
{
    [TestClass]
    public class AtomicityTests
    {
        // validate that mutex is released after each operation.
        // target action performed twice to cover empty and nonempty code paths

        [TestMethod]
        public void ReleasedAfterPush()
        {
            Deque dq = new Deque();
            Assert.IsTrue(dq.Mutex_available());
            dq.Push("abc");
            Assert.IsTrue(dq.Mutex_available());
            dq.Push("def");
            Assert.IsTrue(dq.Mutex_available());
        }

        [TestMethod]
        public void ReleasedAfterPushFront()
        {
            Deque dq = new Deque();
            Assert.IsTrue(dq.Mutex_available());
            dq.Push_front("abc");
            Assert.IsTrue(dq.Mutex_available());
            dq.Push_front("def");
            Assert.IsTrue(dq.Mutex_available());
        }

        [TestMethod]
        public void ReleasedAfterPop()
        {
            Deque dq = new Deque();
            dq.Push("abc");

            dq.Pop();
            Assert.IsTrue(dq.Mutex_available());
            dq.Pop();
            Assert.IsTrue(dq.Mutex_available());
        }

        [TestMethod]
        public void ReleasedAfterPopBack()
        {
            Deque dq = new Deque();
            dq.Push("abc");

            dq.Pop_back();
            Assert.IsTrue(dq.Mutex_available());
            dq.Pop_back();
            Assert.IsTrue(dq.Mutex_available());
        }

        // test concurrent access

        private static Deque ConcurrentDeque = new Deque();

        private void ThreadRoutine()
        {
            for (int i = 0; i < 10; i++)
                ConcurrentDeque.Push(i);

            for (int i = 0; i < 10; i++)
                ConcurrentDeque.Pop();
        }

        [TestMethod]
        public void ConcurrentAccess()
        {
            // can two threads do a simple routine on the deque simultaneously without an error?
            Thread t1 = new Thread(new ThreadStart(ThreadRoutine));
            Thread t2 = new Thread(new ThreadStart(ThreadRoutine));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }
    }

    [TestClass]
    public class EmptyTests
    {
        [TestMethod]
        public void TestPushFrontOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act
            dq.Push_front("abcd");

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNotNull(dq.head);
            Assert.AreEqual(dq.head.data, "abcd");
        }

        [TestMethod]
        public void TestPushOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act
            dq.Push("abcd");

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNotNull(dq.head);
            Assert.AreEqual(dq.head.data, "abcd");
        }

        [TestMethod]
        public void TestPopToEmpty()
        {
            //arrange
            Deque dq = new Deque();
            dq.Push("abcd");

            //act
            dq.Pop();

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNull(dq.head);
        }

        [TestMethod]
        public void TestPopBackToEmpty()
        {
            //arrange
            Deque dq = new Deque();
            dq.Push("abcd");

            //act
            dq.Pop_back();

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNull(dq.head);
        }

        [TestMethod]
        public void TestPopOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act & assert
            Assert.AreEqual(null, dq.Pop());
        }

        [TestMethod]
        public void TestPopBackOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act & assert
            Assert.AreEqual(null, dq.Pop_back());
        }
    }

    [TestClass]
    public class OrderingTests
    {
        [TestMethod]
        public void TestQueueOrder()
        {
            // queue-like (FIFO) ordering is achieved using push and pop
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.Push(i);

            for (int i = 0; i < 10; i++)
            {
                int j = (int)dq.Pop();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestStackOrder()
        {
            // stack-like (LIFO) ordering is achieved using push_front and pop
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.Push_front(i);

            for (int i = 9; i >= 0; i--)
            {
                int j = (int)dq.Pop();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestOtherQueueOrder()
        {
            // queue-like ordering (FIFO) can also be achieved using push_front and pop_back
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.Push_front(i);

            for (int i = 0; i < 10; i++)
            {
                int j = (int)dq.Pop_back();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestOtherStackOrder()
        {
            // stack-like ordering (LIFO) can also be achieved using push and pop_back
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.Push(i);

            for (int i = 9; i >= 0; i--)
            {
                int j = (int)dq.Pop_back();
                Assert.AreEqual(i, j);
            }
        }
    }
}
