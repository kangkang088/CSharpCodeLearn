using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TeachUdpAsyncServerExercises
{
    class ServerSocket
    {
        private Socket socket;

        private bool isClose;

        //我们可以通过记录谁给我发了消息 把它的 ip和端口记下来 这样就认为它是我的客户端了嘛
        private Dictionary<string, Client> clientDic = new Dictionary<string, Client>();
        //用于接收消息的容器
        private byte[] cacheBytes = new byte[512];

        public void Start(string ip, int port)
        {
            EndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            //声明一个用于UDP通信的Socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                socket.Bind(ipPoint);
                isClose = false;
                //消息接收的处理 
                socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref ipPoint, ReceiveMsg, ipPoint);

                //定时检测超时线程
                ThreadPool.QueueUserWorkItem(CheckTimeOut);
            }
            catch (Exception e)
            {
                Console.WriteLine("UDP开启出错" + e.Message);
            }
        }

        private void CheckTimeOut(object obj)
        {
            long nowTime = 0;
            List<string> delList = new List<string>();
            while (true)
            {
                //每30s检测一次 是否移除长时间没有接收到消息的客户端信息
                Thread.Sleep(30000);
                //得到当前系统时间
                nowTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                foreach (Client c in clientDic.Values)
                {
                    //超过10秒没有收到消息的 客户端信息 需要被移除
                    if (nowTime - c.frontTime >= 10)
                        delList.Add(c.clientStrID);
                }
                //从待删除列表中移除 超时的客户端信息
                for (int i = 0; i < delList.Count; i++)
                    RemoveClient(delList[i]);
                delList.Clear();
            }
        }

        private void ReceiveMsg(IAsyncResult result)
        {
            //接收消息的容器
            //记录谁发的
            //用于拼接字符串 位移ID 是由 IP + 端口构成的
            
            EndPoint ipPoint = result.AsyncState as IPEndPoint;
            
            string ip = (ipPoint as IPEndPoint).Address.ToString();
            int port = (ipPoint as IPEndPoint).Port;
            string strID = ip + port;//拼接成一个唯一ID 这个是我们自定义的规则
            try
            {
                socket.EndReceiveFrom(result, ref ipPoint);
                //判断有没有记录这个客户端信息 如果有 用它直接处理消息
                if (clientDic.ContainsKey(strID))
                    clientDic[strID].ReceiveMsg(cacheBytes);
                else//如果没有 直接添加并且处理消息
                {
                    clientDic.Add(strID, new Client(ip, port));
                    clientDic[strID].ReceiveMsg(cacheBytes);
                }

                //继续接受消息
                socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref ipPoint, ReceiveMsg, ipPoint);
            }
            catch (SocketException s)
            {
                Console.WriteLine("接受消息出错" + s.SocketErrorCode + s.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("接受消息出错(非Socket错误)" + e.Message);
            }
        }

        //指定发送一个消息给某个目标
        public void SendTo(BaseMsg msg, IPEndPoint ipPoint)
        {
            try
            {
                byte[] bytes = msg.Writing();
                socket.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, ipPoint, (result) =>
                {
                    try
                    {
                        socket.EndSendTo(result);
                    }
                    catch (SocketException s)
                    {
                        Console.WriteLine("发消息出现问题" + s.SocketErrorCode + s.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发送消息出问题（可能是序列化问题）" + e.Message);
                    }
                }, null);
            }
            catch (SocketException s)
            {
                Console.WriteLine("发消息出现问题" + s.SocketErrorCode + s.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("发送消息出问题（可能是序列化问题）" + e.Message);
            }

        }

        public void Broadcast(BaseMsg msg)
        {
            //广播消息 给谁广播
            foreach (Client c in clientDic.Values)
            {
                SendTo(msg, c.clientIPandPort);
            }
        }

        public void Close()
        {
            if (socket != null)
            {
                isClose = true;
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }

        public void RemoveClient(string clientID)
        {
            if (clientDic.ContainsKey(clientID))
            {
                Console.WriteLine("客户端{0}被移除了" + clientDic[clientID].clientIPandPort);
                clientDic.Remove(clientID);
            }
        }
    }
}
