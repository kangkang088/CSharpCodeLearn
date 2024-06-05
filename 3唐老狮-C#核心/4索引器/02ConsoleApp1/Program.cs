using System;

namespace _02ConsoleApp1 {
    class IntArray {
        private int[] array;
        private int capacity;
        private int length;

        public int Length {
            get => length;
            set => length = value;
        }

        public IntArray() {
            capacity = 5;
            Length = 0;
            array = new int[capacity];
        }

        //add
        public void Add(int value) {
            if (Length < capacity) {
                array[Length++] = value;
            } else {
                capacity *= 5;
                int[] tempArray = new int[capacity];
                for (int i = 0; i < array.Length; i++) {
                    tempArray[i] = array[i];
                }
                array = tempArray;
                array[Length++] = value;
            }
        }
        //remove
        public void Remove(int value) {
            for (int i = 0; i < Length; i++) {
                if (array[i] == value) {
                    RemoveAt(i);
                    return;
                }
            }
            Console.WriteLine("the value is not existed.");
        }
        public void RemoveAt(int index) {
            if (index > Length - 1) {
                Console.WriteLine("当前数组长度为{0},越界", Length);
                return;
            }
            for (int i = index; i < Length - 1; i++) {
                array[i] = array[i + 1];
            }
            --Length;
        }
        //search and change
        public int this[int index] {
            get {
                if (index >= Length) {
                    Console.Write("the array has out of Arrange. : ");
                    return 0;
                }
                return array[index];
            }
            set {
                if (index >= Length || index < 0) {
                    Console.WriteLine("the array has out of Arrange.");
                }
                array[index] = value;
            }
        }
    }
    internal class Program {
        static void Main(string[] args) {
            IntArray array = new IntArray();
            array.Add(100);
            array.Add(200);
            array.Add(300);
            array.Add(400);
            array.Add(500);
            array.Add(600);
            array.Add(700);
            Console.WriteLine(array.Length);
            array.RemoveAt(1);
            Console.WriteLine(array.Length);
            Console.WriteLine(array[1]);
            array.Remove(1);
            Console.WriteLine(array[100]);
        }
    }
}
