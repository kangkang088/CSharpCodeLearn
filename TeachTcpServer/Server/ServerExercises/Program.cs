using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerExercises {
    internal class Program {
        static Socket? socket;
        static List<Socket?> clientSockets = new List<Socket?>();
        static bool isClose = false;
        static void Main(string[] args) {
            //1.建立socket
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8080);
            socket.Bind(ipPoint);
            socket.Listen(1024);
            //2.等待客户端连接
            Thread acceptThread = new Thread(AcceptClientConnect);
            acceptThread.Start();
            //3.收发消息
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
            //4.释放连接，关闭套接字
            while (!isClose) {
                string? input = Console.ReadLine();
                if(input == "Quit") {
                    isClose = true;
                    for(int i = 0;i < clientSockets.Count;i++) {
                        clientSockets[i]?.Shutdown(SocketShutdown.Both);
                        clientSockets[i]?.Close();
                    }
                    clientSockets.Clear();
                    break;
                }
                else if(input?.Substring(0,2) == "B:") {
                    for(int i = 0; i < clientSockets.Count;i++) {
                        clientSockets[i]?.Send(Encoding.UTF8.GetBytes(input.Substring(2)));
                    }
                }
            }
        }
        static void AcceptClientConnect() {
            while(true) {
                Socket? socketClient = socket?.Accept();
                clientSockets.Add(socketClient);
                socketClient?.Send(Encoding.UTF8.GetBytes("欢迎你连入"));
            }
        }
        static void ReceiveMessage() {
            Socket? clientSocket;
            byte[] result = new byte[1024 * 1024];
            int receiveNumber;
            int i;
            while(true) {
                for(i = 0;i < clientSockets.Count;i++) {
                    clientSocket = clientSockets[i];
                    if(clientSocket?.Available > 0) {
                        receiveNumber = clientSocket.Receive(result);
                        ThreadPool.QueueUserWorkItem(HandleMessage,(clientSocket, Encoding.UTF8.GetString(result,0,receiveNumber)));
                    }
                }
            }
        }
        static void HandleMessage(object? obj) {
            (Socket s, string str) info = ((Socket s, string str))obj;
            Console.WriteLine("收到客户端{0}发来的信息：{1}",info.s.RemoteEndPoint,info.str);
        }
    }
}
