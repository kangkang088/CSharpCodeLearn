using System;
using System.Text;

namespace _02StringBuilder {
    internal class Program {
        static void Main(string[] args) {
            StringBuilder sb = new StringBuilder("12312312312312311111",100);
            Console.WriteLine(sb);
            Console.WriteLine(sb.Capacity);
            Console.WriteLine(sb.Length);
            
        }
    }
}
