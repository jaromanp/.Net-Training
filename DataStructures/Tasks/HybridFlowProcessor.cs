using System;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public T Dequeue()
        {
            if (tail == null)
                throw new InvalidOperationException();

            T data = tail.Data;

            if (head == tail)
                head = tail = null;
            else
            {
                tail = tail.Prev;
                tail.Next = null;
            }

            return data;
        }

        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                head = newNode;
            }
        }

        public T Pop()
        {
            if (tail == null)
                throw new InvalidOperationException();

            T data = tail.Data;

            if (head == tail)
                head = tail = null;
            else
            {
                tail = tail.Prev;
                tail.Next = null;
            }

            return data;
        }

        public void Push(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }
    }
}
