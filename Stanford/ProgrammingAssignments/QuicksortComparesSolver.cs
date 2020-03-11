using System;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Sorting;
using Algorithms.Stanford.Sorting.PivotPartition;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class QuicksortComparesSolver
    {
        /*The file contains all of the integers between 1 and 10,000 (inclusive, with no repeats) in unsorted order.The integer in the row of the file gives you the  entry of an input array.

            Your task is to compute the total number of comparisons used to sort the given input file by QuickSort. As you know, the number of comparisons depends on which elements are chosen as pivots, so we'll ask you to explore three different pivoting rules.

           You should not count comparisons one-by-one.Rather, when there is a recursive call on a subarray of length, you should simply add  to your running total of comparisons. (This is because the pivot element is compared to each of the other  elements in the subarray in this recursive call.)

            1. Calculate number of comparisons always choosing first element of array as pivot.
            2. Calculate number of comparisons always choosing last element of array as pivot.
            3. Calculate number of comparisons always choosing median element of array as pivot.

            answers are:  Compares first: 162085
                        Compares last: 164123
                        Compares median: 138382
        */

        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_32387ba40b36359a38625cbb397eee65_QuickSort.txt?Expires=1584057600&Signature=HnkP8c3moPFSnn58xmxT-BwP3qpwz-4OfIBj4rjNUxk23PJWA5GV7IMN-1xnWYii4K6xLoIRHheUAixPGQvOZM-oAKQgJkLruEk3D-NBtr4tepZv8Uqu4rpTuglYLiUizYZxnUYr3gx70SdkuqandFwniasQRerPm7aKNrP6QVE_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        
        public static void Solve()
        {
            var webArray = GetArrayFromWeb(Link);
            var arrayForLast = (int[])webArray.Clone();
            var arrayForMedian = (int[])webArray.Clone();

            var comparesFirst = new QuicksortComparesCounter(webArray, PivotSelect.First);
            var comparesLast = new QuicksortComparesCounter(arrayForLast, PivotSelect.Last);
            var comparesMedian = new QuicksortComparesCounter(arrayForMedian, PivotSelect.Median);

            Console.WriteLine($"Compares first: {comparesFirst.GetComparisons()}\n" +
                              $"Compares last: {comparesLast.GetComparisons()}\n" +
                              $"Compares median: {comparesMedian.GetComparisons()}");
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