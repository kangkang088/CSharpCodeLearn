using System.Collections;

namespace _02练习 {
    class CustomList : IEnumerable, IEnumerator {
        private int[] list;
        private int index;

        public CustomList() {
            list = [1, 2, 3, 4, 5];
        }

        public object Current {
            get {
                return list[index];
            }
        }

        public IEnumerator GetEnumerator() {
            Reset();
            return this;
        }

        public bool MoveNext() {
            index++;
            return index < list.Length;
        }

        public void Reset() {
            index = -1;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            CustomList customList = new CustomList();
            foreach (int i in customList) {
                Console.WriteLine(i);
            }
            foreach (int i in customList) {
                Console.WriteLine(i);
            }
        }
    }
}
