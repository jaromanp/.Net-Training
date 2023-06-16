using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private class LinkedListEnumerator : IEnumerator<T>
        {
            private Node<T> current;
            private Node<T> head;

            public LinkedListEnumerator(Node<T> head)
            {
                this.head = head;
                current = null;
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                // No resources to dispose
            }

            public bool MoveNext()
            {
                if (current == null)
                    current = head;
                else
                    current = current.Next;

                return current != null;
            }

            public void Reset()
            {
                current = null;
            }
        }

        private Node<T> head;
        private Node<T> tail;
        private int length;
        public int Length => length;

        public void Add(T e)
        {
            Node<T> newNode = new Node<T>(e);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = head;
                tail = newNode;
            }
            length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > length)
            {
                throw new IndexOutOfRangeException();
            }

            Node<T> newNode = new Node<T>(e);
            if(index == 0)
            {
                if(head == null)
                {
                    head = newNode;
                    tail = newNode;
                } else
                {
                    head.Prev = newNode;
                    newNode.Next = head;
                    head = newNode;
                }
            } else if (index == length)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            } else
            {
                Node<T> current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                current.Next.Prev = newNode;
                current.Next = newNode;
                newNode.Prev = current;
            }
            length++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= length)
            {
                throw new IndexOutOfRangeException();
            }
            Node<T> current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(head);
        }

        public void Remove(T item)
        {
            Node<T> current = head;
            while (current != null && !current.Data.Equals(item))
                current = current.Next;

            if (current != null)
            {
                if (current == head)
                    head = head.Next;

                if (current == tail)
                    tail = tail.Prev;

                if (current.Prev != null)
                    current.Prev.Next = current.Next;

                if (current.Next != null)
                    current.Next.Prev = current.Prev;

                length--;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= length)
                throw new IndexOutOfRangeException();

            T data;

            if (index == 0)
            {
                data = head.Data;

                if (head == tail)
                    head = tail = null;
                else
                    head = head.Next;

                length--;
            }
            else if (index == length - 1)
            {
                data = tail.Data;
                tail = tail.Prev;
                tail.Next = null;
                length--;
            }
            else
            {
                Node<T> current = head;
                for (int i = 0; i < index; i++)
                    current = current.Next;

                data = current.Data;
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
                length--;
            }

            return data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
