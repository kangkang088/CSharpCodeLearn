namespace _01反射 {
    class Test {
        private int i = 1;
        public int j = 0;
        public string str = "123";

        public Test() {
        }
        public Test(int i) {
            this.i = i;
        }
        public Test(int i, string str) : this(i) {
            this.str = str;
        }
        public void Speak() {
            Console.WriteLine(i);
        }
    }
    internal class Program {
        static void Main(string[] args) {

        }
    }
}
