using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = genRandomArray(10000, 0, 2000);
            int[] test2 = cloneArray(test);
            int[] test3 = cloneArray(test);
            DateTime start, end;
            //printArray(test);

            start = DateTime.Now;
            bubbleSort(test);
            end = DateTime.Now;
            Debug.WriteLine("Bubble sort time = " + String.Format("{0:0.00}", (end - start).TotalMilliseconds) + "ms");
            //printArray(test);

            start = DateTime.Now;
            int[] testSmerge = mergeSort(test2);
            end = DateTime.Now;
            Debug.WriteLine("Merge sort time = " + String.Format("{0:0.00}", (end - start).TotalMilliseconds) + "ms");
            //printArray(testSmerge);

            start = DateTime.Now;
            quickSort(test3, 0, test3.Length - 1);
            end = DateTime.Now;
            Debug.WriteLine("Quick sort time = " + String.Format("{0:0.00}", (end - start).TotalMilliseconds) + "ms");

            //printArray(test3);
        }

        public static void quickSort(int[] x, int pL, int pR) {
            if (pL == pR) {
                return;
            }
            int pivot = new Random().Next(pL, pR);
            int split = partition(x, pivot, pL, pR);
            //If the pivot ends up on one of the ends of the array
            int pLA, pRA, pLB, pRB;
            if (split == pL)
            {
                pLA = pL;
                pRA = pL;
                pLB = split + 1;
                pRB = pR;
            }
            else if (split == pR)
            {
                pLA = pL;
                pRA = split-1;
                pLB = pR;
                pRB = pR;
            }
            else {
                pLA = pL;
                pRA = split;
                pLB = split + 1;
                pRB = pR;
            }
            quickSort(x, pLA, pRA);
            quickSort(x, pLB, pRB);
        }

        public static int partition(int[] x, int pivot, int pL, int pR) {
            int pivotValue = x[pivot];
            x[pivot] = x[pL];
            x[pL] = pivotValue;
            int temp = 0;
            int m = pL + 1;
            for (int n = pL + 1; n <= pR; n++ ) {
                if (x[n] < pivotValue) {
                    temp = x[m];
                    x[m] = x[n];
                    x[n] = temp;
                    m = m + 1;
                }
            }

            temp = x[pL];
            x[pL] = x[m - 1];
            x[m - 1] = temp;
            return m - 1;
        }

        public static int[] mergeSort(int[] x)
        {
            if (x.Length == 1)
            {
                //There is nothing to sort, just return the array back up.
                return x;
            }
            else
            {
                //Split the array into 2 halfs
                int[] A, B;
                //We have to split the array differently depending on if there are an even or odd number of elements
                if (x.Length % 2 == 0)
                {
                    int M = x.Length / 2;
                    A = new int[M];
                    B = new int[M];
                    for (int m = 0; m < M; m++)
                    {
                        A[m] = x[m];
                        B[m] = x[M + m];
                    }
                }
                else
                {
                    int M = (x.Length - 1) / 2;
                    A = new int[M];
                    B = new int[M + 1];
                    for (int m = 0; m < M; m++)
                    {
                        A[m] = x[m];
                        B[m] = x[M + m];
                    }
                    B[M] = x[2 * M];
                }

                //Now sort the 2 halfs using merge sort
                int[] As = mergeSort(A);
                int[] Bs = mergeSort(B);

                //Now that A and B are sorted, merge the two arrays together
                int ai = 0;
                int bi = 0;
                int[] xs = new int[x.Length];
                for (int n = 0; n < x.Length; n++)
                {
                    //Make sure our indices don't run out of bounds
                    if (ai < As.Length && bi < Bs.Length)
                    {
                        if (As[ai] < Bs[bi])
                        {
                            xs[n] = As[ai];
                            ai++;
                        }
                        else
                        {
                            xs[n] = Bs[bi];
                            bi++;
                        }
                    }
                    else if (ai >= As.Length)
                    { //If we use everything from A, just copy over B
                        xs[n] = Bs[bi];
                        bi++;
                    }
                    else
                    { //This is when we have used every element in B, and now just copy over A
                        xs[n] = As[ai];
                        ai++;
                    }
                }
                return xs;
            }
        }

        public static void bubbleSort(int[] x) {
            int N = x.Length;
            for (int n = 0; n < N; n++) {
                for (int m = 0; m < N-1; m++) {
                    if (x[m] > x[m + 1]) {
                        int temp = x[m + 1];
                        x[m + 1] = x[m];
                        x[m] = temp;
                    }
                }
            }
        }

        public static int[] genRandomArray(int N, int min, int max) {
            int[] x = new int[N];
            Random rng = new Random(N + min + max);
            for (int n = 0; n < N; n++) {
                x[n] = rng.Next(min, max);
            }
            return x;
        }

        public static int[] cloneArray(int[] x) {
            int N = x.Length;
            int[] y = new int[N];
            for (int n = 0; n < N; n++) {
                y[n] = x[n];
            }
            return y;
        }

        public static void printArray(int[] x)
        {
            for (int n = 0; n < x.Length; n++)
            {
                Debug.Write(x[n] + ", ");
            }
            Debug.WriteLine("");
        }

    }
}
