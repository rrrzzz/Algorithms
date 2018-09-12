using System;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class StackTests
    {
        private const string ExceptionMessage = "Stack underflow";

        [Test]
        public void CheckStackunderflow()
        {
            var stack = new Algorithms.Corman.DataStructures.Stack<int>();
            var ex = Assert.Throws<InvalidOperationException>(() => stack.Peek());
            Assert.IsTrue(ex.Message.Contains(ExceptionMessage));
        }

        [Test]
        public void CheckPushPop()
        {
            var stack = new Algorithms.Corman.DataStructures.Stack<int>();
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i);
            }

            Assert.IsTrue(stack.Pop() == 4);
        }
    }
}