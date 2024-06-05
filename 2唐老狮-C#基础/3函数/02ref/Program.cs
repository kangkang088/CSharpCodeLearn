using System;
using System.Security.Authentication;

namespace _02ref {
    internal class Program {
        static void Main(string[] args) {
            int[] a = { 5 };
            ExchangeValue(ref a);
            Console.WriteLine(a[0]);
        }
        static void ExchangeValue(ref int[] a) {
            a = new int[1] { 6};
        }
    }
}
