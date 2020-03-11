using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class ClusterizationBinaryNodesSolver
    {
        //In this question your task is again to run the clustering algorithm from lecture, but on a MUCH bigger graph. So big, in fact, that the distances (i.e., edge costs) are only defined implicitly, rather than being provided as an explicit list.
        //    For example, the third line of the file "0 1 1 0 0 1 1 0 0 1 0 1 1 1 1 1 1 0 1 0 1 1 0 1" denotes the 24 bits associated with node #2.

        //The distance between two nodes u and v in this problem is defined as the Hamming distance--- the number of differing bits --- between the two nodes' labels. For example, the Hamming distance between the 24-bit label of node #2 above and the label "0 1 0 0 0 1 0 0 0 1 0 1 1 1 1 1 1 0 1 0 0 1 0 1" is 3 (since they differ in the 3rd, 7th, and 21st bits).

        //The question is: what is the largest value of k such that there is a k-clustering with spacing at least 3? That is, how many clusters are needed to ensure that no pair of nodes with all but 2 bits in common get split into different clusters?
        // Answer: 6118


        //read only 10,000 vertices and run your algorightm, then read 50,000, then 100,000, then 200,000 and compare the running times. They should grow linearly.

        public static int FindLargestClusterization()
        {
            var timer = new Stopwatch();
            timer.Start();
            var nodesDictionary = ParseBinaryNodeGraphFromWeb();
            var clusterCount = nodesDictionary.Count;
            var nodesUnionFind = new UnionFind(clusterCount);

            foreach (var nodePair in nodesDictionary)
            {
                var nearbyNodes = GenerateNodesSmallHammingDistance(nodePair.Key);
                foreach (var nearbyNode in nearbyNodes)
                {
                    if (!nodesDictionary.ContainsKey(nearbyNode)) continue;

                    var nodeIndex = nodesDictionary[nearbyNode];

                    if (nodesUnionFind.IsComponentsConnected(nodePair.Value, nodeIndex)) continue;
                    nodesUnionFind.Union(nodePair.Value, nodeIndex);
                    clusterCount--;
                }
            }

            Console.WriteLine(timer.Elapsed);
            return clusterCount;
        }

        private static IEnumerable<bool[]> GenerateNodesSmallHammingDistance(bool[] originalNode)
        {
            var temp = (bool[])originalNode.Clone();
            for (int i = 0; i < originalNode.Length; i++)
            {
                temp[i] = !temp[i];
                yield return temp;

                for (int j = i + 1; j < originalNode.Length; j++)
                {
                    temp[j] = !temp[j];
                    yield return temp;
                    temp[j] = !temp[j];
                }
                temp[i] = !temp[i];
            }
        }

        private static Dictionary<bool[], int> ParseBinaryNodeGraphFromWeb()
        {
            var output = new Dictionary<bool[], int>(new MyEqualityComparer());
            var link = "https://d3c33hcgiwev3.cloudfront.net/_fe8d0202cd20a808db6a4d5d06be62f4_clustering_big.txt?Expires=1584057600&Signature=eyuaYbWWTP2COvlPgokt-PH~pxWtix2QXUAZeKsxy3fKq9NurEk5fo6lDkiCtf9intOz3F1MOMiDSsx3bSrBkyJxeoV0oBZXP4xl5kfFAuQyBCAheFlfHaOLvfELhn124VOmwVTOgqqtrhLx90UxUaE3WIJpEqcgI-nLrZ-1dFg_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

            var stringParsed = UtilityMethods.GetParsedStringArrayFromWeb(link, '\n');

            var nodeCount = int.Parse(stringParsed[0].Split(' ')[0]);
            var indexCounter = 0;
            
            for (int i = 1; i <= nodeCount; i++)
            {
                var currentLineBitsArray = stringParsed[i].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => x == "1").ToArray();
                
                if (!output.ContainsKey(currentLineBitsArray)) output[currentLineBitsArray] = indexCounter++;
            }
            
            return output;
        }
    }

    public class MyEqualityComparer : IEqualityComparer<bool[]>
    {
        public bool Equals(bool[] x, bool[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(bool[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    var boolInt = obj[i] ? 1 : 0; 
                    result = result * 23 + boolInt;
                }
            }
            return result;
        }
    }
}