using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class DijkstraShortestPath
    {
        public int[] GetShortestPaths(Dictionary<int,NodeWeighted> graph, int startingVertex)
        {
            var visitedNodesCount = 0;
            var closestUnvisitedNodes = new MinHeapUniversal<NodeWeighted>();
            
            var pathLengths = new int[graph.Count + 1];
            var firstNode = graph[startingVertex];

            firstNode.Value = 0;
            pathLengths[startingVertex] = 0;
            visitedNodesCount++;
            firstNode.Visit();

            var lastAddedNode = firstNode;

            while (visitedNodesCount != graph.Count)
            {
                var unvisitedHeadNodes = lastAddedNode.Neighbours.FindAll(x => !x.Item1.IsVisited);

                foreach (var nodeWeightTuple in unvisitedHeadNodes)
                {
                    var currentNode = nodeWeightTuple.Item1;
                    var pathLength = nodeWeightTuple.Item2 + lastAddedNode.Value;
                    var isNodeInHeap = currentNode.HeapIndex != 0;

                    if (isNodeInHeap)
                    {
                        closestUnvisitedNodes.DecreaseKey(currentNode.HeapIndex, pathLength);
                    }
                    else
                    {
                        currentNode.Value = pathLength;
                        closestUnvisitedNodes.InsertElement(currentNode);
                    }
                }

                lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                lastAddedNode.Visit();
                pathLengths[lastAddedNode.Id] = lastAddedNode.Value;
                visitedNodesCount++;
            }

            return pathLengths;
        }
    }
}