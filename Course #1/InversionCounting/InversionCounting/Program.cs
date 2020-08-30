using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InversionCounting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int N = 100000;
            int[] test = new int[N];
            Random rng = new Random();
            for (int n = 0; n < N; n++) {
                test[n] = rng.Next(0, N);
            }

            DateTime start = DateTime.Now;
            Debug.WriteLine("Number of inversions (Brute Force) = " + inversionCountBruteForce(test));
            DateTime end = DateTime.Now;
            Debug.WriteLine("Brute force count time = " + String.Format("{0:0.00}", (end-start).TotalMilliseconds) + "ms");

            int[] testSort;
            start = DateTime.Now;
            int inverCount = sortAndCountInversions(test, out testSort);
            end = DateTime.Now;
            Debug.WriteLine("Number of inverions (Divide and Conquer) = " + inverCount);
            Debug.WriteLine("Divide and conquer count time = " + String.Format("{0:0.00}", (end - start).TotalMilliseconds) + "ms");
        }

        public static int inversionCountBruteForce(int[] x) {
            int inversionCount = 0;
            int N = x.Length;
            for (int n = 0; n < N; n++) {
                for (int m = n; m < N; m++) {
                    if (x[n] > x[m]) {
                        inversionCount++;
                    }
                }
            }
            return inversionCount;
        }

        public static int sortAndCountInversions(int[] A, out int[] Asort) {
            //Base case
            if (A.Length == 1){
                Asort = A;
                return 0;
            }
            else {
                //Split the array into 2 halfs
                int[] B, C;
                if (A.Length % 2 == 0)
                {
                    int L = A.Length / 2;
                    B = new int[L];
                    C = new int[L];
                    for (int n = 0; n < L; n++)
                    {
                        B[n] = A[n];
                        C[n] = A[L + n];
                    }
                }
                else {
                    int L = (A.Length - 1) / 2;
                    B = new int[L];
                    C = new int[L + 1];
                    for (int n = 0; n < L; n++)
                    {
                        B[n] = A[n];
                        C[n] = A[L+n];
                    }
                    C[L] = A[A.Length - 1];
                }

                int[] Bsort, Csort, Dsort;
                int leftCount = sortAndCountInversions(B, out Bsort);
                int rightCount = sortAndCountInversions(C, out Csort);
                int splitCount = mergeAndCountSplitInversion(Bsort, Csort, out Dsort);
                Asort = Dsort;
                return leftCount + rightCount + splitCount;
            }
        }
        public static int mergeAndCountSplitInversion(int[] Bs, int[] Cs, out int[] Ds)
        {
            int Bsi = 0;
            int Csi = 0;
            int BsL = Bs.Length;
            int CsL = Cs.Length;
            int N = BsL + CsL;
            Ds = new int[N];
            int splitCount = 0;

            for (int n = 0; n < N; n++)
            {
                //Make sure we don't run out of bounds
                if (Bsi < Bs.Length && Csi < Cs.Length)
                {
                    if (Bs[Bsi] <= Cs[Csi])
                    {
                        Ds[n] = Bs[Bsi];
                        Bsi++;
                    }
                    else
                    {
                        Ds[n] = Cs[Csi];
                        Csi++;
                        splitCount = splitCount + (BsL - Bsi);
                    }
                }
                else if (Bsi >= BsL)
                {
                    //We have used all  of the elements in Bs
                    Ds[n] = Cs[Csi];
                    Csi++;
                    splitCount = splitCount + (BsL - Bsi);
                }
                else
                {
                    //We have used all of the elements in Cs
                    Ds[n] = Bs[Bsi];
                    Bsi++;
                }
            }
            return splitCount;
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
