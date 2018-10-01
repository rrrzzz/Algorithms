using System;
using Algorithms.Corman.DataStructures;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class HashTableTests
    {
        public HashTable<int> TestTable;
        [SetUp]
        public void Init()
        {
            TestTable = new HashTable<int>(2);
        } 

        [Test]
        public void AdditionAndRetrieval()
        {
            TestTable.Add(0, 0);
            Assert.IsTrue(TestTable.GetValue(0) == 0);
        }

        [Test]
        public void SetValue()
        {
            TestTable.Add(0,0);
            TestTable.SetValue(0, 5);
            Assert.IsTrue(TestTable.GetValue(0) == 5);
        }

        [Test]
        public void Deletion()
        {
            TestTable.Add(1, 1);
            TestTable.Add(2, 2);
            TestTable.Add(3, 3);

            TestTable.Delete(3);
            Assert.Throws<ArgumentException>(() => TestTable.GetValue(3));
        }

        [Test]
        public void Resize()
        {
            var elementCountLoadTwo = 22;

            for (int i = 0; i < elementCountLoadTwo; i++)
            {
                TestTable.Add(i,i);
            }

            for (int i = 0; i < elementCountLoadTwo; i++)
            {
                Assert.IsTrue(TestTable.GetValue(i) == i);
            }
        }
    }
}