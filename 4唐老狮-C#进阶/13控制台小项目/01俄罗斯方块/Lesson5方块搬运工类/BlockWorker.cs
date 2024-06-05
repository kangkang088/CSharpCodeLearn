using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    /// <summary>
    /// 变形枚举
    /// </summary>
    enum E_Change_Type {
        Left,
        Right,
    }
    internal class BlockWorker : IDraw {
        //方块们
        private List<DrawObject> blocks;
        //心中默认知道 各个形状的方块信息是什么
        private Dictionary<E_Draw_Type, BlockInfo> blockInfoDic;
        //记录创建出来的方块的具体形态信息
        private BlockInfo nowBlockInfo;
        //当前形态索引
        private int nowInfoIndex;
        public BlockWorker() {
            //初始化 砖块信息
            blockInfoDic = new Dictionary<E_Draw_Type, BlockInfo> {
                { E_Draw_Type.Cube,new BlockInfo(E_Draw_Type.Cube)},
                { E_Draw_Type.Line,new BlockInfo(E_Draw_Type.Line)},
                { E_Draw_Type.Tank,new BlockInfo(E_Draw_Type.Tank)},
                { E_Draw_Type.Left_Ladder,new BlockInfo(E_Draw_Type.Left_Ladder)},
                { E_Draw_Type.Right_Ladder,new BlockInfo(E_Draw_Type.Right_Ladder)},
                { E_Draw_Type.Left_Long_Ladder,new BlockInfo(E_Draw_Type.Left_Long_Ladder)},
                { E_Draw_Type.Right_Long_Ladder,new BlockInfo(E_Draw_Type.Right_Long_Ladder)},
            };
            RandomCreateBlock();
        }

        public void Draw() {
            for (int i = 0; i < blocks.Count; i++) {
                blocks[i].Draw();
            }
        }

        /// <summary>
        /// 随机创建方块
        /// </summary>
        public void RandomCreateBlock() {
            Random r = new Random();
            E_Draw_Type type = (E_Draw_Type)r.Next(1, 8);
            //一个砖块包括四个小方形
            blocks = new List<DrawObject>() {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
            };
            //初始化方块位置
            //原点位置 随机
            blocks[0].pos = new Postion(24, -5);
            //其他三个位置，由形态信息决定
            //取出方块的形态信息，进行具体的随机
            //取出的方块的具体形态信息存起来，用于之后的变形
            nowBlockInfo = blockInfoDic[type];
            //随机几种形态中的一种，来设置方块信息
            nowInfoIndex = r.Next(0, nowBlockInfo.Count);
            //取出其中一种形态的坐标信息
            Postion[] pos = nowBlockInfo[nowInfoIndex];
            for (int i = 0; i < pos.Length; i++) {
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
        }

        #region Lesson6 变形相关方法

        public void CleanDraw() {
            for (int i = 0; i < blocks.Count; i++) {
                blocks[i].Clean();
            }
        }
        public void Change(E_Change_Type type) {
            //变之前，擦除，变之后，绘制
            CleanDraw();

            switch (type) {
                case E_Change_Type.Left:
                    --nowInfoIndex;
                    if (nowInfoIndex < 0)
                        nowInfoIndex = nowBlockInfo.Count - 1;
                    break;
                case E_Change_Type.Right:
                    ++nowInfoIndex;
                    if (nowInfoIndex >= nowBlockInfo.Count)
                        nowInfoIndex = 0;
                    break;
            }
            //得到索引的目的，是得到对应形态的 位置信息
            //用于设置另外三个小方块
            Postion[] pos = nowBlockInfo[nowInfoIndex];
            for (int i = 0; i < pos.Length; i++) {
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
            //变完之后，draw
            Draw();
        }
        /// <summary>
        /// 是否可以进行变形
        /// </summary>
        /// <param name="type">变形方向</param>
        /// <param name="map">地图信息</param>
        /// <returns></returns>
        public bool CanChange(E_Change_Type type, Map map) {
            //临时变量记录当前索引，不变化当前索引，变化临时索引
            int nowIndex = nowInfoIndex;
            switch (type) {
                case E_Change_Type.Left:
                    --nowIndex;
                    if (nowIndex < 0)
                        nowIndex = nowBlockInfo.Count - 1;
                    break;
                case E_Change_Type.Right:
                    ++nowIndex;
                    if (nowIndex >= nowBlockInfo.Count)
                        nowIndex = 0;
                    break;
            }
            Postion[] pos = nowBlockInfo[nowIndex];
            //是否超出地图边界
            Postion tempPos;
            for (int i = 0; i < pos.Length; i++) {
                tempPos = blocks[0].pos + pos[i];
                if (tempPos.x < 2 || tempPos.x >= Game.w - 2 || tempPos.y >= map.h)
                    return false;
            }
            //是否和地图上的动态方块重合
            for (int i = 0; i < pos.Length; i++) {
                tempPos = blocks[0].pos + pos[i];
                for (int j = 0; j < map.dynamicWalls.Count; j++) {
                    if (tempPos == map.dynamicWalls[j].pos) {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Lesson7 方块左右移动
        /// <summary>
        /// 左右移动
        /// </summary>
        /// <param name="type"></param>
        public void MoveRL(E_Change_Type type) {
            //动之前，擦，动之后，画
            CleanDraw();

            //根据传入的类型，左动还是右动
            //左动x-2，y0，右动x+2，y0。
            Postion movePos = new Postion(type == E_Change_Type.Left ? -2 : 2, 0);
            //遍历所有小方块并移动
            for (int i = 0;i < blocks.Count;i++) {
                blocks[i].pos += movePos;
            }
            Draw();
        }
        /// <summary>
        /// 判断是否可以进行左右移动
        /// </summary>
        public bool CanMoveRL(E_Change_Type type,Map map) {
            //根据传入的类型，左动还是右动
            //左动x-2，y0，右动x+2，y0。
            Postion movePos = new Postion(type == E_Change_Type.Left ? -2 : 2, 0);
            //遍历所有小方块并移动
            //动过后的结果，不能直接改小方块的位置
            Postion pos;
            for (int i = 0; i < blocks.Count; i++) {
                pos = blocks[i].pos + movePos;
                if (pos.x < 2 || pos.x >= Game.w - 2) {
                    return false;
                }
            }
            //动态方块重合
            for (int i = 0; i < blocks.Count; i++) {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++) {
                    if (pos == map.dynamicWalls[j].pos) {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Lesson8 方块自动向下移动
        /// <summary>
        /// 自动移动方法
        /// </summary>
        public void AutoMove() {
            //变位置前擦除，变后画
            CleanDraw();
            //首先得到向下移动多少
            Postion downMove = new Postion(0,1);
            for (int i = 0;i < blocks.Count; ++i) {
                blocks[i].pos += downMove;
            }
            Draw();
        }
        /// <summary>
        /// 方块是否可以继续自动下移
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool CanMove(Map map) {
            //临时变量存储下一次移动的位置，用于进行重合判断
            Postion movePos = new Postion(0,1);
            Postion pos;
            //边界
            for (int i = 0;i < blocks.Count;i++) {
                pos = blocks[i].pos + movePos;
                if (pos.y >= map.h) {
                    //停下，给与地图
                    map.AddWalls(blocks);
                    //随机创建新的方块
                    RandomCreateBlock();
                    return false;
                }
            }
            //动态方块
            for (int i = 0; i < blocks.Count; i++) {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++) {
                    if (pos == map.dynamicWalls[j].pos) {
                        //停下，给与地图
                        map.AddWalls(blocks);
                        //随机创建新的方块
                        RandomCreateBlock();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
