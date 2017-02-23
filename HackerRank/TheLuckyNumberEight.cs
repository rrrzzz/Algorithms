using System;
using System.Linq;

namespace Algorithms.HackerRank
{
    public static class TheLuckyNumberEight
    {
        public static void FindNumberOfEights()
        {
            const int n = 1;
            const string number = "0";
            const int divisor = 8;
            const int mod = 1000000007;

            long answer = number.Count(x => (int)char.GetNumericValue(x) % divisor == 0);
            for (var z = 0; z < n - 1; z++)
            {
                var currentNum = number[z];
                if ((int)char.GetNumericValue(currentNum) % divisor == 0)
                {
                    answer++;
                }
                var limit = n - z;
                for (var i = 1; i < limit; i++)
                {
                    var startingIndex = z + 1;
                    while (startingIndex - 1 + i < n)
                    {
                        var temp = string.Concat(currentNum, number.Substring(startingIndex, i));
                        var tempNum = long.Parse(temp);
                        if (tempNum % divisor == 0)
                        {
                            answer++;
                        }
                        startingIndex++;
                    }
                }
            }

            if ((int)char.GetNumericValue(number[n - 1]) % divisor == 0)
            {
                answer++;
            }

            Console.WriteLine(answer % mod);
        }
    }
}
