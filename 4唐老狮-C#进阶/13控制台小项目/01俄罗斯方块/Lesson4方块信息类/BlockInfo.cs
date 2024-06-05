using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    internal class BlockInfo {
        //方块信息坐标的容器
        private List<Postion[]> lists;
        public BlockInfo(E_Draw_Type type) {
            lists = new List<Postion[]>();
            switch (type) {
                case E_Draw_Type.Cube:
                    lists.Add(new Postion[3] {
                        new Postion(2,0),
                        new Postion(0,1),
                        new Postion(2,1),
                    });
                    break;
                case E_Draw_Type.Line:
                    //初始化长条形状的四种形态的信息
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(0,1),
                        new Postion(0,2),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-4,0),
                        new Postion(-2,0),
                        new Postion(2,0),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-2),
                        new Postion(0,-1),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,0),
                        new Postion(2,0),
                        new Postion(4,0),
                    });
                    break;
                case E_Draw_Type.Tank:
                    lists.Add(new Postion[3] {
                        new Postion(-2,0),
                        new Postion(2,0),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(-2,0),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(-2,0),
                        new Postion(2,0),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(2,0),
                        new Postion(0,1),
                    });
                    break;
                case E_Draw_Type.Left_Ladder:
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(2,0),
                        new Postion(2,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(2,0),
                        new Postion(-2,1),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,-1),
                        new Postion(-2,0),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(2,-1),
                        new Postion(-2,0),
                    });
                    break;
                case E_Draw_Type.Right_Ladder:
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(-2,0),
                        new Postion(-2,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,-1),
                        new Postion(0,-1),
                        new Postion(2,0),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(2,-1),
                        new Postion(2,0),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,0),
                        new Postion(0,1),
                        new Postion(2,1),
                    });
                    break;
                case E_Draw_Type.Left_Long_Ladder:
                    lists.Add(new Postion[3] {
                        new Postion(-2,-1),
                        new Postion(0,-1),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(2,1),
                        new Postion(-2,0),
                        new Postion(2,0),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(0,1),
                        new Postion(2,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,0),
                        new Postion(2,0),
                        new Postion(-2,1),
                    });
                    break;
                case E_Draw_Type.Right_Long_Ladder:
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(2,-1),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,0),
                        new Postion(2,0),
                        new Postion(2,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(0,-1),
                        new Postion(-2,1),
                        new Postion(0,1),
                    });
                    lists.Add(new Postion[3] {
                        new Postion(-2,-1),
                        new Postion(-2,0),
                        new Postion(2,0),
                    });
                    break;
            }
        }
        //提供外部快速获取位置偏移信息的。
        public Postion[] this[int index] {
            get {
                if (index < 0) {
                    return lists[0];
                } else if (index > lists.Count) {
                    return lists[lists.Count - 1];
                } else {
                    return lists[index];
                }
            }
        }
        /// <summary>
        /// 提供给外部，形态有几种
        /// </summary>
        public int Count {
            get=> lists.Count;
        }
    }
}
