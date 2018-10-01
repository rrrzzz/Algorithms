using System.Collections.Generic;

namespace Algorithms.Corman
{
    public static class PrimeGenerator
    {
        public static int GetNextClosestPrime(int number)
        {
            var sieve = GetPrimeEnumeratorEratosthenes(number * 2);

            while (sieve.MoveNext() && sieve.Current < number){}
            
            return sieve.Current;
        }

        //inverted false and true to avoid initializing array values to true
        public static IEnumerator<int> GetPrimeEnumeratorEratosthenes(int number)
        {
            var sieve = new bool[number + 1];

            for (int i = 2; i <= number; i++)
            {
                if (sieve[i]) continue;

                yield return i;

                for (int j = i*i; j <= number; j+=i)
                {
                    sieve[j] = true;
                }
            }
        }
    }
}