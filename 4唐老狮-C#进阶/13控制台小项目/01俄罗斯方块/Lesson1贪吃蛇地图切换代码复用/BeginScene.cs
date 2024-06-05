using System;
using System.Collections.Generic;
using System.Text;

namespace _01俄罗斯方块
{
    class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene()
        {
            strTitle = "俄罗斯方块";
            strOne = "开始游戏";
        }

        public override void EnterJDoSomthing()
        {
            //按J键做什么的逻辑
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Game);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
