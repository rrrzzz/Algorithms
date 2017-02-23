using System;

namespace Algorithms.HackerRank
{
    public static class StockMaximize
    {
        /*
        Each day, you can either buy one share of WOT, sell any number of shares of WOT that you own, or not make any transaction at all. 
        What is the maximum profit you can obtain with an optimum trading strategy?
        
        Input Format

        The first line contains the number of test cases T. T test cases follow:

        The first line of each test case contains a number N. The next line contains N integers, denoting the predicted price of WOT shares for the next N days.

        Constraints

        1 <= T <= 10
        1 <= N <= 50000
        All share prices are between 1 and 100000 
        */

        public static long FindMaxProfit(int pricesCount, int[] stocksPrices)
        {
            var t = int.Parse(Console.ReadLine());
            for (var i = 0; i < t; i++)
            {
                var n = int.Parse(Console.ReadLine());
                var prices = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                Console.WriteLine(FindCaseProfit(n, prices));
            }

            return FindCaseProfit(pricesCount, stocksPrices);
        }

        private static long FindCaseProfit(int i, int[] prices)
        {
            long profit = 0;

            var currentMax = prices[i - 1];
            for (var z = i - 2; z > -1; z--)
            {
                var currentPrice = prices[z];

                if (currentPrice < currentMax)
                {
                    profit += currentMax - currentPrice;
                }
                else
                {
                    currentMax = currentPrice;
                }
            }
            return profit;
        }
    }
}
