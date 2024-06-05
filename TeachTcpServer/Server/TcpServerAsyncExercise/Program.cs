using System;

namespace TeachTcpServerAsync
{
    class Program
    {
        public static ServerSocket serverSocket;
        static void Main(string[] args)
        {
            serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1", 8080, 1024);
            Console.WriteLine("开启服务器成功");

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Substring(2) == "1001")
                {
                    PlayerMsg msg = new PlayerMsg();
                    msg.playerID = 9876;
                    msg.playerData = new PlayerData();
                    msg.playerData.name = "服务器端发来的消息";
                    msg.playerData.lev = 99;
                    msg.playerData.atk = 80;
                    serverSocket.Broadcast(msg);
                }
            }
        }
    }
}
