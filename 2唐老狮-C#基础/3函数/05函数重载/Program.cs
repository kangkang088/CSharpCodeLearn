using System;

namespace _05函数重载 {
    internal class Program {
        static void Main(string[] args) {
            
        }
        static void Say(string name) {
            Console.WriteLine(name);
        }
        static void Say(char name) {
            Console.WriteLine(name);
        }

    }
}
