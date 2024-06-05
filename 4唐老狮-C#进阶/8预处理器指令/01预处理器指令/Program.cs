

#define Unity4

namespace _01预处理器指令 {
    internal class Program {
        static void Main(string[] args) {
#if Unity4
            Console.WriteLine("版本为unity2017！");
#warning 这个版本会报错
//#error 这个版本会报错
#endif
        }
    }
}
