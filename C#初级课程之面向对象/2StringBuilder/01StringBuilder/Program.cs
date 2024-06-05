using System;
using System.Diagnostics;
using System.Text;

namespace _01StringBuilder {
    internal class Program {
        static void Main(string[] args) {
            StringBuilder sb = new StringBuilder();
            sb.Append("15151");
            Console.WriteLine(sb.ToString());
            sb.Clear();
            Console.WriteLine(sb.ToString());

            string s1 = "";
            StringBuilder sb2 = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 90000; i++) {
                s1 += i;
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 90000; i++) {
                sb.Append(i);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

        }
    }
}
