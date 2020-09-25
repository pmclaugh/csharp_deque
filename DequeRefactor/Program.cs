using System;

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

        public Deque()
        {
            head = null;
            tail = null;
        }

        public void Init_from_empty(object o)
        {
            head = new DLLNode(o);
            tail = head;
        }

        public void Push(object o)
        {
            if (tail == null)
                Init_from_empty(o);
            else
            {
                DLLNode new_tail = new DLLNode(o);
                tail.next = new_tail;
                new_tail.prev = tail;
                tail = new_tail;
            }
        }

        public void Push_front(object o)
        {
            if (head == null)
                Init_from_empty(o);
            else
            {
                DLLNode new_head = new DLLNode(o);
                new_head.next = head;
                head.prev = new_head;
                head = new_head;
            }
        }

        public object? Pop()
        {
            if (head == null)
                return null;
            else
            {
                DLLNode popped = head;
                head = head.next;
                if (head != null)
                    head.prev = null;
                else
                    tail = null;
                return popped.data;
            }
        }

        public object? Pop_back()
        {
            if (tail == null)
                return null;
            else
            {
                DLLNode popped = tail;
                tail = popped.prev;
                if (tail != null)
                    tail.next = null;
                else
                    head = null;
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
