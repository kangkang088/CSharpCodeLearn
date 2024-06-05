using System;
using System.Collections.Generic;
using System.Text;

namespace _01俄罗斯方块
{
    class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            strTitle = "结束游戏";
            strOne = "回到开始界面";
        }

        public override void EnterJDoSomthing()
        {
            //按J键做什么的逻辑
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Begin);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
