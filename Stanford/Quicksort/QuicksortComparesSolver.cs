using System;
using System.Linq;
using System.Net;

namespace Algorithms.Stanford.Quicksort
{
    public static class QuicksortComparesSolver
    {
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