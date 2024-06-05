using System;
using System.Collections;

namespace Lesson1_练习题
{
    #region 练习题一
    //请简述ArrayList和数组的区别
    #region 答案
    //ArrayList本质上是一个object数组的封装

    //1.ArrayList可以不用一开始就定长，单独使用数组是定长的
    //2.数组可以指定存储类型，ArrayList默认为object类型
    //3.数组的增删需要我们自己去实现，ArrayList帮我们封装了方便的API来使用
    //4.ArrayList使用时可能存在装箱拆箱，数组使用时只要不是object数组那就不存在这个问题
    //5.数组长度为Length, ArrayList长度为Count
    #endregion
    #endregion

    #region 练习题二
    //创建一个背包管理类，使用ArrayList存储物品，
    //实现购买物品，卖出物品，显示物品的功能。购买与卖出物品会导致金钱变化

    class BagMgr
    {
        //背包中的物品
        private ArrayList items;

        private int money;

        public BagMgr( int money )
        {
            this.money = money;
            items = new ArrayList();
        }

        public void BuyItem(Item item)
        {
            if( item.num <= 0 || item.money < 0 )
            {
                Console.WriteLine("请传入正确的物品信息");
                return;
            }

            if( this.money < item.money*item.num )
            {
                //买不起 钱不够
                Console.WriteLine("买不起 钱不够");
                return;
            }
            //减钱
            this.money -= item.money * item.num;
            Console.WriteLine("购买{0}{1}个花费{2}钱", item.name, item.num, item.money * item.num);
            Console.WriteLine("剩余{0}元钱", this.money);
            //如果想要叠加物品 可以在前面先判断 是否有这个物品 然后加数量
            for (int i = 0; i < items.Count; i++)
            {
                if( (items[i] as Item).id == item.id )
                {
                    //叠加数量
                    (items[i] as Item).num += item.num;
                    return;
                }
            }
            //把一组物品加到 list中
            items.Add(item);
        }

        public void SellItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                //如何判断 卖的东西我有没有
                //这是在判断 两个引用地址 指向的是不是同一个房间
                //所以我们要判断 卖的物品 一般不这样判断
                //if(items[i] == item)
                //{

                //}
                if( (items[i] as Item).id == item.id )
                {
                    //两种情况
                    int num = 0;
                    string name = (items[i] as Item).name;
                    int money = (items[i] as Item).money;
                    if ((items[i] as Item).num > item.num)
                    {
                        //1.比我身上的少
                        num = item.num;
                    }
                    else
                    {
                        //2.大于等于我身上的东西数量
                        num = (items[i] as Item).num;
                        //卖完了  就从身上移除了
                        items.RemoveAt(i);
                    }

                    int sellMoney = (int)(num * money * 0.8f);
                    this.money += sellMoney;

                    Console.WriteLine("卖了{0}{1}个，赚了{2}钱", name, num, sellMoney);
                    Console.WriteLine("目前拥有{0}元钱", this.money);

                    return;
                }
            }
        }

        public void SellItem(int id, int num = 1)
        {
            //我这是想 直接调用上面写好的方法 
            //我就直接构造一个Item类 把ID和数量两个关键信息设置了即可 就可以卖了
            Item item = new Item(id, num);
            SellItem(item);
        }

        public void ShowItem()
        {
            Item item;
            for (int i = 0; i < items.Count; i++)
            {
                item = items[i] as Item;
                Console.WriteLine("有{0}{1}个", item.name, item.num);
            }
            Console.WriteLine("当前拥有{0}元钱", this.money);
        }
    }

    class Item
    {
        //物品唯一ID 来区分物品的种类
        public int id;
        //物品多少钱(单价)
        public int money;
        //物品名字
        public string name;
        //public string tips;
        //物品数量
        public int num;

        public Item(int id, int num)
        {
            this.id = id;
            this.num = num;
        }

        public Item(int id, int money, string name, int num)
        {
            this.id = id;
            this.money = money;
            this.name = name;
            this.num = num;
        }
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ArrayList练习题");

            BagMgr bag = new BagMgr(99999);
            Item i1 = new Item(1, 10, "红药", 10);
            Item i2 = new Item(2, 20, "蓝药", 10);
            Item i3 = new Item(3, 999, "屠龙刀", 1);

            bag.BuyItem(i1);
            bag.BuyItem(i2);
            bag.BuyItem(i3);

            bag.SellItem(i3);
            bag.SellItem(1, 1);
            bag.SellItem(1, 1);
            bag.SellItem(2, 1);
            bag.SellItem(2, 1);
        }
    }
}
