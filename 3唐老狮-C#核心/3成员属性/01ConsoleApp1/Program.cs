using System;
using System.Diagnostics;

namespace _01ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {

        }
    }
    class Person {
        private int age;
        private string name;
        private bool sex;

        public int Age {
            get {
                return age;
            }
            set {
                age = value;
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public int Money {
            get;
            set;
        }
    }
}
