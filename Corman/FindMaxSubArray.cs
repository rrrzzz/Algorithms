using System;

namespace Algorithms.Corman
{
    public static class FindMaxSubArray
    {
        public static void FindMaxSubarrayBrute(int[] array)
        {
            int maxSum = array[0];
            var maxIndexes = new[] { 0, 0 };

            for (int i = 0; i < array.Length; i++)
            {
                var currentSum = 0;
                for (int j = i; j < array.Length; j++)
                {
                    currentSum += array[j];
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        maxIndexes[0] = i;
                        maxIndexes[1] = j;
                    }
                }
            }

            Console.WriteLine($"Max sum is {maxSum}. Max subarray starts at: {maxIndexes[0]}, ends at: {maxIndexes[1]}.");
            Console.ReadLine();
        }

        public static int[] FindMaxSubarrayRecursive(int[] array, int low, int high)
        {
            if (low == high)
            {
                return new[] { low, high, array[high] };
            }

            var mid = (low + high) / 2;

            var leftSum = FindMaxSubarrayRecursive(array, low, mid);
            var crossSum = FindMaxCrossingSubArray(array, mid, low, high);
            var rightSum = FindMaxSubarrayRecursive(array, mid + 1, high);

            if (leftSum[2] >= crossSum[2] && leftSum[2] >= rightSum[2])
            {
                return leftSum;
            }

            if (rightSum[2] >= crossSum[2] && rightSum[2] >= leftSum[2])
            {
                return rightSum;
            }

            return crossSum;

        }

        public static int[] FindMaxCrossingSubArray(int[] array, int mid, int low, int high)
        {
            int maxLeftIndex, maxRightIndex;

            var maxLeftSum = array[mid];
            var maxRightSum = array[mid + 1];

            var currentSum = 0;
            for (int i = maxLeftIndex = mid; i >= low; i--)
            {
                currentSum += array[i];
                if (currentSum > maxLeftSum)
                {
                    maxLeftSum = currentSum;
                    maxLeftIndex = i;
                }
            }

            currentSum = 0;

            for (int i = maxRightIndex = mid + 1; i <= high; i++)
            {
                currentSum += array[i];
                if (currentSum > maxRightSum)
                {
                    maxRightSum = currentSum;
                    maxRightIndex = i;
                }
            }

            return new[] { maxLeftIndex, maxRightIndex, maxRightSum + maxLeftSum };
        }
    }
}
