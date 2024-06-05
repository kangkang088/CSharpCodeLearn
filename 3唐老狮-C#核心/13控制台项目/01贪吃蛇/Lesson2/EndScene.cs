using _01贪吃蛇.Lesson1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson2 {
    internal class EndScene : BeginOrEndBaseScene {
        public EndScene() {
            strTitle = "结束游戏";
            strOne = "回到开始界面";
        }

        public override void EnterJDoSomething() {
            //按J键的逻辑
            if (nowSelIndex == 0) {
                Game.ChangeScene(E_SceneType.Begin);
            } else {
                Environment.Exit(0);
            }
        }
    }
}
