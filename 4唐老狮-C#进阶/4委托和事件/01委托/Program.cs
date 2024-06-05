
namespace _01委托 {
    delegate void MyFunc1();
    delegate int MyFunc2(int a);
    internal class Program {
        static void Main(string[] args) {
            MyFunc1 myFunc1 = new MyFunc1(Fun);
            myFunc1.Invoke();
        }
        static void Fun() {
            Console.WriteLine("Hello World!");
            
        }
    }
}
