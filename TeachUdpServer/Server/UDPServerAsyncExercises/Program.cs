using System;

namespace TeachUdpAsyncServerExercises
{
    class Program
    {
        public static ServerSocket serverSocket;
        static void Main(string[] args)
        {
            serverSocket = new ServerSocket();
            serverSocket.Start("127.0.0.1", 8080);

            Console.WriteLine("UDP服务器启动了");

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Substring(0, 2) == "B:")
                {
                    PlayerMsg msg = new PlayerMsg();
                    msg.playerData = new PlayerData();
                    msg.playerID = 1001;
                    msg.playerData.name = "唐老狮的UDP服务器";
                    msg.playerData.atk = 88;
                    msg.playerData.lev = 66;
                    serverSocket.Broadcast(msg);
                }
            }
        }
    }
}
