using System;

namespace _04params {
    internal class Program {
        static void Main(string[] args) {
            Print(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        private static void Print(params int[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
        }
    }
}
