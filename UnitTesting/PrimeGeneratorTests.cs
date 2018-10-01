using Algorithms.Corman;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class PrimeGeneratorTests
    {
        [Test]
        public void SieveFirstPrime()
        {
            var firstPrime = 2;
            var sieve = PrimeGenerator.GetPrimeEnumeratorEratosthenes(2);
            sieve.MoveNext();
            Assert.IsTrue(sieve.Current == firstPrime);
        }

        [Test]
        public void SieveFirstPrimes()
        {
            var firstPrimes = new[]{2,3,5,7,11,13,17,19,23};
            var sieve = PrimeGenerator.GetPrimeEnumeratorEratosthenes(24);
            sieve.MoveNext();

            foreach (var prime in firstPrimes)
            {
                Assert.IsTrue(prime == sieve.Current);
                sieve.MoveNext();
            }
        }

        [Test]
        public void NextClosestPrimeLargeNumber()
        {
            var number = 10094;
            var closestPrime = 10099;

            var actualResult = PrimeGenerator.GetNextClosestPrime(number);
            Assert.IsTrue(actualResult == closestPrime);
        }

        [Test]
        public void ClosestPrimeNumberInputIsPrime()
        {
            var number = 10099;

            var actualResult = PrimeGenerator.GetNextClosestPrime(number);
            Assert.IsTrue(actualResult == number);
        }
    }
}