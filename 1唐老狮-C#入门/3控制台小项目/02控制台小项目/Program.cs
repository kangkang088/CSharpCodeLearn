using System;

namespace _02控制台小项目 {
    class Program {
        static void Main(string[] args) {
            #region 控制台基础设置
            Console.CursorVisible = false;

            int w = 50;
            int h = 30;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            #endregion

            #region 多个场景
            int nowSceneId = 1;
            string gameOverInfo = "";
            while (true) {
                switch (nowSceneId) {
                    //begin
                    case 1: {
                            Console.Clear();
                            Console.SetCursorPosition(w / 2 - 5, 8);
                            Console.Write("勇士救公主");
                            int nowSelIndex = 0;
                            while (true) {
                                bool isQuitWhile = false;
                                Console.SetCursorPosition(w / 2 - 4, 13);
                                Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                                Console.Write("开始游戏");
                                Console.SetCursorPosition(w / 2 - 4, 15);
                                Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                                Console.Write("结束游戏");
                                char c = Console.ReadKey(true).KeyChar;
                                switch (c) {
                                    case 'W':
                                    case 'w':
                                        nowSelIndex--;
                                        if (nowSelIndex < 0) {
                                            nowSelIndex = 0;
                                        }
                                        break;
                                    case 'S':
                                    case 's':
                                        nowSelIndex++;
                                        if (nowSelIndex > 1) {
                                            nowSelIndex = 1;
                                        }
                                        break;
                                    case 'J':
                                    case 'j':
                                        if (nowSelIndex == 0) {
                                            nowSceneId = 2;
                                            isQuitWhile = true;
                                        } else {
                                            Environment.Exit(0);
                                        }
                                        break;
                                }
                                if (isQuitWhile)
                                    break;
                            }
                            break;
                        }
                    //game
                    case 2:
                        Console.Clear();
                        #region 不变的红墙
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 0; i < w; i += 2) {
                            //第一行
                            Console.SetCursorPosition(i, 0);
                            Console.Write("■");
                            //最后一行
                            Console.SetCursorPosition(i, h - 1);
                            Console.Write("■");
                            //中间行
                            Console.SetCursorPosition(i, h - 6);
                            Console.Write("■");
                        }
                        for (int i = 0; i < h; i++) {
                            //第一列
                            Console.SetCursorPosition(0, i);
                            Console.Write("■");
                            //最后一列
                            Console.SetCursorPosition(w - 2, i);
                            Console.Write("■");
                        }
                        #endregion
                        #region BOSS相关
                        int bossX = 24;
                        int bossY = 15;
                        int bossAtkMin = 7;
                        int bossAtkMax = 13;
                        int bossHp = 100;
                        string bossIcon = "■";
                        ConsoleColor bossColor = ConsoleColor.Green;
                        #endregion
                        #region 玩家相关
                        int playerX = 4;
                        int playerY = 5;
                        int playerAtkMin = 8;
                        int playerAtkMax = 12;
                        int playerHp = 100;
                        string playerIcon = "●";
                        char playerInput;
                        ConsoleColor playerColor = ConsoleColor.Yellow;
                        #endregion
                        #region 公主相关
                        int princessX = 24;
                        int princessY = 5;
                        string princessIcon = "★";
                        ConsoleColor princessColor = ConsoleColor.Blue;
                        #endregion

                        #region 玩家的战斗状态
                        bool isFight = false;
                        bool isOver = false;
                        #endregion
                        while (true) {

                            #region Boss相关
                            if (bossHp > 0) {
                                Console.SetCursorPosition(bossX, bossY);
                                Console.ForegroundColor = bossColor;
                                Console.Write(bossIcon);
                            }
                            #endregion
                            #region 公主显示相关
                            else {
                                Console.SetCursorPosition(princessX, princessY);
                                Console.ForegroundColor = princessColor;
                                Console.Write(princessIcon);
                            }
                            #endregion
                            #region Player相关
                            Console.SetCursorPosition(playerX, playerY);
                            Console.ForegroundColor = playerColor;
                            Console.Write(playerIcon);
                            playerInput = Console.ReadKey(true).KeyChar;
                            if (!isFight) {
                                Console.SetCursorPosition(playerX, playerY);
                                Console.Write("  ");
                                switch (playerInput) {
                                    case 'W':
                                    case 'w':
                                        --playerY;
                                        if (playerY < 1) {
                                            playerY = 1;
                                        } else if (playerX == bossX && playerY == bossY && bossHp > 0) {
                                            ++playerY;
                                        } else if (playerX == princessX && playerY == princessY && bossHp < 0) {
                                            ++playerY;
                                        }
                                        break;
                                    case 'A':
                                    case 'a':
                                        playerX -= 2;
                                        if (playerX < 2) {
                                            playerX = 2;
                                        } else if (playerX == bossX && playerY == bossY && bossHp > 0) {
                                            playerX += 2;
                                        } else if (playerX == princessX && playerY == princessY && bossHp < 0) {
                                            playerX += 2;
                                        }
                                        break;
                                    case 'S':
                                    case 's':
                                        ++playerY;
                                        if (playerY > h - 7) {
                                            playerY = h - 7;
                                        } else if (playerX == bossX && playerY == bossY && bossHp > 0) {
                                            --playerY;
                                        } else if (playerX == princessX && playerY == princessY && bossHp < 0) {
                                            --playerY;
                                        }
                                        break;
                                    case 'D':
                                    case 'd':
                                        playerX += 2;
                                        if (playerX > w - 4) {
                                            playerX = w - 4;
                                        } else if (playerX == bossX && playerY == bossY && bossHp > 0) {
                                            playerX -= 2;
                                        } else if (playerX == princessX && playerY == princessY && bossHp < 0) {
                                            playerX -= 2;
                                        }
                                        break;
                                    case 'J':
                                    case 'j':
                                        if ((playerX == bossX && playerY == bossY - 1 ||
                                            playerX == bossX && playerY == bossY + 1 ||
                                            playerX == bossX - 2 && playerY == bossY ||
                                            playerX == bossX + 2 && playerY == bossY) &&
                                            bossHp > 0) {
                                            isFight = true;
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("开始战斗，按J键继续");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("当前玩家血量为{0}", playerHp);
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("当前BOSS血量为{0}", bossHp);
                                        } else if ((playerX == princessX && playerY == princessY - 1 ||
                                             playerX == princessX && playerY == princessY + 1 ||
                                             playerX == princessX - 2 && playerY == princessY ||
                                             playerX == princessX + 2 && playerY == princessY) &&
                                             bossHp < 0) {
                                            nowSceneId = 3;
                                            gameOverInfo = "游戏通关";
                                            isOver = true;
                                        }
                                        break;
                                }
                            } else {
                                if (playerInput == 'J' || playerInput == 'j') {
                                    if (playerHp <= 0) {
                                        nowSceneId = 3;
                                        break;
                                    } else if (bossHp <= 0) {
                                        Console.SetCursorPosition(bossX, bossY);
                                        Console.Write("  ");
                                        isFight = false;
                                    } else {
                                        Random r = new Random();
                                        int atk = r.Next(playerAtkMin, playerAtkMax);
                                        bossHp -= atk;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("                                              ");
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("你对BOSS造成了{0}点伤害，BOSS剩余血量为{1}", atk, bossHp);
                                        if (bossHp > 0) {
                                            atk = r.Next(bossAtkMin, bossAtkMax);
                                            playerHp -= atk;
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                              ");

                                            if (playerHp <= 0) {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("很遗憾，您失败了");
                                                gameOverInfo = "游戏失败";
                                            } else {
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("BOSS对你造成了{0}点伤害，你剩余血量为{1}", atk, playerHp);
                                            }
                                        } else {
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                              ");
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("恭喜你，你战胜了BOSS！");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("前往公主身边，按J键继续");
                                        }
                                    }

                                }
                            }
                            #endregion
                            if (isOver == true) {
                                break;
                            }
                        }
                        break;
                    //end
                    case 3:
                        Console.Clear();
                        #region 结束场景
                        Console.SetCursorPosition(w / 2 - 4, 5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("GameOver");
                        Console.SetCursorPosition(w / 2 - 4, 7);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(gameOverInfo);

                        int nowSelEndIndex = 0;
                        while (true) {
                            bool isQuitEndWhile = false;    
                            Console.SetCursorPosition(w / 2 - 6, 9);
                            Console.ForegroundColor = nowSelEndIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("回到开始界面");
                            Console.SetCursorPosition(w / 2 - 4, 10);
                            Console.ForegroundColor = nowSelEndIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("退出游戏");
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input) {
                                case 'W':
                                case 'w':
                                    --nowSelEndIndex;
                                    if (nowSelEndIndex < 0) {
                                        nowSelEndIndex = 0;
                                    }
                                    break;
                                case 'S':
                                case 's':
                                    ++nowSelEndIndex;
                                    if (nowSelEndIndex > 0) {
                                        nowSelEndIndex = 1;
                                    }
                                    break;
                                case 'J':
                                case 'j':
                                    if (nowSelEndIndex == 0) {
                                        nowSceneId = 1;
                                        isQuitEndWhile = true;
                                    } else {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (isQuitEndWhile)
                                break;
                        }
                        break;
                        #endregion
                }
            }
            #endregion
        }
    }
}
