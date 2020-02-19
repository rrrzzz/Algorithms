using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class JohnsonReweighting
    {
        public Dictionary<int, NodeWeighted> ReweightEdges(Dictionary<int, NodeWeighted> graph)
        {
            var lastVertex = graph.Keys.Max();
            var newNode = new NodeWeighted(lastVertex + 1);

            graph[lastVertex + 1] = newNode;
            var parent = new Tuple<NodeWeighted,int>(newNode, 0);
            foreach (var vert in graph.Keys)
            {
                if (vert == lastVertex+1) continue;
                graph[vert].Parents.Add(parent);
            }
            
            var bellmanResult = new BellmanFord().GetShortestPaths(graph, lastVertex + 1);
            if (bellmanResult == null) return null;
            var arr = bellmanResult.ToArray();
            
            for (int i = 1; i < lastVertex + 1; i++)
            {
                if (!arr[i].HasValue) throw new ArgumentException("Some of the shortest paths are null");
                graph[i].JohnsonsValue = arr[i].Value;
            }

            graph.Remove(lastVertex + 1);
            
            foreach (var vert in graph.Keys)
            {
                var vertScore = graph[vert].JohnsonsValue;
                for (var i = 0; i < graph[vert].Neighbours.Count; i++)
                {
                    var (neighbour, weight) = graph[vert].Neighbours[i];
                    var reweighted = weight + vertScore - neighbour.JohnsonsValue;
                    graph[vert].Neighbours[i] = new Tuple<NodeWeighted, int>(neighbour, reweighted);
                }
            }

            return graph;
        }
    }
}