using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerAsync {
    internal class ClientSocket {
        public Socket socket;
        public int clientID;
        public static int CLIENT_BEGIN_ID = 1;
        private byte[] cacheBytes = new byte[1024];
        //收到消息时放入缓存容器的位置
        private int cacheNum = 0;
        public ClientSocket(Socket socket) {
            this.socket = socket;
            this.clientID = CLIENT_BEGIN_ID++;

            //开始收消息
            this.socket.BeginReceive(cacheBytes,0,cacheBytes.Length,SocketFlags.None,ReceiveCallback,this.socket);
        }
        private void ReceiveCallback(IAsyncResult result) {
            try {
                cacheNum = this.socket.EndReceive(result);
                //通过字符串解析
                Console.WriteLine(Encoding.UTF8.GetString(cacheBytes,0,cacheNum));
                cacheNum = 0;
                if(this.socket.Connected) {
                    this.socket.BeginReceive(cacheBytes,0,cacheBytes.Length,SocketFlags.None,ReceiveCallback,this.socket);
                }
                else {
                    Console.WriteLine("连接断开，收消息停止");
                }
            }
            catch(SocketException e) {
                Console.WriteLine("接收消息失败：" + e.SocketErrorCode + e.Message);
            }
        }
        public void Send(string str) {
            if(this.socket.Connected) {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                //随时可以获取到socket，那么最后一个参数不传了，不需要通过result.AsyncState了
                this.socket.BeginSend(bytes,0,bytes.Length,SocketFlags.None,SendCallback,null);
            }
            else {
            
            }
        }
        private void SendCallback(IAsyncResult result) {
            try {
                this.socket.EndSend(result);
            }
            catch(SocketException e) {
                Console.WriteLine("发送消息失败：" + e.SocketErrorCode + e.Message);
            }
        }
    }
}
