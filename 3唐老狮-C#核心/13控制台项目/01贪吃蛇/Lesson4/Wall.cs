using _01贪吃蛇.Lesson3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson4 {
    internal class Wall : GameObject {
        public Wall(int x,int y) {
            pos = new Position(x,y);
        }

        public override void Draw() {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
        }
    }
}
