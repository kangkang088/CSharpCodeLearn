namespace _04选择排序 {
    internal class Program {
        static void Main(string[] args) {
            int[] arr = new int[] { 8,7,1,5,4,2,6,3,9 };
            //冒泡
            //for(int i = 0; i < arr.Length - 1; i++) {
            //    for(int j = 0; j < arr.Length - 1 - i; j++) {
            //        if(arr[j] < arr[j + 1]) {
            //            int temp = arr[j];
            //            arr[j] = arr[j + 1];
            //            arr[j + 1] = temp;
            //        }
            //    }
            //}
            //插入
            //for(int i = 1;i < arr.Length;i++) {
            //    int sortIndex = i - 1;
            //    int nowNum = arr[i];
            //    while(sortIndex >= 0 && arr[sortIndex] > nowNum) {
            //        arr[sortIndex + 1] = arr[sortIndex];
            //        sortIndex--;
            //    }
            //    arr[sortIndex + 1] = nowNum;
            //}
            //选择
            for(int i = 0; i < arr.Length; i++) {
                int index = 0;
                for(int j = 1;j < arr.Length - i;j++) {
                    if(arr[index] < arr[j]) {
                        index = j;
                    }
                }
                if(index != arr.Length - 1 - i) {
                    int temp = arr[index];
                    arr[index] = arr[arr.Length - 1 - i];
                    arr[arr.Length - 1 - i] = temp;
                }
            }
            for(int i = 0;i < arr.Length;i++) {
                Console.WriteLine(arr[i]);
            }
            
        }
    }
}
