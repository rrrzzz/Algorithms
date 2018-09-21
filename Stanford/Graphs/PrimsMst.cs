using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class PrimsMst
    {
        // input - connected graph. Task - find collection of cheapest edges to connect all nodes

        public List<Tuple<int, int, int>> GetMinimumSpanningTree(Dictionary<int,NodeWeighted> graph)
        {
            var nodesCount = graph.Count;
            var cheapestEdgesHeap = new MinHeapUniversal<NodeWeighted>();
            var msTree = new List<Tuple<int, int, int>>();

            var cheapestNode = graph.Values.First();
            cheapestNode.Visit();
            nodesCount--;

            while (nodesCount != 0)
            {
                var unvisitedNeighbours = cheapestNode.Neighbours.Where(x => !x.Item1.IsVisited);

                foreach (var nodeTuple in unvisitedNeighbours)
                {
                    var currentNode = nodeTuple.Item1;
                    var edgeWeight = nodeTuple.Item2;
                    if (currentNode.HeapIndex == 0)
                    {
                        currentNode.Parent = cheapestNode;
                        currentNode.Value = edgeWeight;
                        cheapestEdgesHeap.InsertElement(currentNode);
                        continue;
                    }

                    if (cheapestEdgesHeap.DecreaseKey(currentNode.HeapIndex, edgeWeight))
                    {
                        currentNode.Parent = cheapestNode;
                    }
                }

                cheapestNode = cheapestEdgesHeap.ExtractMinElement();
                cheapestNode.Visit();

                var edge = new Tuple<int, int, int>(cheapestNode.Parent.Id, cheapestNode.Id, cheapestNode.Value);
                msTree.Add(edge);

                nodesCount--;
            }

            return msTree;
        }
    }
}
