namespace ServerExercises2 {
    internal class Program {
        public static ServerSocket? socket;
        static void Main(string[] args) {
            socket = new ServerSocket();
            socket.Start("127.0.0.1",8080,1024);
            Console.WriteLine("服务器开启成功");
            while (true) {
                string? input = Console.ReadLine();
                if(input == "Quit") {
                    socket.Close();
                }
                else if(input?.Substring(0,2) == "B:") {
                    if(input.Substring(2) == "1001") {
                        PlayerMsg playerMsg = new PlayerMsg();
                        playerMsg.playerID = 9876;
                        playerMsg.playerData = new PlayerData();
                        playerMsg.playerData.name = "服务端端发来的消息";
                        playerMsg.playerData.atk = 80;
                        playerMsg.playerData.lev = 100;
                        socket.BroadCast(playerMsg);
                    }
                }
            }
        }
    }
}
