using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Algorithms.Stanford;
using Algorithms.Stanford.Graphs;

namespace Algorithms
{
    public class UtilityMethods
    {
        public int number = 25;
        public static void SwapValues<T>(T[] array , int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        public static UnionFind CopyUnionFind(UnionFind input)
        {
            var n = input.Nodes.Length;
            var output = new UnionFind
            {
                Nodes = new int[n],
                Sizes = new int[n]
            };

            input.Nodes.CopyTo(output.Nodes, 0);
            input.Sizes.CopyTo(output.Sizes, 0);

            return output;
        }

        public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    return null;

                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string[] GetParsedStringArrayFromWeb(string link, string separator)
        {
            string content;
            using (var client = new WebClient())
            {
                content = client.DownloadString(link);
            }

            var parsedLines = content.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return parsedLines;
        }

        public static string[] GetParsedStringArrayFromWeb(string link, char separator)
        {
            string content;
            using (var client = new WebClient())
            {
                content = client.DownloadString(link);
            }

            var parsedLines = content.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            return parsedLines;
        }

        public static Dictionary<int, NodeWeighted> GetTailHeadWeightSpaceGraph(IEnumerable<string> parsed)
        {
            var nodes = new Dictionary<int, NodeWeighted>();
            foreach (var line in parsed)
            {
                var curArray = line.Split(' ');
                var tailNode = int.Parse(curArray[0]);
                var headNode = int.Parse(curArray[1]);
                var weight = int.Parse(curArray[2]);
                if (!nodes.ContainsKey(tailNode))
                {
                    nodes[tailNode] = new NodeWeighted(tailNode);
                }

                if (!nodes.ContainsKey(headNode))
                {
                    nodes[headNode] = new NodeWeighted(headNode);
                }

                var neighbourToAdd = new Tuple<NodeWeighted, int>(nodes[headNode], weight);
                var parentToAdd = new Tuple<NodeWeighted, int>(nodes[tailNode], weight);
                nodes[tailNode].Neighbours.Add(neighbourToAdd);
                nodes[headNode].Parents.Add(parentToAdd);
            }
            return nodes;
        }

        public static void GetMethodRunningTime(Func<string> method)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            method.Invoke();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

        }
        
        public static void GetMethodRunningTime(Func<IEnumerable<int>> method)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            method.Invoke();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            
        }
        
        public static void GetMethodRunningTime(Func<Dictionary<int, NodeWeighted>, int, IEnumerable<int>> method, Dictionary<int, NodeWeighted> arg1, int agr2)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            method.Invoke(arg1, agr2);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
        
        public static void GetMethodRunningTime(Func<IEnumerable<int?>> method)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            method.Invoke();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

        }
    }
}