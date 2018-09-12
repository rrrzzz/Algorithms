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

        public ListItem<T> FindLastItem()
        {
            var currentItem = Head;

            while (currentItem.Next != null)
            {
                currentItem = currentItem.Next;
            }

            return currentItem;
        }

        public ListItem<T> Search(T value)
        {
            var currentItem = Head;
            while (currentItem != null && !EqualityComparer<T>.Default.Equals(currentItem.Value, value))
            {
                currentItem = Head.Next;
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
        }

        public bool IsLoopExists()
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
    }
}