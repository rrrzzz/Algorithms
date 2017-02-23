using System;
using System.Collections.Generic;

namespace Algorithms.HackerRank
{
    public class KnightOnChessboard
    {
        private Dictionary<Tuple<int, int>, int> Solutions { get; } = new Dictionary<Tuple<int, int>, int>();

        public void Solve(int n)
        {
            var nMinus = n - 1;
            for (int i = 0; i < n; i++)
            {
                var answer = "";

                for (int j = 0; j < n; j++)
                {
                    int moves;

                    if (i > j)
                    {
                        var tuple = new Tuple<int, int>(i, j);
                        answer += Solutions[tuple] + " ";
                    }
                    else if (i == j)
                    {
                        moves = nMinus % j == 0 ? nMinus / j : -1;
                        answer += moves + " ";
                    }
                    else
                    {
                        var graph = new GraphKnight(nMinus);
                        moves = graph.FindShortestPath(i, j);
                        answer += moves + " ";
                        Solutions.Add(new Tuple<int, int>(i, j), moves);
                    }
                }

                Console.WriteLine(answer.Trim());
            }
        }
    }

    public class NodeMove
    {
        public NodeMove(Tuple<int, int> tuple)
        {
            Id = tuple;
        }

        public Tuple<int, int> Id { get; set; }

        public List<NodeMove> Children { get; set; } = new List<NodeMove>();

        public List<NodeMove> Parents { get; set; } = new List<NodeMove>();
    }

    public class GraphKnight
    {

        private int NMinus { get; set; }

        public bool IsCornerVisited { get; set; }

        private Dictionary<Tuple<int, int>, NodeMove> Graph { get; } = new Dictionary<Tuple<int, int>, NodeMove>();

        private NodeMove Endpoint { get; set; }

        private NodeMove Root { get; } = new NodeMove(new Tuple<int, int>(0, 0));

        public GraphKnight(int nMinus)
        {
            NMinus = nMinus;
        }

        public int FindShortestPath(int x, int y)
        {
            TraceAllMoves(x, y);

            if (!IsCornerVisited)
            {
                return -1;
            }

            var nodes = Endpoint.Parents;
            var count = 1;

            return FindRoot(nodes, count);
        }

        private int FindRoot(List<NodeMove> nodes, int count)
        {
            var parents = new List<NodeMove>();
            foreach (var node in nodes)
            {
                if (node == Root)
                {
                    return count;
                }
                parents.AddRange(node.Parents);
            }
            count++;
            return FindRoot(parents, count);
        }

        private void TraceAllMoves(int x, int y)
        {
            var tuplesMoves = GetPossibleMoves(Root, x, y);

        }

        private List<Tuple<int, int>> GetPossibleMoves(NodeMove node, int a, int b)
        {
            var possibleMoves = new List<Tuple<int, int>>();
            var x1 = node.Id.Item1;
            var y1 = node.Id.Item2;

            if (x1 + a <= NMinus)
            {
                if (y1 + b <= NMinus)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 + a, y1 + b));
                    possibleMoves.Add(new Tuple<int, int>(y1 + b, x1 + a));
                }

                if (y1 - b >= 0)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 + a, y1 - b));
                    possibleMoves.Add(new Tuple<int, int>(y1 - b, x1 + a));
                }
            }

            if (x1 - a >= 0)
            {
                if (y1 + b <= NMinus)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 - a, y1 + b));
                    possibleMoves.Add(new Tuple<int, int>(y1 + b, x1 - a));
                }

                if (y1 - b >= 0)
                {
                    possibleMoves.Add(new Tuple<int, int>(x1 - a, y1 - b));
                    possibleMoves.Add(new Tuple<int, int>(y1 - b, x1 - a));
                }
            }

            return possibleMoves;
        }

        private bool AddNode(NodeMove parentNode, NodeMove nodeToAdd, Tuple<int, int> coordinates)
        {
            if (Graph.ContainsKey(coordinates))
            {
                Graph[coordinates].Parents.Add(parentNode);
                return false;
            }

            //var newNode = new NodeMove(x, y);
            //newNode.Parents.Add(parentNode);
            Graph.Add(coordinates, nodeToAdd);

            if (coordinates.Item1 == coordinates.Item2 && coordinates.Item2 == NMinus)
            {
                IsCornerVisited = true;
                Endpoint = nodeToAdd;
                return false;
            }

            return true;
        }
    }
}
