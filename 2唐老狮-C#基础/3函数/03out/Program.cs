using Microsoft.VisualBasic;
using System;

namespace _03out {
    internal class Program {
        static void Main(string[] args) {
            //int a;
            string str1 = "asd";
            ExchangeValue(str1);
            Console.WriteLine(str1);
        }

        static void ExchangeValue(string a) {
            a = "12121";
        }
    }
}
