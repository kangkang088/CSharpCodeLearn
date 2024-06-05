using System.Collections;

namespace _01迭代器 {
    class CustomList : IEnumerable, IEnumerator {
        private int[] list;
        //光标
        private int postion = -1;
        public CustomList() {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        #region IEnumerable
        public IEnumerator GetEnumerator() {
            Reset();
            return this;
        }
        #endregion

        #region IEnumerator
        public object Current {
            get {
                return list[postion];
            }
        }

        public bool MoveNext() {
            //每次都移动光标
            ++postion;
            //溢出就不合法
            return postion < list.Length;
        }

        public void Reset() {
            postion = -1;
        }
        #endregion 
    }
    class CustomList2 : IEnumerable {
        private int[] list;

        public CustomList2() {
            list = new int[] { 1,2,3,4,5,6,7,8,9,10 };
        }

        public IEnumerator GetEnumerator() {
            for (int i = 0; i < list.Length; ++i) {
                yield return list[i];
            }
        }
    }
    internal class Program {
        static void Main(string[] args) {
            //CustomList list = new CustomList();
            //foreach (var item in list) {
            //    Console.WriteLine(item);
            //}
            //foreach (var item in list) {
            //    Console.WriteLine(item);
            //}
            CustomList2 list2 = new CustomList2();
            foreach (int item in list2)
            {
                Console.WriteLine(item);
            }
            foreach (int item1 in list2) {
                Console.WriteLine(item1);
            }
        }
    }
}
