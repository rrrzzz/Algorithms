using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Stanford.Graphs
{
    public class KruskalsMst
    {
        public List<Edge> GetMst(List<Edge> graphEdges, int nodeCount)
        {
            var sortedEdges = graphEdges.OrderBy(x => x.Weight).ToArray();
            var unionFind = new UnionFind(nodeCount);
            var minmumSpanningTree = new List<Edge>(nodeCount - 1);

            var currentEdgeIndex = 0;

            while (minmumSpanningTree.Count != nodeCount - 1)
            {
                var currentEdge = sortedEdges[currentEdgeIndex++];

                if (unionFind.FindRoot(currentEdge.HeadId) == unionFind.FindRoot(currentEdge.TailId)) continue;

                unionFind.Union(currentEdge.HeadId, currentEdge.TailId);
                minmumSpanningTree.Add(currentEdge);
                Console.WriteLine("Processed node " + minmumSpanningTree.Count);
            }

            return minmumSpanningTree;
        }
    }
}