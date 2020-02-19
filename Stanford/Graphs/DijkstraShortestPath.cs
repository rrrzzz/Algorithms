using System;
using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class DijkstraShortestPath
    {
        public int[] GetShortestPaths(Dictionary<int,NodeWeighted> graph, int startingVertex)
        {
            var visitedNodesCount = 1;
            var closestUnvisitedNodes = new MinHeapUniversal<NodeWeighted>();
            
            var pathLengths = new int[graph.Count + 1];
            var firstNode = graph[startingVertex];

            firstNode.Value = 0;
            pathLengths[startingVertex] = 0;
            var lastAddedNode = firstNode;
            lastAddedNode.Visit();
            while (visitedNodesCount++ != graph.Count - 1)
            {
                //prolly can rewrite this to add all nodes to heap, then all nodes !in heap are visited; Compare times
                var unvisitedHeadNodes = lastAddedNode.Neighbours.FindAll(x => !x.Item1.IsVisited);
            
                foreach (var nodeWeightTuple in unvisitedHeadNodes)
                {
                    var (neighbour, edgeLength) = nodeWeightTuple;
                    var pathLength = edgeLength + lastAddedNode.Value;
                    var isNodeInHeap = neighbour.HeapIndex != 0;
            
                    if (isNodeInHeap)
                    {
                        closestUnvisitedNodes.TryDecreaseKey(neighbour.HeapIndex, pathLength);
                    }
                    else
                    {
                        neighbour.Value = pathLength;
                        closestUnvisitedNodes.InsertElement(neighbour);
                    }
                }
            
                if (closestUnvisitedNodes.GetHeapSize() != 0)
                {
                    lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                    lastAddedNode.Visit();
                    lastAddedNode.HeapIndex = 0;
                    pathLengths[lastAddedNode.Id] = lastAddedNode.Value;
                }
            }

            return pathLengths;
        }

        public int GetMinAllPairsShortestPaths(Dictionary<int, NodeWeighted> graph)
        {
            var vertCount = graph.Count;
            var min = int.MaxValue;

            var ls = new List<int> {graph.Count, graph.Count, graph.Count};

            foreach (var startingNode in graph)
            {
                
                Dictionary<int, NodeWeighted> copy = new Dictionary<int, NodeWeighted>();

                foreach (var nd in graph)
                {
                    copy[nd.Key] = new NodeWeighted(nd.Key){JohnsonsValue = graph[nd.Key].JohnsonsValue};
                }
                
                foreach (var nd in graph)
                {
                    foreach (var neighbour in graph[nd.Key].Neighbours)
                    {
                        var (nb, edgeLength) = neighbour;
                        copy[nd.Key].Neighbours.Add(new Tuple<NodeWeighted, int>(copy[nb.Id], edgeLength));
                    }
                }
                
                
                var sourceId = startingNode.Key;
                var visitedNodesCount = 0;
                var closestUnvisitedNodes = new MinHeapUniversal<NodeWeighted>();

                var sourceNode = graph[sourceId];

                sourceNode.Value = 0;

                foreach (var node in graph)
                {
                    closestUnvisitedNodes.InsertElement(node.Value);
                }

                while (visitedNodesCount++ != vertCount)
                {
                    var lastAddedNode = closestUnvisitedNodes.ExtractMinElement();
                    lastAddedNode.HeapIndex = 0;

                    if (lastAddedNode.Id != sourceId)
                    {
                        var adjustedPathValue = lastAddedNode.Value + lastAddedNode.JohnsonsValue - lastAddedNode.Parent.JohnsonsValue;
                        min = Math.Min(min, adjustedPathValue);
                    }

                    foreach (var nodeWeightTuple in lastAddedNode.Neighbours)
                    {
                        var (neighbour, edgeLength) = nodeWeightTuple;
                        if (neighbour.HeapIndex == 0) continue;

                        var pathLength = edgeLength + lastAddedNode.Value;
                        var isKeyDecreased = closestUnvisitedNodes.TryDecreaseKey(neighbour.HeapIndex, pathLength);
                        if (isKeyDecreased)
                            neighbour.Parent = lastAddedNode;
                    }

                    lastAddedNode.Value = Globals.DefaultDijkstraValue;
                }
            }

            return min;
        }

    }
}