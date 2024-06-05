using System;

namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = false;
            int x = 0;
            int y = 0;           
            while (true) {
                //Console.Clear();
                Console.SetCursorPosition(x, y);
                Console.Write("■");
                char s = Console.ReadKey(true).KeyChar;
                Console.SetCursorPosition(x, y);
                Console.Write("  ");
                switch (s) {
                    case 'W':
                    case 'w':
                        if (y > 0) {
                            y -= 1;                        
                        }
                        break;
                    case 'A':
                    case 'a':
                            x -= 2;
                        if (x < 0) {
                            x = 0;
                        }
                        break;
                    case 'S':
                    case 's':                        
                            y += 1;
                        if (y > Console.BufferHeight - 1) {
                            y = Console.BufferHeight - 1;
                        }
                        break;
                    case 'D':
                    case 'd':
                            x += 2;
                        if (x > Console.BufferWidth - 2) {
                            x = Console.BufferWidth - 2;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
