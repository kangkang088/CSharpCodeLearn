using _01贪吃蛇.Lesson1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson2 {
    internal class BeginScene : BeginOrEndBaseScene {
        public BeginScene() {
            strTitle = "贪吃蛇";
            strOne = "开始游戏";
        }

        public override void EnterJDoSomething() {
            //按J键的逻辑
            if (nowSelIndex == 0) {
                Game.ChangeScene(E_SceneType.Game);
            } else {
                Environment.Exit(0);
            }
        }
    }
}
