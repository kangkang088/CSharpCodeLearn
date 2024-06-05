using _01贪吃蛇.Lesson1;
using _01贪吃蛇.Lesson3;
using _01贪吃蛇.Lesson6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson4 {
    internal class Food : GameObject {
        public Food(Snake snake) {
            RandomPos(snake);
        }

        public override void Draw() {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("¤");
        }

        public void RandomPos(Snake snake) {
            //得到蛇的位置，随机时排除
            Random r = new Random();
            int x = r.Next(2, Game.w / 2 - 1) * 2;
            int y = r.Next(1, Game.h - 4);
            pos = new Position(x, y);
            if (snake.CheckSamePos(pos)) {
                //重合
                RandomPos(snake);
            }
        }
    }
}
