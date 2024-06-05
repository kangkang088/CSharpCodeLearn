using System;

namespace _07冒泡排序 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = { 1,2,3,4,5,6,7,8,9,10 };
            bool isSort = false;
            for (int i = 0; i < arr.Length - 1; i++) {
                isSort = false;
                for (int j = 0; j < arr.Length - i - 1; j++) {
                    if (arr[j] > arr[j + 1]) {
                        isSort = true;
                        int temp = arr[j];
                        arr[j] = arr[j +1];
                        arr[j + 1] = temp;
                    }
                }
                if (!isSort) {
                    break;
                }
            }
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
            
        }
    }
}
