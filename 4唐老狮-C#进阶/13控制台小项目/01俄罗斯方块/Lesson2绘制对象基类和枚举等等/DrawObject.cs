using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    enum E_Draw_Type {
        /// <summary>
        /// 墙壁
        /// </summary>
        Wall,
        /// <summary>
        /// 方块
        /// </summary>
        Cube,
        /// <summary>
        /// 长条
        /// </summary>
        Line,
        /// <summary>
        /// 坦克
        /// </summary>
        Tank,
        /// <summary>
        /// 左拐子
        /// </summary>
        Left_Ladder,
        /// <summary>
        /// 右拐子
        /// </summary>
        Right_Ladder,
        /// <summary>
        /// 左长拐子
        /// </summary>
        Left_Long_Ladder,
        /// <summary>
        /// 右长拐子
        /// </summary>
        Right_Long_Ladder,
    }
    internal class DrawObject : IDraw {
        public Postion pos;
        public E_Draw_Type type;

        public DrawObject(E_Draw_Type type) {
            this.type = type;
        }
        public DrawObject(E_Draw_Type type, int x, int y) : this(type) {
            pos = new Postion(x, y);
        }

        public void Draw() {
            //屏幕外不需要绘制
            if (pos.y < 0)
                return;
            Console.SetCursorPosition(pos.x, pos.y);
            switch (type) {
                case E_Draw_Type.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_Draw_Type.Cube:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_Draw_Type.Line:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_Draw_Type.Tank:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_Draw_Type.Left_Ladder:
                case E_Draw_Type.Right_Ladder:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case E_Draw_Type.Left_Long_Ladder:
                case E_Draw_Type.Right_Long_Ladder:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.Write("■");
        }
        #region Lesson6 清除绘制的方法
        public void Clean() {
            //屏幕外不需要擦
            if (pos.y < 0)
                return;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("  ");
        }
        #endregion
        /// <summary>
        /// 切换方块类型，主要用于板砖下落到地图时 把板砖类型变成墙壁类型
        /// </summary>
        /// <param name="type"></param>
        public void ChangeType(E_Draw_Type type) {
            this.type = type;
        }
    }
}
