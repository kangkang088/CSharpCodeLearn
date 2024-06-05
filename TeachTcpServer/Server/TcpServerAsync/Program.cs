namespace TcpServerAsync {
    internal class Program {
        static void Main(string[] args) {
            ServerSocket serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1",8080,1024);
            Console.WriteLine("开启服务器成功");
            while (true) {
                string input = Console.ReadLine();
                if(input.Substring(0,2) == "B:") {
                    serverSocket.Broadcast(input.Substring(2));
                }
            }
        }
    }
}
