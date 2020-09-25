using System;
using System.Threading;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DequeTests")]

namespace DequeRefactor
{
#nullable enable
    public class DLLNode
    {
        // node of a doubly-linked list
        internal DLLNode? next;
        internal DLLNode? prev;
        internal object data;

        public DLLNode(object d)
        {
            data = d;
            next = null;
            prev = null;
        }

    }
    public class Deque
    {
        // Double-ended Queue (Deque)
        internal DLLNode? head;
        internal DLLNode? tail;
        private static Mutex mut = new Mutex();

        public Deque()
        {
            head = null;
            tail = null;
        }

        public void Init_from_empty(object o)
        {
            mut.WaitOne();
            head = new DLLNode(o);
            tail = head;
            mut.ReleaseMutex();
        }

        public void Push(object o)
        {
            mut.WaitOne();
            if (tail == null)
                Init_from_empty(o);
            else
            {
                DLLNode new_tail = new DLLNode(o);
                tail.next = new_tail;
                new_tail.prev = tail;
                tail = new_tail;
            }
            mut.ReleaseMutex();
        }

        public void Push_front(object o)
        {
            mut.WaitOne();
            if (head == null)
                Init_from_empty(o);
            else
            {
                DLLNode new_head = new DLLNode(o);
                new_head.next = head;
                head.prev = new_head;
                head = new_head;
            }
            mut.ReleaseMutex();
        }

        public object? Pop()
        {
            mut.WaitOne();
            if (head == null)
            {
                mut.ReleaseMutex();
                return null;
            }
            else
            {
                DLLNode popped = head;
                head = head.next;
                if (head != null)
                    head.prev = null;
                else
                    tail = null;
                mut.ReleaseMutex();
                return popped.data;
            }
        }

        public object? Pop_back()
        {
            mut.WaitOne();
            if (tail == null)
            {
                mut.ReleaseMutex();
                return null;
            }
            else
            {
                DLLNode popped = tail;
                tail = popped.prev;
                if (tail != null)
                    tail.next = null;
                else
                    head = null;
                mut.ReleaseMutex();
                return popped.data;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
