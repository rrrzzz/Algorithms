namespace Algorithms.Corman.DataStructures
{
    public class DoublyLinkedList<T> : SinglyLinkedList<T>
    {
        public override void Insert(ListItem<T> item)
        {
            base.Insert(item);
            Head.Next.Previous = Head;
        }

        public override void Delete(ListItem<T> item)
        {
            var result = Search(item.Value);
            if (result == null) return;

            if (result.Next == null)
            {
                result.Previous.Next = null;
            }
            else
            {
                result.Previous.Next = result.Next;
                result.Next.Previous = result.Previous;
            }
        }
    }
}