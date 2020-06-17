using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*************************************************************************************/
//  The following exercises are the solutions of the basic searching and 
//  sorting algorithms in C# from w3resource page.
//  Exercise 1: Write a C# Sharp program to sort a list of elements using Shell sort.
//  Exercise 2: Write a C# Sharp program to sort a list of elements using Bubble sort.
//  Exercise 3: Write a C# Sharp program to sort a list of elements using Count sort.
//  Exercise 4: Write a C# Sharp program to sort a list of elements using Merge sort.
//  Exercise 5: Write a C# Sharp program to sort a list of elements using Quick sort.
/*************************************************************************************/

namespace SearchAndSorting
{
    class Program
    {
        //  Helper function that prints the array elements:
        static void show_array_Elements(int[] myArray)
        {
            foreach (int element in myArray)
            {
                Console.Write(element + "   ");
            }
            Console.WriteLine();
        }
        /********************************************************************/
        //              Searching and sorting:
        /********************************************************************/

        /*****************************************************/
        //              1. SHELL SORT ALGORITHM:
        /*****************************************************/
        static void shellSort(int[] myArray, int len)
        {
            int j, gap, temp;
            gap = 3;
            // While gap exist: Start with the largest gap and work down to a gap of 1
            while (gap > 0)
            {
                //  Do a gapped insertion sort for this gap size:
                //  The first gap elements a[0..gap-1] are already in gapped order
                //  keep adding one more element until the entire array is gap sorted
                for (int i = 0; i < len; i++)
                {
                    //  add a[i] to the elements that have been gap sorted
                    //  save a[i] in temp and make a hole at position i
                    j = i;
                    temp = myArray[i];
                    //  shift earlier gap-sorted elements up until the correct 
                    //  location for a[i] is found
                    while ((j >= gap) && (myArray[j - gap] > temp))
                    {
                        myArray[j] = myArray[j - gap];
                        j = j - gap;
                    }

                    //put temp (the original a[i]) in its correct location
                    myArray[j] = temp;
                }
                //  Make the gap smaller
                if (gap / 2 != 0)
                    gap = gap / 2;
                else if (gap == 1)
                    gap = 0;
                else
                    gap = 1;
            }
        }


        /*****************************************************/
        //            2. BUBBLE SORT ALGORITHM:
        /*****************************************************/
        static void swap(int[] myArray, int i, int j)
        {
            int tmp = myArray[i];
            myArray[i] = myArray[j];
            myArray[j] = tmp;
        }

        static void BubbleSort(int[] myArray, int len)
        {
            //  The idea is to run through the array swap elements
            //  if they are not sorted
            bool isSorted = false;  //value that check if the array is sorted
            int lastUnsorted = len - 1;
            //  While the array is not sorted
            while (!isSorted)
            {
                isSorted = true;    // when sorted become true
                //  get pairs starting form the first 2 numbers
                for (int i = 0; i < lastUnsorted; i++)
                {
                    //  If the elements are not sorted swap the values
                    if (myArray[i] > myArray[i+1])
                    {
                        swap(myArray, i, i + 1);
                        isSorted = false;
                    }
                }
                lastUnsorted--;
            }
        }

        /*****************************************************/
        //            3. COUNT SORT ALGORITHM:
        /*****************************************************/
        //  The idea behind:
        //  First Part: Find the start index of each numeber
        //  e.g. Array = {1, 0, 3, 1, 3, 1} => 0=>1 time/1=>3itmes/3=>2 times
        //  | 0 | 1 | 2 | 3 |
        //  | 1 | 3 | 0 | 2 |
        //  Store this numbers in an array of length for this particular phase
        //  Next step:
        //  Then we add every next number with the main number
        //  | 0 | 1 | 2 | 3 |
        //  | 1 | 4 | 4 | 6 |
        //  Next step:
        //  Shift that array by 1 cell from write to left
        //  So the table become:
        //  | 0 | 1 | 2 | 3 |
        //  | 0 | 1 | 4 | 4 |
        //  Second Part:
        //  Initialise a new array with the same length as first array
        //  Add in array the coresponding elements
        //  Properties:
        //  1)  Stable
        //  2)  Time O(n+k) -> n = numbers in array and k = number of repeditions

        static void CountSort(int[] myArray, int len)
        {
            //Phase 2: Sorted Array Initialisation
            int [] sortedArray = new int[len];

            //Find Smallest and biggest value:
            int minValue = myArray[0];
            int maxValue = myArray[0];
            for (int i = 1; i < len; i++)
            {
                if (myArray[i] < minValue) minValue = myArray[i];
                else if (myArray[i] > maxValue) maxValue = myArray[i];
            }

            // Phase 1: initialise array of frequencies
            int[] counts = new int[maxValue - minValue + 1];

            // initialise the frequencies
            for (int i = 0; i < myArray.Length; i++)
            {
                counts[myArray[i] - minValue]++;
            }

            // Recalculation of counts
            counts[0]--;
            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] = counts[i] + counts[i - 1];
            }

