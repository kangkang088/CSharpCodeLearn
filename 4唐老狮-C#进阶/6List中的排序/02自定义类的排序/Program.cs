namespace _02自定义类的排序 {
    class Item : IComparable<Item> {
        public int money;
        public Item(int money) {
            this.money = money;
        }

        public int CompareTo(Item other) {
            if (this.money > other.money) {
                return 1;
            } else {
                return -1;
            }
        }
    }
    internal class Program {
        static void Main(string[] args) {
            List<Item> list = new List<Item>();
            list.Add(new Item(15));
            list.Add(new Item(156));
            list.Add(new Item(962));
            list.Add(new Item(58));
            list.Add(new Item(957));
            list.Add(new Item(1820));
            list.Add(new Item(5645));
            list.Sort();
            foreach (Item item in list) {
                Console.WriteLine(item.money);
            }
        }
    }
}
