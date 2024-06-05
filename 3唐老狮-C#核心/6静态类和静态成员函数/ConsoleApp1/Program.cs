using System;

namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            TestStatic.Test = 5;
            Console.WriteLine(TestStatic.Test);
            
        }
    }
    static class TestStatic {
        private static int testIndex = 0;

        public static int Test {
            get;
            set;
        }

        public static void TestFun() {

        }
    }
}
