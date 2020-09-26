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
            Assert.IsTrue(dq.MutexAvailable());
            dq.Push("abc");
            Assert.IsTrue(dq.MutexAvailable());
            dq.Push("def");
            Assert.IsTrue(dq.MutexAvailable());
        }

        [TestMethod]
        public void ReleasedAfterPushFront()
        {
            Deque dq = new Deque();
            Assert.IsTrue(dq.MutexAvailable());
            dq.PushFront("abc");
            Assert.IsTrue(dq.MutexAvailable());
            dq.PushFront("def");
            Assert.IsTrue(dq.MutexAvailable());
        }

        [TestMethod]
        public void ReleasedAfterPop()
        {
            Deque dq = new Deque();
            dq.Push("abc");

            dq.Pop();
            Assert.IsTrue(dq.MutexAvailable());
            dq.Pop();
            Assert.IsTrue(dq.MutexAvailable());
        }

        [TestMethod]
        public void ReleasedAfterPopBack()
        {
            Deque dq = new Deque();
            dq.Push("abc");

            dq.PopBack();
            Assert.IsTrue(dq.MutexAvailable());
            dq.PopBack();
            Assert.IsTrue(dq.MutexAvailable());
        }

        // test concurrent access

        private Deque ConcurrentDeque = new Deque();

        private void ThreadRoutine()
        {
            for (int i = 0; i < 10; i++)
                ConcurrentDeque.Push(i);

            for (int i = 0; i < 10; i++)
                Assert.IsNotNull(ConcurrentDeque.Pop());
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

            Assert.AreEqual(ConcurrentDeque.head, ConcurrentDeque.tail);
            Assert.IsNull(ConcurrentDeque.head);
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
            dq.PushFront("abcd");

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
            dq.PopBack();

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
            Assert.AreEqual(null, dq.PopBack());
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
                dq.PushFront(i);

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
                dq.PushFront(i);

            for (int i = 0; i < 10; i++)
            {
                int j = (int)dq.PopBack();
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
                int j = (int)dq.PopBack();
                Assert.AreEqual(i, j);
            }
        }
    }
}
