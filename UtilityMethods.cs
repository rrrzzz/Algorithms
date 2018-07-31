using System.Collections;
using System.IO;
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
    }
}