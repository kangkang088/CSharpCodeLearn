namespace _01自带的排序 {
    internal class Program {
        static void Main(string[] args) {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(5);
            list.Add(3);
            list.Add(67);
            list.Add(12);
            list.Add(679);
            
            foreach (int i in list) {
                Console.Write(i + " ");
            }
            list.Sort();
            Console.WriteLine();
            foreach (int i in list) {
                Console.Write(i + " ");
            }
        }
    }
}
