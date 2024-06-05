using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer {
    internal class Program {
        static void Main(string[] args) {
            //1.创建UDP套接字
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //2.绑定本机地址
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8081);
            socket.Bind(ipPoint);
            Console.WriteLine("服务器开启成功");
            //3.接收消息
            byte[] bytes = new byte[512];
            EndPoint remoteIPPoint2 = new IPEndPoint(IPAddress.Any,0);
            //把给我发消息的那个服务器的IP和端口存入到声明的这个EndPoint里面
            int length = socket.ReceiveFrom(bytes,ref remoteIPPoint2);
            Console.WriteLine((remoteIPPoint2 as IPEndPoint).Address.ToString() + "发来了：" + Encoding.UTF8.GetString(bytes,0,length));
            //4.发送到指定目标
            //先收，就已经知道了谁给我们发的了
            //IPEndPoint remoteIPPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8081);
            socket.SendTo(Encoding.UTF8.GetBytes("welcome to host"),remoteIPPoint2);
            //5.释放、关闭
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
