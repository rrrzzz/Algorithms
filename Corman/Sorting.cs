using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Corman.HeapStructure;

namespace Algorithms.Corman
{
    public class Sorting
    {
        public void SelectionSort(List<int> ints)
        {
            var length = ints.Count;
            for (int i = 0; i < length - 1; i++)
            {
                var currentMin = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (ints[currentMin] > ints[j])
                    {
                        currentMin = j;
                    }
                }

                if (currentMin != i)
                {
                    var temp = ints[i];
                    ints[i] = ints[currentMin];
                    ints[currentMin] = temp;
                }
            }

            Console.WriteLine(string.Join(", ", ints));
            Console.ReadLine();
        }

        public void HeapSort(int[] arrayToSort)
        {
            var heap = new Heap(arrayToSort);
            heap.BuildMaxHeap();
            
            while (heap.HeapSize > 1)
            {
                heap.SwapHeapValues(1, heap.HeapSize--);
                heap.MaxHeapify(1);
            }
        }

        public void Quicksort(int[] arrayToSort)
        {
            var startIndex = 0;
            var endIndex = arrayToSort.Length - 1;

            QuicksortRecursive(arrayToSort, startIndex, endIndex);
        }

        private void QuicksortRecursive(int[] arrayToSort, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var pivotIndex = Partition(arrayToSort, startIndex, endIndex);
                QuicksortRecursive(arrayToSort, startIndex, pivotIndex - 1);
                QuicksortRecursive(arrayToSort, pivotIndex + 1, endIndex);
            }
        }

        private int Partition(int[] arrayToSort, int startIndex, int endIndex)
        {
            var pivotIndex = endIndex;
            var pivotValue = arrayToSort[pivotIndex];
            var smallerPartitionEndIndex = startIndex - 1;
            for (int currentPosition = startIndex; currentPosition < pivotIndex; currentPosition++)
            {
                if (arrayToSort[currentPosition] <= pivotValue)
                {
                    UtilityMethods.SwapValues(arrayToSort, ++smallerPartitionEndIndex, currentPosition);
                }
            }

            pivotIndex = smallerPartitionEndIndex + 1;
            UtilityMethods.SwapValues(arrayToSort, pivotIndex, endIndex);

            return pivotIndex;
        }

        public int[] NaiveCountingSort(int[] array, int range)
        {
            var valuesCount = new int[range + 1];
            var output = new int[array.Length];
            var currentIndex = 0;

            foreach (var i in array)
            {
                valuesCount[i] += 1;
            }

            for (var i = 0; i <= range; i++)
            {
                var valueCount = valuesCount[i];
                while (valueCount != 0)
                {
                    output[currentIndex++] = i;
                    valueCount--;
                }    
            }

            return output;
        }

        public int[] CountingSortStable(int[] arrayToSort, int range)
        {
            var valuesCount = new int[range];
            var output = new int[arrayToSort.Length];

            foreach (var i in arrayToSort)
            {
                valuesCount[i] += 1;
            }

            for (int i = 1; i < range; i++)
            {
                valuesCount[i] += valuesCount[i - 1];
            }

            // inverse for loop to preserve order of elements with satellite data
            for (int i = arrayToSort.Length - 1; i >= 0; i--)
            {
                output[valuesCount[arrayToSort[i]] - 1] = arrayToSort[i];
                valuesCount[arrayToSort[i]]--;
            }

            return output;
        }

        public int[] RadixSort(int[] arrayToSort, int digitsCount)
        {
            for (var digitsProcessed = 0; digitsProcessed < digitsCount; digitsProcessed++)
            {
                var divisor = (int)Math.Pow(10, digitsProcessed);
                var singleDigitArray = arrayToSort.Select(x => x / divisor % 10).ToArray();
                arrayToSort = RadixSubroutineCountingSort(singleDigitArray, arrayToSort);
            }

            return arrayToSort;
        }

        public string[] RadixCharacterSort(string[] words)
        {
            var wordLength = words[0].Length;
            var wordNumberRepresentation = new int[words.Length];
            var numberToWord = new Dictionary<int, string>();

            for (var q = 0; q < words.Length; q++)
            {
                var currentWord = words[q];
                var numberRepresentation = 0;

                for (int i = 0, z = (wordLength - 1) * 2; i < wordLength; i++, z -= 2)
                {
                    var multiplier = (int)Math.Pow(10, z);
                    numberRepresentation += char.ToUpper(currentWord[i]) * multiplier;
                }
                numberToWord.Add(numberRepresentation, currentWord);
                wordNumberRepresentation[q] = numberRepresentation;
            }

            wordNumberRepresentation = RadixSort(wordNumberRepresentation, wordLength * 2);

            return wordNumberRepresentation.Select(x => numberToWord[x]).ToArray();
        }

        private int[] RadixSubroutineCountingSort(int[] singleDigitsArray, int[] originalArray, int range = 10)
        {
            var valuesCount = new int[range];
            var output = new int[singleDigitsArray.Length];

            foreach (var i in singleDigitsArray)
            {
                valuesCount[i] += 1;
            }

            for (int i = 1; i < range; i++)
            {
                valuesCount[i] += valuesCount[i - 1];
            }

            for (int i = singleDigitsArray.Length - 1; i >= 0; i--)
            {
                output[valuesCount[singleDigitsArray[i]] - 1] = originalArray[i];
                valuesCount[singleDigitsArray[i]]--;
            }

            return output;
        }
    }
}