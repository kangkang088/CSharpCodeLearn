using _01贪吃蛇.Lesson3;
using _01贪吃蛇.Lesson4;
using _01贪吃蛇.Lesson5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01贪吃蛇.Lesson6 {
    enum E_MoveDir {
        Up,
        Down,
        Left,
        Right
    }
    internal class Snake : IDraw {
        SnakeBody[] snakeBodies;
        //记录当前蛇的长度
        int nowNum = 0;

        E_MoveDir dir;

        public Snake(int x, int y) {
            snakeBodies = new SnakeBody[200];
            snakeBodies[0] = new SnakeBody(E_SnakeBody_Type.Head, x, y);
            nowNum = 1;
            dir = E_MoveDir.Right;
        }

        public void Draw() {
            for (int i = 0; i < nowNum; i++) {
                snakeBodies[(int)i].Draw();
            }
        }
        #region Lesson7 蛇的移动
        public void Move() {
            //移动前擦除最后一个位置
            SnakeBody lastBody = snakeBodies[nowNum - 1];
            Console.SetCursorPosition(lastBody.pos.x,lastBody.pos.y);
            Console.Write("  ");

            //蛇头移动之前，从蛇尾开始，往前顶
            for (int i = nowNum - 1; i > 0; i--) {
                snakeBodies[i].pos = snakeBodies[i - 1].pos;
            }

            switch (dir) {
                case E_MoveDir.Up:
                    --snakeBodies[0].pos.y;
                    break;
                case E_MoveDir.Down:
                    ++snakeBodies[0].pos.y;
                    break;
                case E_MoveDir.Left:
                    snakeBodies[0].pos.x -= 2;
                    break;
                case E_MoveDir.Right:
                    snakeBodies[0].pos.x += 2;
                    break;
            }
        }
        #endregion

        #region Lesson8 蛇的转向
        public void ChangeDir(E_MoveDir dir) {
            //only have head,can turn left to right and turn right to left
            //or turn up to down and turn down to up
            if (dir == this.dir || nowNum > 1 && 
                (this.dir == E_MoveDir.Left && dir == E_MoveDir.Right ||
                this.dir == E_MoveDir.Right && dir == E_MoveDir.Left ||
                this.dir == E_MoveDir.Up && dir == E_MoveDir.Down ||
                this.dir == E_MoveDir.Down && dir == E_MoveDir.Up))
                return;
            this.dir = dir;
        }
        #endregion

        #region Lesson9 检测蛇撞墙或撞身体结束游戏
        public bool CheckEnd(Map map) {
            for (int i = 0; i < map.walls.Length; ++i) {
                if (snakeBodies[0].pos == map.walls[i].pos) {
                    return true;
                }
            }
            for (int i = 1; i < nowNum; i++) {
                if (snakeBodies[0].pos == snakeBodies[i].pos) {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Lesson10 吃食物相关
        //通过传入的位置判断该位置和蛇是否重合
        public bool CheckSamePos(Position p) {
            for (int i = 0; i < nowNum; i++) {
                if (snakeBodies[i].pos == p) {
                    return true;
                }
            }
            return false;
        }
        public void CheckEatFood(Food food) {
            if (snakeBodies[0].pos == food.pos) {
                //吃到了
                food.RandomPos(this);
                //长身体
                AddBody();
            }
            
        }
        #endregion

        #region Lesson11 长身体
        private void AddBody() {
            SnakeBody frontBody = snakeBodies[nowNum - 1];
            snakeBodies[nowNum] = new SnakeBody(E_SnakeBody_Type.Body, frontBody.pos.x, frontBody.pos.y);
            nowNum++;
        }
        #endregion
    }
}
