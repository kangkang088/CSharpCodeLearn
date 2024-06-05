using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01俄罗斯方块 {
    internal class Map : IDraw {
        private GameScene nowGameScene;
        //固定墙壁
        private List<DrawObject> walls = new List<DrawObject>();
        //动态墙壁
        public List<DrawObject> dynamicWalls = new List<DrawObject>();
        #region Lesson6 为了外界能快速得到地图边界
        //动态墙壁的宽容量，一行最多多少个
        public int w;
        public int h;
        #endregion

        #region Lesson10 垮层
        //记录每一行有多少个小方块的容器
        //索引对应的就算行号
        private int[] recordInfo;
        #endregion
        public Map(GameScene gameScene) {
            this.nowGameScene = gameScene;
            h = Game.h - 6;
            //这个就代表对应每行的计数初始化，默认都为0
            //0-Game.h-7
            recordInfo = new int[h];
            w = 0;
            //绘制横向固定墙壁
            for (int i = 0; i < Game.w; i += 2) {
                walls.Add(new DrawObject(E_Draw_Type.Wall, i, h));
                ++w;
            }
            w -= 2;
            //纵向
            for (int i = 0; i < h; i++) {
                walls.Add(new DrawObject(E_Draw_Type.Wall, 0, i));
                walls.Add(new DrawObject(E_Draw_Type.Wall, Game.w - 2, i));
            }
        }

        public void Draw() {
            //绘制固定墙壁
            for (int i = 0; i < walls.Count; i++) {
                walls[i].Draw();
            }
            //绘制动态墙壁
            for (int i = 0; i < dynamicWalls.Count; i++) {
                dynamicWalls[i].Draw();
            }
        }
        /// <summary>
        /// 清除动态墙壁
        /// </summary>
        public void CleanDraw() {
            for (int i = 0; i < dynamicWalls.Count; i++) {
                dynamicWalls[i].Clean();
            }
        }
        public void AddWalls(List<DrawObject> walls) {
            for (int i = 0; i < walls.Count; i++) {
                //传递方块进来时，把类型改为墙壁类型
                walls[i].ChangeType(E_Draw_Type.Wall);
                dynamicWalls.Add(walls[i]);

                //只要在动态墙壁添加出，发现位置顶满了，就结束
                if (walls[i].pos.y <= 0) {
                    //关闭输入线程
                    nowGameScene.StopThread();
                    //切换到结束界面
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }

                //进行添加动态墙壁的计数
                //根据索引得到行
                recordInfo[h - 1 - walls[i].pos.y] += 1;
            }
            //先把之前的动态小方块擦掉，检测移除后再绘制动态小方块
            CleanDraw();
            //检测移除
            CheckClear();
            //再绘制
            Draw(); 
        }
        /// <summary>
        /// 检测是否垮层
        /// </summary>
        public void CheckClear() {
            //待移除方块列表
            List<DrawObject> delList = new List<DrawObject>();
            //要选择记录行中有多少个方块的容器  数组
            //判断这一行是否满了
            //遍历数组 检测数组里面存的数 是不是w-2
            for (int i = 0; i < recordInfo.Length; i++) {
                //必须满足条件才说明满了， 小方块计数 == w(已经去掉左右两边的了)
                if (recordInfo[i] == w) {
                    //1.这一行的所有小方块移除
                    for (int j = 0; j < dynamicWalls.Count; j++) {
                        //当前通过动态方块的y计算它在哪一行，如果行号和当前记录索引一致，就应该移除
                        if (i == h - 1 - dynamicWalls[j].pos.y) {
                            //移除方块
                            //为了安全移除，添加一个记录列表
                            delList.Add(dynamicWalls[j]);
                        }
                        //2.这一行之上的所有小方块下移一个单位
                        //如果当前位置是该行以上，那就该小方块下移一格
                        else if (h - 1 - dynamicWalls[j].pos.y > i) {
                            dynamicWalls[j].pos.y++;
                        }
                    }
                    //移除待删除的小方块
                    for (int j = 0; j < delList.Count; j++) {
                        dynamicWalls.Remove(delList[j]);
                    }
                    //3.记录小方块数量的数组从上到下前移
                    for(int j = i;j < recordInfo.Length - 1; j++) {
                        recordInfo[j] = recordInfo[j + 1];
                    }
                    //置空最顶的计数
                    recordInfo[recordInfo.Length - 1] = 0;
                    //垮掉一行后再次去从头检测是否垮层--(解决同时垮两层及以上的问题：从第0层开始，第0层满计数消除后计数下移，第一层满计数移到第0层，第二层非满计数移到第一层，然而第二次循环从第一层开始检测了。)
                    CheckClear();
                    break;
                }
            }
        }
    }
}
