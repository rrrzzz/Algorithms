using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Corman.DynamicProgramming
{
    public static class RodCutting
    {
        public static int GetBestRevenue(int rodLength, int[] rodPrices)
        {
            var cache = Enumerable.Repeat(-1, rodLength - 1).ToArray();

            return GetBestRevenueCached(rodLength, rodPrices, cache);
        }

        public static List<int> GetBestRevenueCutsBottomUp(int rodLength, int[] rodPrices)
        {
            var cache = Enumerable.Repeat(-1, rodLength).ToArray();
            var bestCutsFirstPieceLengths = new int[rodLength];

            cache[0] = rodPrices[0];
            bestCutsFirstPieceLengths[0] = 1;

            for (int i = 1; i < rodLength; i++)
            {
                var currentMax = rodPrices[i];
                bestCutsFirstPieceLengths[i] = i + 1;

                for (int j = 1; j <= i; j++)
                {
                    var tempRevenue = rodPrices[j - 1] + cache[i - j];
                    if (currentMax < tempRevenue)
                    {
                        bestCutsFirstPieceLengths[i] = j;
                        currentMax = tempRevenue;
                    }
                }
                cache[i] = currentMax;
            }

            var result = new List<int>();
            while (rodLength != 0)
            {
                var currentPieceLength = bestCutsFirstPieceLengths[rodLength - 1];
                result.Add(currentPieceLength);
                rodLength -= currentPieceLength;
            }

            return result;
        }

        private static int GetBestRevenueCached(int rodLength, int[] rodPrices, int[] cache)
        {
            if (rodLength == 1) return rodPrices[0];

            if (cache[rodLength - 1] != -1) return cache[rodLength - 1];

            var max = rodPrices[rodLength - 1];

            for (int i = 0; i < rodLength - 1; i++)
            {
                var currentCutBestRevenue = rodPrices[i] + GetBestRevenueCached(rodLength - i - 1, rodPrices, cache);
                if (currentCutBestRevenue > max)
                {
                    max = currentCutBestRevenue;
                }
            }

            cache[rodLength - 1] = max;
            return max;
        }
    }
}