using System;
using System.Threading;

namespace _01const {
    class Program {
        static void Main(string[] args){
            // const int i = 50;
            // //i = 25;
            // Console.WriteLine(i);
            // char a = '\a';
            // Console.WriteLine(a);
            // const string name = "wudi";
            // Console.WriteLine($"my name is {name}");
            // int aa = 10;
            // double bb = aa / 3f;
            // Console.WriteLine(bb);
            // Console.WriteLine(aa++);
            //
            // int m = 50;
            // int n = 40;
            // m = m * n;
            // n = m / n;
            // m = m / n;
            // Console.WriteLine(m);
            // Console.WriteLine(n);
            //
            // bool x = !(5 + 3 > 2);
            // char o = '0';
            // Console.WriteLine(Convert.ToInt32("12"));

            // for (int i = 0; i < 10; i++){
            //     for (int j = 0; j < i; j++){
            //         Console.Write("*");
            //     }
            //     Console.WriteLine();
            // }

            for (int i = 1; i < 11; i++){
                int temp = i;
                while (temp <=10){
                    Console.Write(' ');
                    temp++;
                }
                for (int j = 1; j <= 2*i-1; j++){
                    Console.Write("*");
                }
                Console.WriteLine();
            }

        }
    }
}