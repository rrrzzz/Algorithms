using System;
using System.Linq;
using System.Net;
using Algorithms.Stanford.PivotPartition;

namespace Algorithms.Stanford.Quicksort
{
    public static class QuicksortComparesSolver
    {
        // default: https://lagunita.stanford.edu/assets/courseware/v1/e4180be5ec3e5b00f55703423698327f/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/QuickSort.txt
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