using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Reflection;
using Algorithms.Stanford;

namespace Algorithms
{
    public static class UtilityMethods
    {
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
    }
}