using System;

namespace _01ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            int num = 5;
            num.SpeakValue();
            string str = "000";
            str.SpeakValue("kk", "tt");
        }
    }
    static class Tools {
        public static void SpeakValue(this int val) {
            Console.WriteLine("扩展方法：" + val);
        }
        public static void SpeakValue(this string str,string s1,string s2) {
            Console.WriteLine("string扩展方法");
            Console.WriteLine("调用方法的对象：" + str);
            Console.WriteLine("参数1：" + s1);
            Console.WriteLine("参数1：" + s2);
            
        }
    }
}
