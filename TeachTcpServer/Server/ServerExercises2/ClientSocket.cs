using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerExercises2 {
    internal class ClientSocket {
        private static int CLIENT_BEGIN_ID = 1;
        public int ClientID;
        public Socket? socket;
        public bool IsConnect => this.socket.Connected;

        private byte[] cacheBytes = new byte[1024 * 1024];
        private int cacheNum = 0;

        private long frontTime = -1;
        private static int TIME_OUT_TIME = 10;

        public ClientSocket(Socket? socket) {
            this.ClientID = CLIENT_BEGIN_ID;
            this.socket = socket;
            CLIENT_BEGIN_ID++;

            //新开线程检测很浪费性能
            //ThreadPool.QueueUserWorkItem(CheckTimeOut);
        }
        private void CheckTimeOut(/*object? obj*/) {
            //while (IsConnect) {
                if(frontTime != -1 && DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= TIME_OUT_TIME) {
                    Program.socket?.AddDelSocket(this);
                    //break;
                }
                //Thread.Sleep(5000);
            //}
            
        }
        public void Close() {
            if(socket != null) {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }

        public void Send(BaseMsg info) {
            if(IsConnect) {
                try {
                    socket?.Send(info.Writing());
                }
                catch(Exception e) {
                    Console.WriteLine("收消息出错：" + e.Message);
                    Program.socket?.AddDelSocket(this);
                }
            }
            else {
                Program.socket?.AddDelSocket(this);
            }
        }
        public void Reveive() {
            if(!IsConnect) {
                Program.socket?.AddDelSocket(this);
                return;
            }
            try {
                byte[] result = new byte[1024 * 5];
                if(socket?.Available > 0) {
                    int receiveNumber = socket.Receive(result);
                    HandleReceiveMessage(result,receiveNumber);
                    //int msgID = BitConverter.ToInt32(result, 0);
                    //BaseMsg baseMsg = null;
                    //switch(msgID) {
                    //    case 1001:
                    //        baseMsg = new PlayerMsg();
                    //        baseMsg.Reading(result,4);
                    //        break; 
                    //}
                    //if(baseMsg == null) {
                    //    return;
                    //}
                    //ThreadPool.QueueUserWorkItem(MessageHandle,baseMsg);
                }
                CheckTimeOut();
            }
            catch(Exception e) {
                Console.WriteLine("发消息出错:" + e.Message);
                Program.socket?.AddDelSocket(this);
            }
        }
        private void HandleReceiveMessage(byte[] receiveBytes,int receiveNumber) {
            int msgID = 0;
            int msgLength = 0;
            int nowIndex = 0;
            //收到消息时，看看之前有没有缓存的，有的话，直接拼接
            receiveBytes.CopyTo(cacheBytes,cacheNum);
            cacheNum += receiveNumber;
            while(true) {
                msgLength = -1;
                if(cacheNum - nowIndex >= 8) {
                    msgID = BitConverter.ToInt32(cacheBytes,nowIndex);
                    nowIndex += 4;
                    msgLength = BitConverter.ToInt32(cacheBytes,nowIndex);
                    nowIndex += 4;
                }
                if(cacheNum - nowIndex >= msgLength && msgLength != -1) {
                    BaseMsg baseMsg = null;
                    switch(msgID) {
                        case 1001:
                            baseMsg = new PlayerMsg();
                            baseMsg.Reading(cacheBytes,nowIndex);
                            break;
                        case 1003:
                            baseMsg = new QuitMsg();
                            break;
                        case 999:
                            baseMsg = new HeartMsg();
                            break;
                    }
                    if(baseMsg != null)
                        ThreadPool.QueueUserWorkItem(MessageHandle,baseMsg);
                    nowIndex += msgLength;
                    if(nowIndex == cacheNum) {
                        cacheNum = 0;
                        break;
                    }

                }
                else {
                    //不满足，有分包，把当前收到的内容记录下来，下次接收到消息的时候再处理
                    // receiveBytes.CopyTo(cacheBytes, 0);
                    // cacheNum = receiveNumber;
                    if(msgLength != -1)
                        nowIndex -= 8;
                    Array.Copy(cacheBytes,nowIndex,cacheBytes,0,cacheNum - nowIndex);
                    cacheNum = cacheNum - nowIndex;
                    break;
                }
            }

        }
        public void MessageHandle(object? obj) {
            BaseMsg msg = obj as BaseMsg;
            if(msg is PlayerMsg) {
                PlayerMsg playerMsg = msg as PlayerMsg;
                Console.WriteLine(playerMsg.playerID);
                Console.WriteLine(playerMsg.playerData.name);
                Console.WriteLine(playerMsg.playerData.atk);
                Console.WriteLine(playerMsg.playerData.lev);
            }
            else if(msg is QuitMsg) {
                Program.socket?.AddDelSocket(this);
            }else if(msg is HeartMsg) {
                frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                Console.WriteLine("收到心跳消息");
            }
        }
    }
}
