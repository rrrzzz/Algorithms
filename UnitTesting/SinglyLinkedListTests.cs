using Algorithms.Corman.DataStructures;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class SinglyLinkedListTests
    {
        private SinglyLinkedList<int> _testList;
        
        [SetUp]
        public void SetUp()
        {
            _testList = new SinglyLinkedList<int>();
            for (int i = -1; i < 3; i++)
            {
                _testList.Insert(new ListItem<int>(i));
            }
        }

        [Test]
        public void SearchValueExists()
        {
            var searchResult = _testList.Search(2);
            Assert.IsTrue(searchResult.Value == 2);
        }

        [Test]
        public void SearchValueNotInList()
        {
            var searchResult = _testList.Search(4);
            Assert.IsTrue(searchResult == null);
        }

        [Test]
        public void Deletion()
        {
            _testList.Delete(new ListItem<int>(1));
            Assert.IsTrue(_testList.Search(1) == null);
        }

        [Test]
        public void LoopExistsDetection()
        {
            var someElement = _testList.Head.Next.Next.Next;
            someElement.Next = _testList.Head.Next;
            Assert.IsTrue(_testList.IsLoopExistsProper());
        }

        [Test]
        public void NoLoopDetection()
        {
            Assert.IsFalse(_testList.IsLoopExistsProper());
        }
    }
}
