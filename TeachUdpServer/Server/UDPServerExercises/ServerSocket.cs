using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServerExercises {
    internal class ServerSocket {
        private Socket socket;
        private bool isClosed;
        private Dictionary<string,Client> clientDic = new Dictionary<string,Client>();
        public void Start(string ip,int port) {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            try {
                socket.Bind(ipPoint);
                isClosed = false;
                //消息接收的处理
                ThreadPool.QueueUserWorkItem(ReceiveMessage);
                //定时检测超时线程
                ThreadPool.QueueUserWorkItem(CheckTimeout);
            }
            catch(Exception e) {
                Console.WriteLine("绑定失败:" + e.Message);
            }

        }
        private void CheckTimeout(object? obj) {
            long nowTime = 0;
            List<string> delList = new List<string>();
            while(true) {
                Thread.Sleep(30000);
                nowTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                foreach(Client item in clientDic.Values) {
                    if(nowTime - item.frontTime >= 10) {
                        delList.Add(item.clientStrID);
                    }
                }
                for(int i = 0;i < delList.Count;i++) {
                    //clientDic.Remove(delList[i]);
                    RemoveClient(delList[i]);
                }
                delList.Clear();
            }
        }
        private void ReceiveMessage(object? obj) {
            byte[] bytes = new byte[512];
            EndPoint remoteIPPoint = new IPEndPoint(IPAddress.Any,0);
            string strID = "";
            string ip;
            int port;
            while(!isClosed) {
                if(socket.Available > 0) {
                    lock(socket)
                        socket.ReceiveFrom(bytes,ref remoteIPPoint);
                    //处理消息
                    ip = (remoteIPPoint as IPEndPoint).Address.ToString();
                    port = (remoteIPPoint as IPEndPoint).Port;
                    strID = ip + port;
                    if(clientDic.ContainsKey(strID)) {
                        clientDic[strID].ReceiveMessage(bytes);
                    }
                    else {
                        clientDic.Add(strID,new Client(ip,port));
                        clientDic[strID].ReceiveMessage(bytes);
                    }
                }
            }
        }
        public void SendTo(BaseMsg msg,IPEndPoint ipPoint) {
            try {
                lock(socket)
                    socket.SendTo(msg.Writing(),ipPoint);
            }
            catch(SocketException e) {
                Console.WriteLine("发送消息出错：" + e.SocketErrorCode + e.Message);
                throw;
            }
            catch(Exception e) {
                Console.WriteLine("可能为序列化问题：" + e.Message);
            }

        }
        public void Broadcast(BaseMsg msg) {
            foreach(Client item in clientDic.Values) {
                SendTo(msg,item.clientIpEndPoint);
            }
        }
        public void Close() {
            if(socket  != null) {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
                isClosed = true;
            }
        }
        public void RemoveClient(string clientID) {
            if(clientDic.ContainsKey(clientID)) {
                clientDic.Remove(clientID);
                Console.WriteLine("客户端：" + clientID + "被移除了");
            }
        }
    }
}
