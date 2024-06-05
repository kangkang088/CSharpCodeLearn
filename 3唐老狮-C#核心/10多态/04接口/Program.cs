using System;


namespace _04接口 {
    internal class Program {
        static void Main(string[] args) {
        }
    }
    interface IFly {
        void Fly();
        string Name {
            get; set;
        }
        int this[int index] {
            get;set; 
        }
        event Action doSomething;
    }
    interface IAtk {
        void Atk();
    }
    internal class Personss:IAtk {
        public virtual void Atk() {
            Console.WriteLine();
        }
    }
    internal class Person1 : Personss {
        public sealed override void Atk() {
            Console.WriteLine();
        }
    }
}