            //Final step: Sort the array:
            for (int i = len - 1; i >= 0; i--)
            {
                sortedArray[counts[myArray[i] - minValue]--] = myArray[i];
            }

            //Assign the sorted array elements in my main array
            //so we are not breaking the programs flow
            for (int i = 0; i < len; i++)
            {
                myArray[i] = sortedArray[i];
            }
        }

        /*****************************************************/
        //            4. MERGE SORT ALGORITHM:
        /*****************************************************/
        //  Based on: https://www.youtube.com/watch?v=4VqmGXwpLqc
        //  Worst case scenario complexity: O(nlogn)
        static void Merge(int[] input, int left, int middle, int right)
        {
            //  Initialise the left and right array
            int[] leftArray = new int[middle - left + 1];
            int[] rightArray = new int[right - middle];

            //  Creates copy of arrays 
            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            //  Counters for left and right side iterations
            int i = 0;
            int j = 0;

            //  Reposition items
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i] <= rightArray[j])
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        static void MergeSort(int[] input, int left, int right)
        {
           //   Function that breaks down the array in idividual items recursively
           //   to left and righ sides
           //   then calls the merge function which makes the idividual items in
           //   arrays and reposition the items (sort) until we have a whole new array
           //   with sorted items.

            if (left < right)
            {
                //  Find the middle
                int middle = (left + right) / 2;

                //  Recursive calls
                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }


        /*****************************************************/
        //            5. QUICK SORT ALGORITHM:
        /*****************************************************/
        //  When using Quick Sort we use something called pivot
        //  need to folow the 3 conditions:
        //  1)  Correct possition in final, sorted array
        //  2)  Items to left are smaller
        //  3)  Items to right are bigger
        static void QuickSort(int[] arr, int left, int right)
        {
            //  stop when left < right
            if (left < right)
            {
                //  Finding the pivot of each new array
                int pivot = Partition(arr, left, right);

                /***********************************************/
                //              Recursive calls
                /***********************************************/
                // if pivot position is > 1 then do quick sort again in left side
                if (pivot > 1)
                    QuickSort(arr, left, pivot - 1);
                
                // if pivot position is < 1 then do quick sort again in right side
                if (pivot + 1 < right)
                    QuickSort(arr, pivot + 1, right);
            }
        }

        static int Partition(int[] arr, int left, int right)
        {
            //  Set pivot to the leftest item in each array
            int pivot = arr[left];
            while (true)
            {
                //  While the leftest item in the array is smaller than the pivot
                //  increase the left by 1
                while (arr[left] < pivot)
                    left++;

                //  While the rightest item in the array is bigger than the pivot
                //  decrease the right by 1
                while (arr[right] > pivot)
                    right--;

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;
                    //  Swap
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                    return right;
            }
        }

        static void Main(string[] args)
        {
            //Initialise the array:
            //  used in Shell/Bubble/Count
            int[] myArray = new int[] { 5, -4, 11, 0, 18, 22, 67, 51, 6 };
            int len = myArray.Length;

            //Print menu of available Sorting Algorithms:
            Console.WriteLine("Which Algorithm do you want to use?");
            Console.WriteLine("Press 1: Shell Sort");
            Console.WriteLine("Press 2: Bubble Sort");
            Console.WriteLine("Press 3: Count Sort");
            Console.WriteLine("Press 4: Merge Sort");
            Console.WriteLine("Press 5: Quick Sort");
            Console.WriteLine("Press 6: Heap Sort");

            //Get input:
            Console.Write("Input: ");
            int input = int.Parse(Console.ReadLine());

            //Print original array:
            Console.WriteLine("Original Array: ");
            show_array_Elements(myArray);

            switch (input)
            {
                case 1:
                    shellSort(myArray, len);
                    Console.WriteLine("Sorted Array:");
                    show_array_Elements(myArray);
                    break;
                case 2:
                    BubbleSort(myArray, len);
                    Console.WriteLine("Sorted Array:");
                    show_array_Elements(myArray);
                    break;
                case 3:
                    CountSort(myArray, len);
                    Console.WriteLine("Sorted Array:");
                    show_array_Elements(myArray);
                    break;
                case 4:
                    MergeSort(myArray, 0, len-1);
                    Console.WriteLine("Sorted Array:");
                    show_array_Elements(myArray);
                    break;
                case 5:
                    QuickSort(myArray, 0, len-1);
                    Console.WriteLine("Sorted Array:");
                    show_array_Elements(myArray);
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }

            Console.ReadLine();
        }
    }
}
