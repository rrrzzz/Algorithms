using Algorithms.Corman.DataStructures;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class SinglyLinkedListInsertionTest
    {
        private SinglyLinkedList<int> _testList;

        [Test]
        public void Insertion()
        {
            _testList = new SinglyLinkedList<int>();
            _testList.Insert(new ListItem<int>(-1));
            Assert.IsTrue(_testList.Head.Value == -1);
            Assert.IsTrue(_testList.Head.Next == null);
        }

        [Test]
        public void MassInsertion()
        {
            _testList = new SinglyLinkedList<int>();

            for (int i = -1; i < 6; i++)
            {
                _testList.Insert(new ListItem<int>(i));
            }

            var current = _testList.Head;

            for (int i = 5; i >= -1; i--)
            {
                Assert.IsTrue(current.Value == i);
                current = current.Next;
            }
        }
    }
}
