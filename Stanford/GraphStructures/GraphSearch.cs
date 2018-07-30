using System;
using System.Collections.Generic;

namespace Algorithms.Stanford.GraphStructures
{
	public class GraphSearch
	{
	    private bool[] _isNodeVisited;
		public Dictionary<int, List<int>> Graph { get; set; }


		public GraphSearch(Dictionary<int, List<int>> graph)
		{
			Graph = graph;
		    _isNodeVisited = new bool [graph.Count];
		}

		public void BreadthFirstSearch(int start)
		{
			var nodeQueue = new Queue<int>();
		    _isNodeVisited[start] = true;

            nodeQueue.Enqueue(start);

			while (nodeQueue.Count != 0)
			{
				var currentNode = nodeQueue.Dequeue();
				foreach (var node in Graph[currentNode])
				{
				    if (_isNodeVisited[node]) continue;

				    _isNodeVisited[node] = true;

                    nodeQueue.Enqueue(node);
				}
			}
		}

		public void DepthFirstSearch(int start, Action<int> processDeadEndNode)
		{
			var nodeStack = new Stack<int>();
			nodeStack.Push(start);

            while (nodeStack.Count != 0)
			{
			    var currentNode = nodeStack.Peek();
			    _isNodeVisited[currentNode] = true;

                var hasNeighbours = false;

			    foreach (var node in Graph[currentNode])
			    {
			        if (_isNodeVisited[node]) continue;

			        hasNeighbours = true;
					nodeStack.Push(node);
			        break;
			    }

			    if (hasNeighbours) continue;

			    processDeadEndNode(currentNode);
			    nodeStack.Pop();
			}
		}

		public void DepthFirstSearchRecursive(int start, Action<int> processDeadEndNode)
		{
			var currentNode = Graph[start];
		    _isNodeVisited[start] = true;

			foreach (var node in currentNode)
			{
				if (!_isNodeVisited[node])
				{
					DepthFirstSearchRecursive(node, processDeadEndNode);
				} 
			}

		    processDeadEndNode(start);
        }

	    public List<int> GetTopologicalOrdering()
	    {
	        var output = new List<int>();

	        for (int i = 0; i < Graph.Count; i++)
	        {
	            if (_isNodeVisited[i]) continue;

	            DepthFirstSearchRecursive(i, x => output.Add(x));
            }

	        output.Reverse();

	        return output;
	    }
	}
}