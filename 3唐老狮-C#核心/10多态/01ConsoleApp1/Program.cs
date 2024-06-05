using System;

namespace _01ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            GameObject gameObject = new Player("wudi");
            gameObject.Atk();
            (gameObject as Player).Atk();   
        }
    }
    class GameObject {
        public string name;
        public GameObject(string name) {
            this.name = name;
        }
        public virtual void Atk() {
            Console.WriteLine("游戏对象进行攻击");
        }

    }
    class Player : GameObject {
        public Player(string name) : base(name) {
        }
        public override void Atk() {
            Console.WriteLine("玩家对象进行攻击");
        }
    }

}
