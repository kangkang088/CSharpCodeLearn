using System;
using System.Collections.Generic;
using System.Text;

namespace _01俄罗斯方块
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex = 0;
        protected string strTitle;
        protected string strOne;

        public abstract void EnterJDoSomthing();

        public void Update()
        {
            //开始和结束场景的 游戏逻辑 
            //选择当前的选项 然后 监听 键盘输入 wsj
            Console.ForegroundColor = ConsoleColor.White;
            //显示标题
            Console.SetCursorPosition(Game.w / 2 - strTitle.Length, 5);
            Console.Write(strTitle);
            //显示下方的选项
            Console.SetCursorPosition(Game.w / 2 - strOne.Length, 8);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(strOne);
            Console.SetCursorPosition(Game.w / 2 - 4, 10);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write("结束游戏");
            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --nowSelIndex;
                    if (nowSelIndex < 0)
                    {
                        nowSelIndex = 0;
                    }
                    break;
                case ConsoleKey.S:
                    ++nowSelIndex;
                    if (nowSelIndex > 1)
                    {
                        nowSelIndex = 1;
                    }
                    break;
                case ConsoleKey.J:
                    EnterJDoSomthing();
                    break;
            }
        }
    }
}
