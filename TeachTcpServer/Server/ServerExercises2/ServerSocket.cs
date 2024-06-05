using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerExercises2 {
    internal class ServerSocket {
        public Socket? socket;
        public Dictionary<int,ClientSocket> clientDic = new Dictionary<int,ClientSocket>();
        private List<ClientSocket> delList = new List<ClientSocket>();
        private bool IsClose = false;
        public void Start(string ip,int port,int num) {
            IsClose = false;
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            socket.Bind(ipPoint);
            socket.Listen(num);
            ThreadPool.QueueUserWorkItem(Accept);
            ThreadPool.QueueUserWorkItem(Receive);
        }
        public void BroadCast(BaseMsg info) {
            lock(clientDic) {
                foreach(ClientSocket item in clientDic.Values) {
                    item.Send(info);
                }
            }
        }
        public void Close() {
            IsClose = true;
            foreach(ClientSocket item in clientDic.Values) {
                item.Close();
            }
            clientDic.Clear();
            socket?.Shutdown(SocketShutdown.Both);
            socket?.Close();
            socket = null;
        }
        public void CloseClientSocket(ClientSocket clientSocket) {
            lock(clientDic) {
                clientSocket.Close();
                if(clientDic.ContainsKey(clientSocket.ClientID)) {
                    clientDic.Remove(clientSocket.ClientID);
                }
            }
            Console.WriteLine("客户端{0}主动断开连接",clientSocket.ClientID);
        }
        public void AddDelSocket(ClientSocket clientSocket) {
            if(!delList.Contains(clientSocket)) {
                delList.Add(clientSocket);
            }
        }
        public void RemoveDelSocket() {
            for(int i = 0;i < delList.Count;i++) {
                CloseClientSocket(delList[i]);
            }
            delList.Clear();
        }
        private void Accept(object? obj) {
            while(!IsClose) {
                try {
                    Socket? socketClient = socket?.Accept();
                    ClientSocket client = new ClientSocket(socketClient);
                    //client.Send();
                    lock(clientDic)
                        clientDic.Add(client.ClientID,client);
                }
                catch(Exception e) {
                    Console.WriteLine("客户端连入报错" + e.Message);
                }
            }
        }
        private void Receive(object? obj) {
            while(!IsClose) {
                if(clientDic.Count > 0) {
                    lock(clientDic) {
                        foreach(ClientSocket item in clientDic.Values) {
                            item.Reveive();
                        }
                        RemoveDelSocket();
                    }

                }
            }
        }
    }
}
