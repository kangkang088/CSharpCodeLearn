using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace TeachUdpAsyncServerExercises
{
    class Client
    {
        public IPEndPoint clientIPandPort;
        public string clientStrID;

        //上一次收到消息的时间
        public long frontTime = -1;

        public Client(string ip, int port)
        {
            //规则和外面一样 记录唯一ID 通过 ip + port 拼接的形式
            clientStrID = ip + port;
            //就把客户端的信息记录下来了
            clientIPandPort = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void ReceiveMsg(byte[] bytes)
        {
            //为了避免处理消息时 又 接受到了 其它消息 所以我们需要在处理之前 先把信息拷贝出来
            //处理消息和接收消息 用不同的容器 避免出现问题
            byte[] cacheBytes = new byte[512];
            bytes.CopyTo(cacheBytes, 0);
            //记录收到消息的 系统时间 单位为秒
            frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            ThreadPool.QueueUserWorkItem(ReceiveHandle, cacheBytes);
        }

        //多线程处理消息
        private void ReceiveHandle(object obj)
        {
            try
            {
                //取出传进来的字节
                byte[] bytes = obj as byte[];
                int nowIndex = 0;
                //先处理 ID
                int msgID = BitConverter.ToInt32(bytes, nowIndex);
                nowIndex += 4;
                //再处理 长度
                int msgLength = BitConverter.ToInt32(bytes, nowIndex);
                nowIndex += 4;
                //再解析消息体
                switch (msgID)
                {
                    case 1001:
                        PlayerMsg playerMsg = new PlayerMsg();
                        playerMsg.Reading(bytes, nowIndex);
                        Console.WriteLine(playerMsg.playerID);
                        Console.WriteLine(playerMsg.playerData.name);
                        Console.WriteLine(playerMsg.playerData.atk);
                        Console.WriteLine(playerMsg.playerData.lev);
                        break;
                    case 1003:
                        QuitMsg quitMsg = new QuitMsg();
                        //由于它没有消息体 所以不用反序列化
                        //quitMsg.Reading(bytes, nowIndex);
                        //处理退出
                        Program.serverSocket.RemoveClient(clientStrID);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("处理消息时出错" + e.Message);
                //如果出错 就不用记录这个客户端信息
                Program.serverSocket.RemoveClient(clientStrID);
            }

        }
    }
}
