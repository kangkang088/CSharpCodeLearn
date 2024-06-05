using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UDPServerExercises {
    internal class Client {
        public IPEndPoint clientIpEndPoint;
        public string clientStrID;
        public long frontTime = -1;
        public Client(string ip,int port) {
            clientStrID = ip + port;
            clientIpEndPoint = new IPEndPoint(IPAddress.Parse(ip),port);
        }
        public void ReceiveMessage(byte[] bytes) {
            byte[] cacheBytes = new byte[512];
            bytes.CopyTo(cacheBytes,0);
            frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            ThreadPool.QueueUserWorkItem(ReceiveHandle,cacheBytes);
        }

        private void ReceiveHandle(object? obj) {
            try {
                byte[]? bytes = obj as byte[];

                int nowIndex = 0;
                int msgID = BitConverter.ToInt32(bytes,nowIndex);
                nowIndex += 4;
                int msgLength = BitConverter.ToInt32(bytes,nowIndex);
                nowIndex += 4;
                switch(msgID) {
                    case 1001:
                        PlayerMsg playerMsg = new PlayerMsg();
                        playerMsg.Reading(bytes,nowIndex);
                        Console.WriteLine(playerMsg.playerID);
                        Console.WriteLine(playerMsg.playerData.name);
                        Console.WriteLine(playerMsg.playerData.atk);
                        Console.WriteLine(playerMsg.playerData.lev);
                        break;
                    case 1003:
                        QuitMsg quitMsg = new QuitMsg();
                        //quitMsg.Reading(bytes,nowIndex);
                        //退出
                        Program.serverSocket.RemoveClient(clientStrID);
                        break;
                }
            }
            catch(Exception e) {
                Console.WriteLine("处理消息时出错：" + e.Message);
                Program.serverSocket.RemoveClient(clientStrID);
            }

        }
    }
}
