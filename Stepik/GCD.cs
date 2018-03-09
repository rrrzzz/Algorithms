using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Stepik
{
    class Gcd
    {
        public void GdcEuclid()
        {
            var input = Console.ReadLine().Split(' ');
            var n = int.Parse(input[0]);
            var m = int.Parse(input[1]);

            var answer = GetGcdRecursive(n, m);
            Console.WriteLine(answer);
        }

        private int GetGcdRecursive(int n, int m)
        {
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            if (n >= m)
            {
                return GetGcdRecursive(n % m, m);
            }
            else
            {
                return GetGcdRecursive(m % n, n);
            }
        }
    }
}
