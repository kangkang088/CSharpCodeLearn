using System;

namespace _02继承中的构造函数 {
    internal class Program {
        static void Main(string[] args) {
            MainPlayer mainPlayer = new MainPlayer();
        }
    }
    class Father {
        //public Father() {
        //}

        public Father(int i) {
            Console.WriteLine("Father构造");
        }
    }
    class Son : Father {
        public Son(int i) : base(i) {

        }
    }
    class GameObject {
        public GameObject() {
            Console.WriteLine("GameObject的构造函数");
        }
    }
    class Player : GameObject {
        public Player() {
            Console.WriteLine("Player的构造函数");
        }
    }
    class MainPlayer : Player {
        public MainPlayer() {
            Console.WriteLine("MainPlayer的构造函数");
        }
    }

}
