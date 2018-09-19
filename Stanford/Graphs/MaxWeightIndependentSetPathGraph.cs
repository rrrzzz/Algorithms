using System;
using System.Collections.Generic;

namespace Algorithms.Stanford.Graphs
{
    public class MaxWeightIndependentSetPathGraph
    {
        public HashSet<int> GetMaxWeightIndependentSet(int[] nodeWeights)
        {
            var maxWeightNodeSet = new HashSet<int>();
            var cache = new int[nodeWeights.Length + 1];

            cache[1] = nodeWeights[0];

            for (int i = 2; i < cache.Length; i++)
            {
                var optimumWithCurrentNode = cache[i - 2] + nodeWeights[i - 1];
                var optimumWithoutCurrentNode = cache[i - 1];
                cache[i] = Math.Max(optimumWithCurrentNode, optimumWithoutCurrentNode);
            }

            var index = cache.Length - 1;
            while (index > 1)
            {
                if (cache[index] == cache[index - 2] + nodeWeights[index - 1])
                {
                    maxWeightNodeSet.Add(index);
                    index = index - 2;
                    continue;
                }
                index--;
            }

            if (index == 1) maxWeightNodeSet.Add(index);
            return maxWeightNodeSet;
        }
    }
}