using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public class KasarajuSccSolver
    {
        /*
            Download the following text file (right click and select "Save As..."): SCC.txt

            The file contains the edges of a directed graph. Vertices are labeled as positive integers from 1 to 875714. Every row indicates an edge, the vertex label in first column is the tail and the vertex label in second column is the head (recall the graph is directed, and the edges are directed from the first column vertex to the second column vertex). So for example, the  row looks liks : "2 47646". This just means that the vertex with label 2 has an outgoing edge to the vertex with label 47646

            Your task is to code up the algorithm from the video lectures for computing strongly connected components (SCCs), and to run this algorithm on the given graph.

            Enter the sizes of the 5 largest SCCs in the given graph using the fields below, in decreasing order of sizes.          

            answer: 434821, 968, 459, 313, 211
         */

        private const string TestInputPath = "Algorithms.Stanford.TestInput.SCC.txt";
        private const int InputMaxNodeNumber = 875714;

        public string Solve()
        {
            var graph = GetDirectedGraphFromSccInput();
            var componentsSizes = new List<int>();

            var searcher = new GraphSearch(InputMaxNodeNumber);
            var connectedComponents = searcher.KasarajuFindSccs(graph);
            
            foreach (var component in connectedComponents)
            {
                componentsSizes.Add(component.Count);
            }

            componentsSizes.Sort();
            componentsSizes.Reverse();

            var topFiveComponents = componentsSizes.Take(5).ToArray();

            return string.Join(", ", topFiveComponents);
        }

        private Dictionary<int, List<int>> GetDirectedGraphFromSccInput()
        {
            var graph = new Dictionary<int, List<int>>();

            var assembly = Assembly.GetExecutingAssembly();
            var currentNode = 0;

            using (Stream stream = assembly.GetManifestResourceStream(TestInputPath))
            using (StreamReader reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var tempLine = line.Split(new[] { ' ' }, 2).Select(int.Parse).ToArray();
                    var firstNode = tempLine[0];

                    if (firstNode != currentNode)
                    {
                        currentNode = firstNode;
                        graph[firstNode - 1] = new List<int>();
                    }

                    graph[firstNode - 1].Add(tempLine[1] - 1);
                    line = reader.ReadLine();
                }
            }

            return graph;
        }
    }
}
