using System;
using System.Threading.Channels;

namespace _01Class {
    public class Apple {
        public string name;
        public float weight;
        public decimal price;
        public Apple() {

        }
        public Apple(string name, float weight) {
            this.name = name;
            this.weight = weight;
        }
        public Apple(string name, float weight, decimal price) : this(name, weight) {
            this.price = price;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            Apple apple = new Apple("kl", 15.6f, 15);
            test();
        }
        public static void test() {
            
        }
    }
}
