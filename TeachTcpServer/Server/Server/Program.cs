using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {
    internal class Program {
        static void Main(string[] args) {

            //1.声明一个tcp套接字Socket
            Socket socketTcp = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //2.用Bind方法将套接字与本地地址绑定，将本机作为服务端
            try {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8080);
                socketTcp.Bind(ipPoint);
            }
            catch(Exception e) {
                Console.WriteLine("绑定失败，ip或端口号不合法");
                return;
            }
            //3.用Listen方法监听
            socketTcp.Listen(1024);
            Console.WriteLine("服务端绑定和监听结束，准备开始等待客户端连入！");
            //4.用Accept开始等待客户端连入,卡住，直到有客户端连入后，再向下执行
            //5.有客户端连入，Accept返回新的套接字，用于通信
            Socket socketClient = socketTcp.Accept();
            Console.WriteLine("有客户端连入了");
            //6.用Send和Receive相关方法收发数据
            //发送
            PlayerMsg playerMsg = new PlayerMsg();
            playerMsg.playerID = 666;
            playerMsg.playerData = new PlayerData();
            playerMsg.playerData.name = "kangkang";
            playerMsg.playerData.atk = 1000;
            playerMsg.playerData.lev = 900;
           
            socketClient.Send(playerMsg.Writing());
            //接受
            //用于数据存入的字节数组
            byte[] result = new byte[1024];
            //返回实际接收到的字节数
            int receiveNumber = socketClient.Receive(result);
            Console.WriteLine("接收到了来自{0}的消息：{1}",socketClient.RemoteEndPoint?.ToString(),Encoding.UTF8.GetString(result,0,receiveNumber));
            //7.释放连接
            socketClient.Shutdown(SocketShutdown.Both);
            //8.关闭套接字
            socketClient.Close();

            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
    }
}
