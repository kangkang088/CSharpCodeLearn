using _01贪吃蛇.Lesson1;
using _01贪吃蛇.Lesson4;
using _01贪吃蛇.Lesson5;
using _01贪吃蛇.Lesson6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson2 {
    internal class GameScene : ISceneUpdate {
        //sparrow penguin swan helicopter ostrich parrot warrior hunter master submachinegun shotgun pistol dagger weapon
        // wood snake  punch the clock  rectangle  circular square male female  occupation
        Map map;
        Snake snake;
        Food food;
        int updateIndex = 0;
        public GameScene() {
            map = new Map();
            snake = new Snake(40, 10);
            food = new Food(snake);
        }

        public void Update() {
            //移动速度控制
            if (updateIndex % 6666 == 0) {
                map.Draw();
                food.Draw();
                snake.Move();
                snake.Draw();
                if (snake.CheckEnd(map)) {
                    //结束逻辑
                    Game.ChangeScene(E_SceneType.End);
                }
                snake.CheckEatFood(food);
                updateIndex = 0;
            }
            updateIndex++;
            if (Console.KeyAvailable) {
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_MoveDir.Up);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_MoveDir.Left);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_MoveDir.Down);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_MoveDir.Right);
                        break;
                }
            }
            
        }
    }
}
