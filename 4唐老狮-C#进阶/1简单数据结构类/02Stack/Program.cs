using System.Collections;

namespace _02Stack {
    internal class Program {
        static void Main(string[] args) {
            Stack s = new Stack();
            s.Push(1);
            s.Push(2);
            s.Push(3);
            var o =  s.Pop();
            Console.WriteLine(o);
            int a = 10;
            Stack stack = new Stack();
            while (a > 0) {
                stack.Push(a % 2);
                a = a / 2;
            }
            while (stack.Count > 0) {
                var num = stack.Pop();
                Console.Write(num);
            }
        }
    }
}
