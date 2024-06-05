using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TeachTcpServerAsync
{
    class ServerSocket
    {
        private Socket socket;
        private Dictionary<int, ClientSocket> clientDic = new Dictionary<int, ClientSocket>();

        public void Start(string ip, int port, int num)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                socket.Bind(ipPoint);
                socket.Listen(num);
                //通过异步接受客户端连入
                socket.BeginAccept(AcceptCallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("启动服务器失败" + e.Message);
            }
        }

        private void AcceptCallBack(IAsyncResult result)
        {
            try
            {
                //获取连入的客户端
                Socket clientSocket = socket.EndAccept(result);
                ClientSocket client = new ClientSocket(clientSocket);
                //记录客户端对象
                clientDic.Add(client.clientID, client);

                //继续去让别的客户端可以连入
                socket.BeginAccept(AcceptCallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("客户端连入失败" + e.Message);
            }
        }

        public void Broadcast(BaseMsg msg)
        {
            foreach (ClientSocket client in clientDic.Values)
            {
                client.Send(msg);
            }
        }

        //关闭客户端连接的 从字典中移除
        public void CloseClientSocket(ClientSocket socket)
        {
            lock (clientDic)
            {
                socket.Close();
                if (clientDic.ContainsKey(socket.clientID))
                {
                    clientDic.Remove(socket.clientID);
                    Console.WriteLine("客户端{0}主动断开连接了", socket.clientID);
                }
            }
        }
    }
}
