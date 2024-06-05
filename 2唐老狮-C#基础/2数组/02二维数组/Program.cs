using System;

namespace _02二维数组 {
    internal class Program {
        static void Main(string[] args) {
            int[,] arr = new int[4, 4];
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    arr[i, j] = 1;
                    if (i >= 0 && i <= 1 && j >= 2 && j <= 3) {
                        arr[i, j] = 0;
                    }
                }
            }
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    Console.Write(arr[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
