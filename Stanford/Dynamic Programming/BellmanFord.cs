using System;
using System.Collections.Generic;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class BellmanFord
    {
        //edge restriction, 0,..,n (last pass to check for neg cycles)

        public IEnumerable<int?> GetShortestPaths(Dictionary<int, NodeWeighted> graph, int startingVertex)
        {
            var n = graph.Count;
            var rv = new int?[n + 1];
            var rv2 = new int?[n + 1];
            
            rv[startingVertex] = 0;
            
            var currentArray = rv2;
            var previousArray = rv;

            var r = 1;

            while(r++ <= n)
            {
                var isValUpdated = false;
                for (int j = 1; j <= n; j++)
                {
                    var node = graph[j];
                    var parents = node.Parents;
                    int? min = null;
                    foreach (var parent in parents)
                    {
                        var (parentNode, edgeWeight) = parent;
                        var parentPathValue = previousArray[parentNode.Id];
                        if (!parentPathValue.HasValue) continue;
                        if (!min.HasValue) min = int.MaxValue;
                        min = Math.Min(min.Value, parentPathValue.Value + edgeWeight);
                    }

                    var currentPathValue = previousArray[j];
                    if (!min.HasValue) currentArray[j] = currentPathValue;
                    else if (currentPathValue.HasValue && currentPathValue <= min) currentArray[j] = currentPathValue;
                    else
                    {
                        currentArray[j] = min;
                        isValUpdated = true;
                    }
                }
                if (!isValUpdated) return previousArray;

                previousArray = currentArray;
                currentArray = currentArray == rv ? rv2 : rv;
            }

            return null;
        }
    }
}