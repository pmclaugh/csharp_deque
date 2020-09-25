using Microsoft.VisualStudio.TestTools.UnitTesting;
using DequeRefactor;

namespace DequeTests
{
    [TestClass]
    public class EmptinessTests
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
            Assert.IsTrue(dq.Mutex_available());
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
            Assert.IsTrue(dq.Mutex_available());
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
            Assert.IsTrue(dq.Mutex_available());
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
            Assert.IsTrue(dq.Mutex_available());
        }

        [TestMethod]
        public void TestPopOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act & assert
            Assert.AreEqual(null, dq.Pop());
            Assert.IsTrue(dq.Mutex_available());
        }

        [TestMethod]
        public void TestPopBackOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act & assert
            Assert.AreEqual(null, dq.Pop_back());
            Assert.IsTrue(dq.Mutex_available());
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
