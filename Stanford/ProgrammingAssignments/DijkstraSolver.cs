using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public class DijkstraSolver
    {
        //In this programming problem you'll code up Dijkstra's shortest-path algorithm.

        //    Download the following text file (Right click and select "Save As..."): dijkstraData.txt

        //    The file contains an adjacency list representation of an undirected weighted graph with 200 vertices labeled 1 to 200. Each row consists of the node tuples that are adjacent to that particular vertex along with the length of that edge. For example, the 6th row has 6 as the first entry indicating that this row corresponds to the vertex labeled 6. The next entry of this row "141,8200" indicates that there is an edge between vertex 6 and vertex 141 that has length 8200. The rest of the pairs of this row indicate the other vertices adjacent to vertex 6 and the lengths of the corresponding edges.

        //    Your task is to run Dijkstra's shortest-path algorithm on this graph, using 1 (the first vertex) as the source vertex, and to compute the shortest-path distances between 1 and every other vertex of the graph. If there is no path between a vertex and vertex 1, we'll define the shortest-path distance between 1 and to be 1000000.

        //You should report the shortest-path distances to the following ten vertices, in order: 7,37,59,82,99,115,133,165,188,197. Enter the shortest-path distances using the fields below for each of the vertices.
        //The answer is
        //2599, 2610, 2947, 2052, 2367, 2399, 2029, 2442, 2505, 3068

        private readonly int[] _requiredPaths = {7, 37, 59, 82, 99, 115, 133, 165, 188, 197};

        public string Solve()
        {
            var graph = ParseWightedGraphFromWeb();
            var pathsArray = new DijkstraShortestPath().GetShortestPaths(graph, 1);

            var answerArray = _requiredPaths.Select(x => pathsArray[x]).ToArray();

            return string.Join(", ", answerArray);
        }

        private Dictionary<int, NodeWeighted> ParseWightedGraphFromWeb()
        {
            const string link =
                "https://lagunita.stanford.edu/assets/courseware/v1/c8748131579ef6bd10b2d46f616988e9/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/dijkstraData.txt";
            var nodes = new Dictionary<int, NodeWeighted>();

            var parsed = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n');

            var graphArray = new string[parsed.Length][];

            for (int i = 0; i < parsed.Length; i++)
            {
                parsed[i] = parsed[i].Replace("\r", "");
                graphArray[i] = parsed[i].Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var stringArray in graphArray)
            {
                var tailNode = int.Parse(stringArray[0]);
                if (!nodes.ContainsKey(tailNode))
                {
                    nodes[tailNode] = new NodeWeighted(tailNode);
                }

                for (int i = 1; i < stringArray.Length; i++)
                {
                    var temp = stringArray[i].Split(',').Select(int.Parse).ToArray();
                    var headNode = temp[0];
                    var weight = temp[1];

                    if (!nodes.ContainsKey(headNode))
                    {
                        nodes[headNode] = new NodeWeighted(headNode);
                    }

                    var neighbourToAdd = new Tuple<NodeWeighted, int>(nodes[headNode], weight);
                    nodes[tailNode].Neighbours.Add(neighbourToAdd);
                }
            }

            return nodes;
        }
    }
}
