using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.HackerRank
{
    //https://www.hackerrank.com/contests/goldman-sachs-womens-codesprint/challenges/stock-exchange-matching-algorithm

    public class StockExchangeMatchingAlgorithm
    {
        public static List<int> computePrices(List<int> s, List<int> p, List<int> q)
        {
            var result = new List<int>(q.Count);
            var tuples = new Tuple<int,int>[s.Count];

            for (int i = 0; i < s.Count; i++)
            {
                var temp = new Tuple<int,int>(s[i], p[i]);
                tuples[i] = temp;
            }

            tuples = tuples.OrderBy(x => x.Item1).ToArray();

            foreach (var query in q)
            {
                result.Add(BinarySearchClosestStock(tuples, query));
            }

            return result;
        }

        private static int BinarySearchClosestStock(Tuple<int, int>[] tuples, int query)
        {
            var start = 0;
            var end = tuples.Length - 1;
            var middle = (start + end) / 2;
            int bestMatchIndex = middle;

            while (start <= end) 
            {
                if (tuples[middle].Item1 <= query)
                {
                    bestMatchIndex = middle;
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }

                middle = (start + end) / 2;
            }

            return tuples[bestMatchIndex].Item2;
        }
    }
}