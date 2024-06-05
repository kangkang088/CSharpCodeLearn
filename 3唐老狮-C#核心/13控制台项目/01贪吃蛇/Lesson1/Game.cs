using _01贪吃蛇.Lesson2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson1 {
    enum E_SceneType {
        Begin,
        Game,
        End,
    }
    internal class Game {
        //游戏窗口宽高
        public const int w = 80;
        public const int h = 20;
        //当前选中场景
        public static ISceneUpdate nowScene;
        public Game() {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            ChangeScene(E_SceneType.Begin);
        }
        //游戏开始
        public void Start() {
            while (true) {
                if (nowScene != null) {
                    nowScene.Update();
                }
            }
        }
        public static void ChangeScene(E_SceneType type) {
            //切换场景前擦除上一个场景
            Console.Clear();
            switch (type) {
                case E_SceneType.Begin:
                    nowScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene();
                    break;
                default:
                    break;
            }
        }
    }
}
