namespace UDPServerExercises {
    internal class Program {
        public static ServerSocket serverSocket;
        static void Main(string[] args) {
            serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1",8080);
            Console.WriteLine("UDP服务器启动");
            while (true) {
                string input = Console.ReadLine();
                if (input.Substring(0,2) == "B:") {
                    PlayerMsg playerMsg = new PlayerMsg();
                    playerMsg.playerData = new PlayerData();
                    playerMsg.playerID = 1001;
                    playerMsg.playerData.name = "kangkang";
                    playerMsg.playerData.atk = 100;
                    playerMsg.playerData.lev = 100;
                    serverSocket.Broadcast(playerMsg);
                }
            }
        }
    }
}
