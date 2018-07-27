using System.Collections;
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
    }
}