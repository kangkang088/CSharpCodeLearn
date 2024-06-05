namespace _02插入排序 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = new int[] { 1,276,5,9,7,0,46,499,57,115,8,46,4165,4,87,466,45,788 };
            for(int step = arr.Length / 2;step > 0;step /= 2) {
                for(int i = step; i < arr.Length; i++) {
                    int nowNum = arr[i];
                    int sortNum = i - step;
                    while(sortNum >= 0 && arr[sortNum] < nowNum) {
                        arr[sortNum + step] = arr[sortNum];
                        sortNum -= step;
                    }
                    arr[sortNum + step] = nowNum;
                }
            }
            for(int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i]);
            }
        }
    }
}
