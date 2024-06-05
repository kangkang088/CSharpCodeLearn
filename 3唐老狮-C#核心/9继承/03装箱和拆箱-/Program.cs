using System;

namespace _03装箱和拆箱_ {
    internal class Program {
        static void Main(string[] args) {
            char a = '0';
            object obj = a;
            //拆箱的类型必须和装箱的类型一样。
            char f = (char)obj;
        }
    }
}
