using System.Collections.Generic;

namespace Algorithms.Corman.DataStructures
{
    public class SinglyLinkedList<T>
    {
        public ListItem<T> Head { get; set; }

        public SinglyLinkedList(){}

        public SinglyLinkedList(ListItem<T> item)
        {
            Head = item;
        }

        public ListItem<T> Search(T value)
        {
            var currentItem = Head;
            while (currentItem != null && !EqualityComparer<T>.Default.Equals(currentItem.Value, value))
            {
                currentItem = currentItem.Next;
            }

            return currentItem;
        }

        public virtual void Insert(ListItem<T> item)
        {
            item.Next = Head;
            Head = item;
        }

        public virtual void Delete(ListItem<T> item)
        {
            if (EqualityComparer<T>.Default.Equals(Head.Value, item.Value))
            {
                Head = Head.Next;
                return;
            }

            var previous = Head;
            var current = Head.Next;
            while (current != null && !EqualityComparer<T>.Default.Equals(current.Value, item.Value))
            {
                previous = current;
                current = current.Next;
            }

            if (current == null) return;

            previous.Next = current.Next;
            current.Next = null;
        }

        public bool IsLoopExistsSpoilData()
        {
            if (Head.Next != null && EqualityComparer<T>.Default.Equals(Head.Next.Value, Head.Value))
            {
                return true;
            }

            var current = Head.Next;
            while (current != null && !EqualityComparer<T>.Default.Equals(current.Value, Head.Value))
            {
                var temp = current.Next;
                current.Next = Head;
                current = temp;
            }

            return current == null;
        }

        public bool IsLoopExistsProper()
        {
            var fastSelector = Head.Next;
            var slowSelector = Head;

            while (true) 
            {
                if (fastSelector?.Next == null)
                {
                    return false;
                }

                if (EqualityComparer<T>.Default.Equals(fastSelector.Value, slowSelector.Value) || EqualityComparer<T>.Default.Equals(fastSelector.Next.Value, slowSelector.Value))
                {
                    return true;
                }

                fastSelector = fastSelector.Next.Next;
                slowSelector = slowSelector.Next;
            }
        }
    }
}