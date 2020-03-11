using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class ClusterizationSolver
    {
        //In this programming problem and the next you'll code up the clustering algorithm from lecture for computing a max-spacing k-clustering.

        //Download the text file below (right click on the file and select "Save As...":
        //clustering1.txt

        //This file describes a distance function (equivalently, a complete graph with edge costs). 

        //Your task in this problem is to run the clustering algorithm from lecture on this data set, 
        //where the target number of clusters is set to 4. What is the maximum spacing of a 4-clustering?
        //answer is 106

        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_fe8d0202cd20a808db6a4d5d06be62f4_clustering1.txt?Expires=1584057600&Signature=IaQHQGmXNfCQct7Meu1rIFtk-ojHlao9FRNFaZcxDeqPQq7atUPm2yhY~ImXnNkFQF9aZd-kE-3FkVzKDG7OYLl7S38WaAoVNuratjG7UofAh5YFZog6WeffuwG~G3EcKjEp1f07bWJo1EiaSYTFCafIgEoKgXO2rKwLiY1EqFk_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

        private static int _nodeCount;

        public static int GetMaxClusterizationSpacing(int k)
        {
            var graph = ParseWeigthedEdgesFromWeb();
            var kruskal = new KruskalsMst();

            var clusterization = kruskal.GetMst(graph, _nodeCount);
            return clusterization[clusterization.Count - k + 1].Weight;
        }

        private static List<Edge> ParseWeigthedEdgesFromWeb()
        {
            var edgesParsedByLine = UtilityMethods.GetParsedStringArrayFromWeb(Link, '\n');
            _nodeCount = int.Parse(edgesParsedByLine[0]);

            var output = new List<Edge>(_nodeCount);

            for (int i = 1; i < edgesParsedByLine.Length; i++)
            {
                var currentLine = edgesParsedByLine[i].Split(' ').Select(int.Parse).ToArray();

                var edgeToAdd = new Edge(currentLine[0] - 1, currentLine[1] - 1, currentLine[2]);

                output.Add(edgeToAdd);
            }

            return output;
        }
    }
}