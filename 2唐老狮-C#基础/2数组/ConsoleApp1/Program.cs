using System;

namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = new int[100];
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = i;
            }



            string[] arr2 = new string[25];
            for (int i = 0; i < arr2.Length; i++) {
                arr2[i] = i%2 == 0 ? "■" : "□";
            }
            for (int i = 0; i < arr2.Length; i++) {
                Console.Write(arr2[i]);
                if (i % 5 + 1 == 5) {
                    Console.WriteLine();
                }
            }
        }
    }
}
