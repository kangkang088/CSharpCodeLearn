using System;

namespace _08选择排序 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = new int[20];
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = random.Next(0, 101);
            }
            //降序
            for (int i = 0; i < arr.Length - 1; i++) {
                int max = i;
                for (int j = i + 1; j < arr.Length; j++) {
                    if (arr[j] > arr[max]) {
                        max = j;
                    }
                }
                if (max != i) {
                    int temp = arr[i];
                    arr[i] = arr[max];
                    arr[max] = temp;
                }
            }
            Console.WriteLine("降序：");
            for (int i = 0; i < arr.Length; i++) {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("升序：");
            //升序
            for (int i = 0; i < arr.Length - 1; i++) {
                int min = i;
                for (int j = i + 1; j < arr.Length; j++) {
                    if (arr[j] < arr[min]) {
                        min = j;
                    }
                }
                if (min != i) {
                    int temp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = temp;
                }
            }
            for (int i = 0; i < arr.Length; i++) {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
