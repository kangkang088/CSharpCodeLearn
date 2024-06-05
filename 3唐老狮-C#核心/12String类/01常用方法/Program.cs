using System;

namespace _01常用方法 {
    internal class Program {
        static void Main(string[] args) {
            string str = "唐老狮";

            Console.WriteLine(str[0]);
            char[] cs =  str.ToCharArray();
            Console.WriteLine(cs[0]);

            str = string.Format("{0}{1}", 1, 333);
            Console.WriteLine(str);
        }
    }
}
