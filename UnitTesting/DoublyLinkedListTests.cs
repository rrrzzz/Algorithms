using Algorithms.Corman.DataStructures;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class DoublyLinkedListTests
    {
        private DoublyLinkedList<int> _list;

        [Test]
        public void Insertion()
        {
            _list = new DoublyLinkedList<int>();
            _list.Insert(new ListItem<int>(2));
            _list.Insert(new ListItem<int>(-5));

            Assert.IsTrue(_list.Head.Value == -5);
            Assert.IsTrue(_list.Head.Next.Previous.Value == -5);
            Assert.IsTrue(_list.Head.Next.Value == 2);
        }

        [Test]
        public void Deletion()
        {
            _list = new DoublyLinkedList<int>();
            _list.Insert(new ListItem<int>(2));
            _list.Insert(new ListItem<int>(-5));
            _list.Insert(new ListItem<int>(3));

            _list.Delete(new ListItem<int>(-5));
            Assert.IsTrue(_list.Head.Next.Value == 2);
            Assert.IsTrue(_list.Head.Next.Previous.Value == 3);
        }
    }
}
