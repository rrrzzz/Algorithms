using System;
using Algorithms.Corman.DataStructures;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class QueueTests
    {
        private QueueLinkedList<int> _queue;
        public const string ExceptionMessage = "Queue has underflowed";

        [SetUp]
        public void SetUp()
        {
            _queue = new QueueLinkedList<int>();
            
        }

        [Test]
        public void Enqueue()
        {
            _queue.Enqueue(3);
            _queue.Enqueue(4);
            Assert.IsTrue(_queue.Peek().Value == 3);
        }

        [Test]
        public void Dequeue()
        {
            _queue.Enqueue(3);
            _queue.Enqueue(4);
            Assert.IsTrue(_queue.Dequeue() == 3);
            Assert.IsTrue(_queue.Dequeue() == 4);
            var ex = Assert.Throws<InvalidOperationException>(() => _queue.Dequeue());
            Assert.IsTrue(ex.Message == ExceptionMessage);
        }
    }
}