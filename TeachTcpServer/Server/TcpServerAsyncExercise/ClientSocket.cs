using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TeachTcpServerAsync
{
    class ClientSocket
    {
        public Socket socket;
        public int clientID;
        private static int CLIENT_BEGIN_ID = 1;

        private byte[] cacheBytes = new byte[1024];
        private int cacheNum = 0;

        //上一次收到消息的时间
        private long frontTime = -1;
        //超时时间
        private static int TIME_OUT_TIME = 10;

        public ClientSocket(Socket socket)
        {
            this.clientID = CLIENT_BEGIN_ID++;
            this.socket = socket;

            //开始收消息
            this.socket.BeginReceive(cacheBytes, cacheNum, cacheBytes.Length, SocketFlags.None, ReceiveCallBack, null);
            ThreadPool.QueueUserWorkItem(CheckTimeOut);
        }

        /// <summary>
        /// 间隔一段时间 检测一次超时 如果超时 就会主动断开该客户端的连接
        /// </summary>
        /// <param name="obj"></param>
        private void CheckTimeOut(object obj)
        {
            while (this.socket != null && this.socket.Connected)
            {
                if (frontTime != -1 &&
                DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= TIME_OUT_TIME)
                {
                    Program.serverSocket.CloseClientSocket(this);
                    break;
                }
                Thread.Sleep(5000);
            }
        }

        private void ReceiveCallBack(IAsyncResult result)
        {
            try
            {
                if (this.socket != null && this.socket.Connected)
                {
                    //消息成功
                    int num = this.socket.EndReceive(result);
                    //处理分包黏包
                    HandleReceiveMsg(num);
                    this.socket.BeginReceive(cacheBytes, cacheNum, cacheBytes.Length - cacheNum, SocketFlags.None, ReceiveCallBack, this.socket);
                }
                else
                {
                    Console.WriteLine("没有连接，不用再收消息了");
                    Program.serverSocket.CloseClientSocket(this);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("接受消息错误" + e.SocketErrorCode + e.Message);
                Program.serverSocket.CloseClientSocket(this);
            }
        }

        public void Send(BaseMsg msg)
        {
            if(socket != null && socket.Connected)
            {
                byte[] bytes = msg.Writing();
                socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallBack, null);
            }
            else
            {
                Program.serverSocket.CloseClientSocket(this);
            }
        }

        private void SendCallBack(IAsyncResult result)
        {
            try
            {
                if (socket != null && socket.Connected)
                    this.socket.EndSend(result);
                else
                    Program.serverSocket.CloseClientSocket(this);
            }
            catch (SocketException e)
            {
                Console.WriteLine("发送失败" + e.SocketErrorCode + e.Message);
                Program.serverSocket.CloseClientSocket(this);
            }
        }

        public void Close()
        {
            if(socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }

        //处理分包黏包
        private void HandleReceiveMsg(int receiveNum)
        {
            int msgID = 0;
            int msgLength = 0;
            int nowIndex = 0;

            //由于消息接收后是直接存储在 cacheBytes中的 所以不需要进行什么拷贝操作
            //收到消息的字节数量
            cacheNum += receiveNum;

            while (true)
            {
                //每次将长度设置为-1 是避免上一次解析的数据 影响这一次的判断
                msgLength = -1;
                //处理解析一条消息
                if (cacheNum - nowIndex >= 8)
                {
                    //解析ID
                    msgID = BitConverter.ToInt32(cacheBytes, nowIndex);
                    nowIndex += 4;
                    //解析长度
                    msgLength = BitConverter.ToInt32(cacheBytes, nowIndex);
                    nowIndex += 4;
                }

                if (cacheNum - nowIndex >= msgLength && msgLength != -1)
                {
                    //解析消息体
                    BaseMsg baseMsg = null;
                    switch (msgID)
                    {
                        case 1001:
                            baseMsg = new PlayerMsg();
                            baseMsg.Reading(cacheBytes, nowIndex);
                            break;
                        case 1003:
                            baseMsg = new QuitMsg();
                            //由于该消息没有消息体 所以都不用反序列化
                            break;
                        case 999:
                            baseMsg = new HeartMsg();
                            //由于该消息没有消息体 所以都不用反序列化
                            break;
                    }
                    if (baseMsg != null)
                        ThreadPool.QueueUserWorkItem(MsgHandle, baseMsg);
                    nowIndex += msgLength;
                    if (nowIndex == cacheNum)
                    {
                        cacheNum = 0;
                        break;
                    }
                }
                else
                {
                    //如果不满足 证明有分包 
                    //那么我们需要把当前收到的内容 记录下来
                    //有待下次接受到消息后 再做处理
                    //receiveBytes.CopyTo(cacheBytes, 0);
                    //cacheNum = receiveNum;
                    //如果进行了 id和长度的解析 但是 没有成功解析消息体 那么我们需要减去nowIndex移动的位置
                    if (msgLength != -1)
                        nowIndex -= 8;
                    //就是把剩余没有解析的字节数组内容 移到前面来 用于缓存下次继续解析
                    Array.Copy(cacheBytes, nowIndex, cacheBytes, 0, cacheNum - nowIndex);
                    cacheNum = cacheNum - nowIndex;
                    break;
                }
            }

        }

        //消息处理
        private void MsgHandle(object obj)
        {
            switch (obj)
            {
                case PlayerMsg msg:
                    PlayerMsg playerMsg = msg as PlayerMsg;
                    Console.WriteLine(playerMsg.playerID);
                    Console.WriteLine(playerMsg.playerData.name);
                    Console.WriteLine(playerMsg.playerData.lev);
                    Console.WriteLine(playerMsg.playerData.atk);
                    break;
                case QuitMsg msg:
                    //收到断开连接消息 把自己添加到待移除的列表当中
                    Program.serverSocket.CloseClientSocket(this);
                    break;
                case HeartMsg msg:
                    //收到心跳消息 记录收到消息的时间
                    frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                    Console.WriteLine("收到心跳消息");
                    break;
            }
        }
    }
}
