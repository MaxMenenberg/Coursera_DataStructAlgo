using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1{
    class Program{
        static void Main(string[] args){
            double[] test = new double[] {4,3,7,1,2,3,8,8,6,5};
            printArray(test);
            printArray(mergeSort(test));
        }

        public static double[] mergeSort(double[] x) {
            if (x.Length == 1){
                //There is nothing to sort, just return the array back up.
                return x;
            }
            else {
                //Split the array into 2 halfs
                double[] A, B;
                //We have to split the array differently depending on if there are an even or odd number of elements
                 if (x.Length % 2 == 0){
                    int M = x.Length / 2;
                    A = new double[M];
                    B = new double[M];
                    for (int m = 0; m < M; m++) {
                        A[m] = x[m];
                        B[m] = x[M + m];
                    }
                }
                else {
                    int M = (x.Length-1) / 2;
                    A = new double[M];
                    B = new double[M+1];
                    for (int m = 0; m < M; m++){
                        A[m] = x[m];
                        B[m] = x[M + m];
                    }
                    B[M] = x[2*M];
                }

                //Now sort the 2 halfs using merge sort
                double[] As = mergeSort(A);
                double[] Bs = mergeSort(B);

                //Now that A and B are sorted, merge the two arrays together
                int ai = 0;
                int bi = 0;
                double[] xs = new double[x.Length];
                for (int n = 0; n < x.Length; n++) {
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
                    else { //This is when we have used every element in B, and now just copy over A
                        xs[n] = As[ai];
                        ai++;
                    }
                }
                return xs;
            }
        }

        public static void printArray(double[] x) {
            for (int n = 0; n < x.Length; n++) {
                Debug.Write(x[n] + ", ");
            }
            Debug.WriteLine("");
        }
    }
}
