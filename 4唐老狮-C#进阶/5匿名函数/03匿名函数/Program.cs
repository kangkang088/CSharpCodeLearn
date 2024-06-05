namespace _03匿名函数 {
    internal class Program {
        static void Main(string[] args) {
            Test().Invoke();
        }
        static Func<int,int> TestFunc(int i) {
            return delegate (int k) {
                return i * k;
            };
        }
        static Action Test() {
            return () => {
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine(i + 1);
                }
            };


            //Action action = null;
            //for (int i = 0; i < 10; i++) {
            //    int temp = i +1;
            //    action += () => {
            //        Console.WriteLine(temp);
            //    };
            //}
            //return action;
        }
    }
}
