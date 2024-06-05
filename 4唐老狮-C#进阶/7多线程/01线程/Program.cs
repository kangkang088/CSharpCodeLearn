
namespace _01线程 {
    internal class Program {
        private static bool isRunning = true;

        static void Main(string[] args) {
            Thread t = new Thread(NewThreadLogic);
            t.Start();
            while (true) {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("●");
            }
        }

        private static void NewThreadLogic() {
            while (isRunning) {
                Console.SetCursorPosition(10, 5);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("■");
            }
        }
    }
}
