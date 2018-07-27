using System;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.Sorting.Quicksort
{
    public static class QuicksortComparesSolver
    {
        /*The file contains all of the integers between 1 and 10,000 (inclusive, with no repeats) in unsorted order.The integer in the row of the file gives you the  entry of an input array.

            Your task is to compute the total number of comparisons used to sort the given input file by QuickSort. As you know, the number of comparisons depends on which elements are chosen as pivots, so we'll ask you to explore three different pivoting rules.

           You should not count comparisons one-by-one.Rather, when there is a recursive call on a subarray of length, you should simply add  to your running total of comparisons. (This is because the pivot element is compared to each of the other  elements in the subarray in this recursive call.)

            1. Calculate number of comparisons always choosing first element of array as pivot.
            2. Calculate number of comparisons always choosing last element of array as pivot.
            3. Calculate number of comparisons always choosing median element of array as pivot.

         https://lagunita.stanford.edu/assets/courseware/v1/e4180be5ec3e5b00f55703423698327f/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/QuickSort.txt */

        public static void Solve(string linkToArray)
        {
            var webArray = GetArrayFromWeb(linkToArray);
            var arrayForLast = (int[])webArray.Clone();
            var arrayForMedian = (int[])webArray.Clone();

            var comparesFirst = new QuicksortComparesCounter(webArray, PivotSelect.First);
            var comparesLast = new QuicksortComparesCounter(arrayForLast, PivotSelect.Last);
            var comparesMedian = new QuicksortComparesCounter(arrayForMedian, PivotSelect.Median);

            Console.WriteLine($"Compares first: {comparesFirst.GetComparisons()}\n" +
                              $"Compares last: {comparesLast.GetComparisons()}\n" +
                              $"Compares median: {comparesMedian.GetComparisons()}");
            Console.ReadLine();
        }

        private static int[] GetArrayFromWeb(string link)
        {
            string content;

            using (var client = new WebClient())
            {
                content = client.DownloadString(link).TrimEnd();
            }

            var stringArray = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return stringArray.Select(int.Parse).ToArray();
        }
    }
}