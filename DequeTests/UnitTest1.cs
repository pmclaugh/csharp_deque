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
            dq.push_front("abcd");

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNotNull(dq.head);
        }

        [TestMethod]
        public void TestPushOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act
            dq.push("abcd");

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNotNull(dq.head);
        }

        [TestMethod]
        public void TestPopToEmpty()
        {
            //arrange
            Deque dq = new Deque();
            dq.push("abcd");

            //act
            dq.pop();

            //assert
            Assert.AreSame(dq.head, dq.tail);
            Assert.IsNull(dq.head);
        }

        [TestMethod]
        public void TestPopBackToEmpty()
        {
            //arrange
            Deque dq = new Deque();
            dq.push("abcd");

            //act
            dq.pop_back();

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
            Assert.AreEqual(null, dq.pop());
        }

        [TestMethod]
        public void TestPopBackOnEmpty()
        {
            //arrange
            Deque dq = new Deque();

            //act & assert
            Assert.AreEqual(null, dq.pop_back());
        }
    }

    [TestClass]
    public class OrderingTests
    {
        [TestMethod]
        public void TestQueueOrder()
        {
            // queue-like ordering is achieved using push and pop
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.push(i);

            for (int i = 0; i < 10; i++)
            {
                int j = (int)dq.pop();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestStackOrder()
        {
            // stack-like ordering is achieved using push_front and pop
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.push_front(i);

            for (int i = 9; i >= 0; i--)
            {
                int j = (int)dq.pop();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestOtherQueueOrder()
        {
            // queue-like ordering can also be achieved using push_front and pop_back
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.push_front(i);

            for (int i = 0; i < 10; i++)
            {
                int j = (int)dq.pop_back();
                Assert.AreEqual(i, j);
            }
        }

        [TestMethod]
        public void TestOtherStackOrder()
        {
            // stack-like ordering can also be achieved using push and pop_back
            Deque dq = new Deque();
            for (int i = 0; i < 10; i++)
                dq.push(i);

            for (int i = 9; i >= 0; i--)
            {
                int j = (int)dq.pop_back();
                Assert.AreEqual(i, j);
            }
        }
    }
}
