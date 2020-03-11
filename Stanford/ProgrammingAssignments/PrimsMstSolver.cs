using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class PrimsMstSolver
    {
        // In this programming problem you'll code up Prim's minimum spanning tree algorithm.
        // You should NOT assume that edge costs are positive, nor should you assume that they are distinct.
        // 
        // Your task is to run Prim's minimum spanning tree algorithm on this graph.
        // 
        // You should report the overall cost of a minimum spanning tree --- an integer, which may or may not be negative --- in the box below.
        //-3612829
        // heap implementation took 00:00:00.0052567 

        public static int FindMstCost()
        {
            var prim = new PrimsMst();
            var graph = ParseMstGraphFromWeb();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var mst = prim.GetMinimumSpanningTree(graph);
            Console.WriteLine(stopwatch.Elapsed);
            return mst.Sum(x => x.Item3);
        }

        private static Dictionary<int, NodeWeighted> ParseMstGraphFromWeb()
        {
            const string link =
                @"https://d3c33hcgiwev3.cloudfront.net/_d4f3531eac1d289525141e95a2fea52f_edges.txt?Expires=1584057600&Signature=SAKD3aP6yNieA8PkxdIOxSYQRpT~qqaANpqLtmZCP6TK-w~WaRk8PGhIXYGBFlFatZc6mjSS15R0nBWqbMcx0GK6ES7lnBFOlZburDIJak0XC~N7sbKd2pq-ILdYvxi8qNStFpYT9Lsqf8QVFs8nLS4IaaV8dw4-h7raExaFj3c_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

            var graphStringsArray = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n');
            var nodeCount = int.Parse(graphStringsArray[0].Split(' ')[0]);
            var edgeCount = int.Parse(graphStringsArray[0].Split(' ')[1]);

            var graph = new Dictionary<int, NodeWeighted>(nodeCount);

            for (int i = 1; i <= edgeCount; i++)
            {
                var currentEdge = graphStringsArray[i].Split(' ').Select(int.Parse).ToArray();
                var firstNode = currentEdge[0];
                var secondNode = currentEdge[1];
                var weight = currentEdge[2];

                if (!graph.ContainsKey(firstNode))
                {
                    graph[firstNode] = new NodeWeighted(firstNode);
                }

                if (!graph.ContainsKey(secondNode))
                {
                    graph[secondNode] = new NodeWeighted(secondNode);
                }

                var first = graph[firstNode];
                var second = graph[secondNode];

                first.Neighbours.Add(new Tuple<NodeWeighted, int>(second, weight));
                second.Neighbours.Add(new Tuple<NodeWeighted, int>(first, weight));
            }

            return graph;
        }
    }
}
