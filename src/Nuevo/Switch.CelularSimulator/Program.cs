using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Switch.CelularSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);//Esperar a que los servicios levanten

            var data = Enviar(Encoding.ASCII.GetBytes("00101234567890"));
            Console.WriteLine("Receive {0} bytes from socket. \n Data : {1}", data.Length, data);
            data = Enviar(Encoding.ASCII.GetBytes("0010ABCDEFGHIJ"));
            Console.WriteLine("Receive {0} bytes from socket. \n Data : {1}", data.Length, data);
            
            Console.ReadLine();
        }

        static string Enviar(byte[] trama)
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint endpoint = new IPEndPoint(ipAddress, 11000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endpoint);

            string stringData = String.Empty;
            if (socket.Connected)
            {
                socket.Send(trama);
                byte[] data = new byte[1024];
                int length = socket.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, length);
                socket.Close();
            }
            return stringData;
        }
    }
}
