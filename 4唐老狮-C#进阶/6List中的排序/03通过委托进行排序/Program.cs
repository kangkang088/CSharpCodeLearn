namespace _03通过委托进行排序 {
    class ShopItem {
        public int id;
        public ShopItem(int id) {
            this.id = id;
        }
    }
    internal class Program {
        static void Main(string[] args) {
            List<ShopItem> list = new List<ShopItem>();
            list.Add(new ShopItem(268));
            list.Add(new ShopItem(215));
            list.Add(new ShopItem(884));
            list.Add(new ShopItem(4646));
            list.Add(new ShopItem(6481));
            list.Add(new ShopItem(4815));
            list.Add(new ShopItem(8698));
            list.Sort(SortShopItem);
            foreach (ShopItem item in list) {
                Console.WriteLine(item.id);
            }
        }
        static int SortShopItem(ShopItem a, ShopItem b) {
            if (a.id > b.id) {
                return -1;
            } else
                return 1;
        }
    }
}
