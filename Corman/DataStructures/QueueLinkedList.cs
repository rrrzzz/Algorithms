using System;

namespace Algorithms.Corman.DataStructures
{
    public class QueueLinkedList<T>
    {
        private readonly SinglyLinkedList<T> _queue;
        private ListItem<T> _tail;

        public QueueLinkedList()
        {
            _queue = new SinglyLinkedList<T>();
        }

        public void Enqueue(T item)
        {
            if (_queue.Head == null)
            {
                _queue.Head = _tail = new ListItem<T>(item);
                return;
            }

            _tail.Next = new ListItem<T>(item);
            _tail = _tail.Next;
        }

        public T Dequeue()
        {
            if (_queue.Head == null)
            {
                throw new InvalidOperationException("Queue has underflowed");
            }

            var output = _queue.Head.Value;
            _queue.Head = _queue.Head.Next;
            
            return output;
        }

        public ListItem<T> Peek()
        {
            return _queue.Head;
        }
    }
}