using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;

namespace _01ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            #region 1控制台初始化
            int w = 50;
            int h = 30;
            ConsoleInit(w, h);
            #endregion

            #region 2 场景选择
            E_SceneType e_SceneType = E_SceneType.Begin;
            while (true) {
                switch (e_SceneType) {
                    case E_SceneType.Begin:
                        Console.Clear();
                        BeginOrEndScene(w, h, ref e_SceneType);
                        break;
                    case E_SceneType.Game:
                        Console.Clear();
                        GameScene(w, h, ref e_SceneType);
                        break;
                    case E_SceneType.End:
                        Console.Clear();
                        BeginOrEndScene(w, h, ref e_SceneType);
                        break;
                }
            }
            #endregion
        }
        #region 1控制台初始化
        static void ConsoleInit(int w, int h) {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }
        #endregion

        #region 3 开始场景逻辑 + 10结束场景逻辑
        static void BeginOrEndScene(int w, int h, ref E_SceneType nowSceneType) {
            Console.SetCursorPosition(nowSceneType == E_SceneType.Begin ? w / 2 - 3 : w / 2 - 4, 8);
            Console.Write(nowSceneType == E_SceneType.Begin ? "飞行棋" : "游戏结束");
            int nowSelIndex = 0;
            bool isQuitBegin = false;
            while (true) {
                isQuitBegin = false;
                Console.SetCursorPosition(nowSceneType == E_SceneType.Begin ? w / 2 - 4 : w / 2 - 5, 13);
                Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write(nowSceneType == E_SceneType.Begin ? "开始游戏" : "回到主菜单");
                Console.SetCursorPosition(w / 2 - 4, 15);
                Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.W:
                        --nowSelIndex;
                        if (nowSelIndex < 0)
                            nowSelIndex = 0;
                        break;
                    case ConsoleKey.S:
                        ++nowSelIndex;
                        if (nowSelIndex > 1)
                            nowSelIndex = 1;
                        break;
                    case ConsoleKey.J:
                        if (nowSelIndex == 0) {
                            nowSceneType = nowSceneType == E_SceneType.Begin ? E_SceneType.Game : E_SceneType.Begin;
                            isQuitBegin = true;
                        } else {
                            Environment.Exit(0);
                        }
                        break;
                }
                if (isQuitBegin)
                    break;
            }
        }
        #endregion

        #region 游戏场景逻辑
        static void GameScene(int w, int h, ref E_SceneType nowSceneType) {
            DrawWall(w, h);
            Map map = new Map(14, 3, 80);
            map.Draw();
            Player player = new Player(0, E_Player_Type.Player);
            Player computer = new Player(0, E_Player_Type.Computer);
            DrawPlayer(player, computer, map);
            while (true) {
                if (PlayerRandomMove(w, h, ref player, ref computer, map, ref nowSceneType)) {
                    break;
                }
                if (PlayerRandomMove(w, h, ref computer, ref player, map, ref nowSceneType)) {
                    break;
                }
            }

        }
        static bool PlayerRandomMove(int w, int h, ref Player p, ref Player otherP, Map map, ref E_SceneType e_SceneType) {
            Console.ReadKey(true);
            bool isGameOver = RandomMove(w, h, ref p, ref otherP, map);
            map.Draw();
            DrawPlayer(p, otherP, map);
            if (isGameOver) {
                Console.ReadKey(true);
                e_SceneType = E_SceneType.End;
            }
            return isGameOver;
        }
        #endregion

        #region 4绘制不变的红墙
        static void DrawWall(int w, int h) {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < w; i += 2) {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 1);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 6);
                Console.Write("■");
                Console.SetCursorPosition(i, h - 11);
                Console.Write("■");
            }
            for (int i = 0; i < h; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(w - 2, i);
                Console.Write("■");
            }
            //文字信息
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 10);
            Console.Write("□：普通格子");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, h - 9);
            Console.Write("‖：暂停，一回合不动");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(26, h - 9);
            Console.Write("●：炸弹，倒退五格");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(2, h - 8);
            Console.Write("¤：时空隧道，随即倒退，暂停，换位置");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(2, h - 7);
            Console.Write("★：玩家");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(12, h - 7);
            Console.Write("▲：电脑");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(22, h - 7);
            Console.Write("◎：玩家和电脑重合");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 5);
            Console.Write("按任意键开始扔骰子");
        }
        #endregion

        #region 8 绘制玩家
        static void DrawPlayer(Player player, Player computer, Map map) {
            //重合
            if (player.nowIndex == computer.nowIndex) {
                Grid grid = map.grids[player.nowIndex];
                Console.SetCursorPosition(grid.pos.x, grid.pos.y);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("◎");
            } else {
                //不重合
                player.Draw(map);
                computer.Draw(map);
            }

        }
        #endregion

        #region 9 扔骰子函数
        static void CleanInfo(int h) {
            Console.SetCursorPosition(2, h - 5);
            Console.Write("                                  ");
            Console.SetCursorPosition(2, h - 4);
            Console.Write("                                  ");
            Console.SetCursorPosition(2, h - 3);
            Console.Write("                                  ");
            Console.SetCursorPosition(2, h - 2);
            Console.Write("                                  ");
        }
        static bool RandomMove(int w, int h, ref Player p, ref Player otherP, Map map) {
            CleanInfo(h);
            Console.ForegroundColor = p.type == E_Player_Type.Player ? ConsoleColor.Cyan : ConsoleColor.Magenta;
            if (p.isPaues) {
                Console.SetCursorPosition(2, h - 5);
                Console.Write("处于该暂停点，{0}需要暂停一回合", p.type == E_Player_Type.Player ? "你" : "电脑");
                Console.SetCursorPosition(2, h - 4);
                Console.Write("请按任意键，让{0}开始扔骰子", p.type == E_Player_Type.Player ? "电脑" : "你");
                p.isPaues = false;
                return false;
            }
            Random r = new Random();
            int randomNum = r.Next(1, 7);
            p.nowIndex += randomNum;
            Console.SetCursorPosition(2, h - 5);
            Console.Write("{0}扔出的点数为：{1}", p.type == E_Player_Type.Player ? "你" : "电脑", randomNum);
            if (p.nowIndex >= map.grids.Length - 1) {
                p.nowIndex = map.grids.Length - 1;
                Console.SetCursorPosition(2, h - 4);
                if (p.type == E_Player_Type.Player) {
                    Console.Write("恭喜你，你率先到达了终点");
                } else {
                    Console.Write("很遗憾，电脑先到达了终点");
                }
                Console.SetCursorPosition(2, h - 3);
                Console.Write("请按任意键结束游戏");
                return true;
            } else {
                Grid grid = map.grids[p.nowIndex];
                switch (grid.type) {
                    case E_Grid_Type.Normal:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}到达了一个安全位置", p.type == E_Player_Type.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔骰子", p.type == E_Player_Type.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.Boom:
                        p.nowIndex -= 5;
                        if (p.nowIndex < 0) {
                            p.nowIndex = 0;
                        }
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}猜到了炸弹，退后5格", p.type == E_Player_Type.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔骰子", p.type == E_Player_Type.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.Pause:
                        p.isPaues = true;
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}到达了暂停点，需要暂停一回合", p.type == E_Player_Type.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔骰子", p.type == E_Player_Type.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.Tunnel:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}踩到了时空隧道", p.type == E_Player_Type.Player ? "你" : "电脑");

                        randomNum = r.Next(1, 91);
                        if (randomNum <= 30) {
                            p.nowIndex -= 5;
                            if (p.nowIndex < 0)
                                p.nowIndex = 0;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发倒退五格");
                        } else if (p.nowIndex <= 60) {
                            p.isPaues = true;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发暂停一回合");
                        } else {
                            int temp = p.nowIndex;
                            p.nowIndex = otherP.nowIndex;
                            otherP.nowIndex = temp;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("双方交换位置");
                        }
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键，让{0}开始扔骰子", p.type == E_Player_Type.Player ? "电脑" : "你");
                        break;
                }
            }
            return false;
        }
        #endregion
    }
    #region 2场景选择相关
    enum E_SceneType {
        /// <summary>
        /// 开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End
    }
    #endregion

    #region 5 格子结构体和格子枚举
    enum E_Grid_Type {
        /// <summary>
        /// 普通格子
        /// </summary>
        Normal,
        /// <summary>
        /// 炸弹
        /// </summary>
        Boom,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 时空隧道
        /// </summary>
        Tunnel,
    }
    /// <summary>
    /// 位置信息结构体
    /// </summary>
    struct Vector2 {
        public int x;
        public int y;
        public Vector2(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    struct Grid {
        public E_Grid_Type type;
        public Vector2 pos;
        public Grid(int x, int y, E_Grid_Type type) {
            this.type = type;
            pos.x = x;
            pos.y = y;
        }
        public void Draw() {
            Console.SetCursorPosition(pos.x, pos.y);
            switch (type) {
                case E_Grid_Type.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("□");
                    break;
                case E_Grid_Type.Boom:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    break;
                case E_Grid_Type.Pause:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("‖");
                    break;
                case E_Grid_Type.Tunnel:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("¤");
                    break;
            }
        }
    }
    #endregion

    #region 6 地图结构体
    struct Map {
        public Grid[] grids;
        public Map(int x, int y, int num) {
            grids = new Grid[num];
            int indexX = 0;
            int indexY = 0;
            int stepNum = 2;
            Random r = new Random();
            int randnomNum;
            for (int i = 0; i < num; i++) {
                randnomNum = r.Next(0, 101);
                if (randnomNum < 85 || i == 0 || i == num - 1) {
                    grids[i].type = E_Grid_Type.Normal;
                } else if (randnomNum >= 85 && randnomNum < 90) {
                    grids[i].type = E_Grid_Type.Boom;
                } else if (randnomNum >= 90 && randnomNum < 95) {
                    grids[i].type = E_Grid_Type.Pause;
                } else {
                    grids[i].type = E_Grid_Type.Tunnel;
                }
                grids[i].pos = new Vector2(x, y);
                if (indexX == 10) {
                    y += 1;
                    ++indexY;
                    if (indexY == 2) {
                        indexX = 0;
                        indexY = 0;
                        stepNum = -stepNum;
                    }

                } else {
                    x += stepNum;
                    ++indexX;
                }
            }

        }
        public void Draw() {
            for (int i = 0; i < grids.Length; i++) {
                grids[i].Draw();
            }
        }
    }
    #endregion

    #region 7 玩家枚举和玩家结构体
    enum E_Player_Type {
        Player,
        Computer,
    }
    struct Player {
        public E_Player_Type type;
        public int nowIndex;
        public bool isPaues;
        public Player(int index, E_Player_Type type) {
            this.nowIndex = index;
            this.type = type;
            isPaues = false;
        }
        public void Draw(Map mapInfo) {
            Grid grid = mapInfo.grids[nowIndex];
            Console.SetCursorPosition(grid.pos.x, grid.pos.y);
            switch (type) {
                case E_Player_Type.Player:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("★");
                    break;
                case E_Player_Type.Computer:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("▲");
                    break;
            }
        }
    }
    #endregion
}
