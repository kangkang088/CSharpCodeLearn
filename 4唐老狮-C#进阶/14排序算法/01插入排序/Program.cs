namespace _01插入排序 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = { 8,1,5,9,4,3,7,6,2 };
            //for(int i = 1;i < arr.Length;i++) {
            //    int sorIndex = i - 1;
            //    int nowNum = arr[i];
            //    while(sorIndex >= 0 && arr[sorIndex] < nowNum) {
            //        arr[sorIndex + 1] = arr[sorIndex];
            //        sorIndex--;
            //    }
            //    arr[sorIndex + 1] = nowNum;
            //}
            for(int i = 1;i < arr.Length;i++) {
                int nowNum = arr[i];
                int sortIndex = i - 1;
                while(sortIndex >= 0 && arr[sortIndex] < nowNum) {
                    arr[sortIndex + 1] = arr[sortIndex];
                    sortIndex--;
                }
                arr[sortIndex + 1] = nowNum;
            }
            for(int i = 0; i < arr.Length;i++) {
                Console.WriteLine(arr[i]);
            }
        }

    }
}
