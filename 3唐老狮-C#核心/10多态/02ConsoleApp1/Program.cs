using System;

namespace _02ConsoleApp1 {
    abstract class Fruits {
        public string name;

        protected Fruits(string name) {
            this.name = name;
        }

        public abstract void Bad();
    }
    class Apple : Fruits {
        public Apple():base("apple") {
        }

        public override void Bad() {
            Console.WriteLine(name);
        }
    }
    internal class Program {
        static void Main(string[] args) {  
            Fruits fruits = new Apple();
            fruits.Bad();
        }
    }

}
