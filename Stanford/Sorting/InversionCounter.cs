using System.Numerics;

namespace Algorithms.Stanford.Sorting
{
    class InversionCounter
    {
        public BigInteger CountInversions(int[] arrayToCount)
        {
            if (arrayToCount.Length < 2)
            {
                return 0;
            }

            var copy = (int[])arrayToCount.Clone();

            return MergeSortAndCountInversions(0, copy.Length - 1, copy);
        }

        private BigInteger MergeSortAndCountInversions(int l, int r, int[] arr)
        {
            if (l < r)
            {
                var m = (l + r) / 2;

                var lhsInversions = MergeSortAndCountInversions(l, m, arr);
                var rhsInversions = MergeSortAndCountInversions(m + 1, r, arr);
                var splitInversions = MergeArrays(l, m, r, arr);

                var totalInversions = lhsInversions + rhsInversions + splitInversions;
                return totalInversions;
            }

            return 0;
        }

        private int MergeArrays(int l, int m, int r, int[] arr)
        {
            var splitInversions = 0;
            var A = new int[m - l + 1];
            var B = new int[r - m];

            for (int i = 0; i < A.Length; i++)
            {
                A[i] = arr[l + i];
            }

            for (int i = 0; i < B.Length; i++)
            {
                B[i] = arr[m + 1 + i];
            }

            int lhsIndex = 0, rhsIndex = 0;

            while (lhsIndex < A.Length && rhsIndex < B.Length)
            {
                if (A[lhsIndex] > B[rhsIndex])
                {
                    splitInversions += A.Length - lhsIndex;
                    arr[l] = B[rhsIndex++];
                }
                else
                {
                    arr[l] = A[lhsIndex++];
                }
                l++;
            }

            while (lhsIndex < A.Length)
            {
                arr[l++] = A[lhsIndex++];
            }

            while (rhsIndex < B.Length)
            {
                arr[l++] = B[rhsIndex++];
            }

            return splitInversions;
        }
    }
}