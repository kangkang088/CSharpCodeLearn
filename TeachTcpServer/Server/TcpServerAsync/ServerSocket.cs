using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerAsync {
    internal class ServerSocket {
        private Socket socket;
        private Dictionary<int,ClientSocket> clientDic = new Dictionary<int, ClientSocket>();
        public void Start(string ip, int port,int num) {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try {
                socket.Bind(ipPoint);
                socket.Listen(num);
                //随时可以获取到socket，那么最后一个参数不传了，不需要通过result.AsyncState了
                socket.BeginAccept(AcceptCallback,null);
            }
            catch(Exception e) {
                Console.WriteLine("服务器启动失败:" + e.Message);
            }
            
        }
        private void AcceptCallback(IAsyncResult result) {
            try {
                //获取通信用的socket
                Socket clientSocket = this.socket.EndAccept(result);
                ClientSocket client = new ClientSocket(clientSocket);
                //记录通信对象
                clientDic.Add(client.clientID, client);

                //继续让别的客户端可以连入
                this.socket.BeginAccept(AcceptCallback,null);
            }
            catch(SocketException e) {
                Console.WriteLine("客户端连入失败：" + e.SocketErrorCode + e.Message);
            }
        }
        public void Broadcast(string str) {
            foreach(ClientSocket item in clientDic.Values) {
                item.Send(str);
            }
        }
    }
}
