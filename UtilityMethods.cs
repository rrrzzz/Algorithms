using System.Collections;

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
    }
}