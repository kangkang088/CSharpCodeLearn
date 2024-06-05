using System;
using System.Collections.Generic;
using System.Text;

namespace _01俄罗斯方块 {
    class GameScene : ISceneUpdate {
        Map map;
        BlockWorker worker;
        //Thread inputThread;
        //bool isRunning;
        public GameScene() {
            map = new Map(this);
            worker = new BlockWorker();
            InputThread.Instance.inputEvent += CheckInputThread;
            //isRunning = true;
            //inputThread = new Thread(CheckInputThread);
            //inputThread.IsBackground = true;

            //inputThread.Start();
        }
        #region Lesson9 输入线程
        /// <summary>
        /// 解决休眠200ms的影响-注意跨线程访问的内存争夺问题
        /// </summary>
        private void CheckInputThread() {
            //while (isRunning) {
                if (Console.KeyAvailable) {
                    //为了避免影响主线程，在输入后加锁
                    lock (worker) {
                        switch (Console.ReadKey(true).Key) {
                            case ConsoleKey.LeftArrow:
                                if (worker.CanChange(E_Change_Type.Left, map))
                                    worker.Change(E_Change_Type.Left);
                                break;
                            case ConsoleKey.RightArrow:
                                if (worker.CanChange(E_Change_Type.Right, map))
                                    worker.Change(E_Change_Type.Right);
                                break;
                            case ConsoleKey.A:
                                if (worker.CanMoveRL(E_Change_Type.Left, map))
                                    worker.MoveRL(E_Change_Type.Left);
                                break;
                            case ConsoleKey.D:
                                if (worker.CanMoveRL(E_Change_Type.Right, map))
                                    worker.MoveRL(E_Change_Type.Right);
                                break;
                            case ConsoleKey.S:
                                if (worker.CanMove(map)) {
                                    worker.AutoMove();
                                }
                                break;

                        }
                    }
                }
            //}
        }
        /// <summary>
        /// 结束线程
        /// </summary>
        public void StopThread() {
            //isRunning = false;
            //inputThread = null;
            InputThread.Instance.inputEvent -= CheckInputThread;
        }
        #endregion
        public void Update() {
            //注意锁里面不要包含休眠，会影响别人
            lock (worker) {
                map.Draw();
                worker.Draw();
                if (worker.CanMove(map)) {
                    worker.AutoMove();
                }
            }
            Thread.Sleep(200);
        }
    }
}
