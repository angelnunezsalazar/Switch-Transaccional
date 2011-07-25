using System;
using System.Net.Sockets;
using System.Text;

namespace PocketPC
{
    public class Servidor
    {
        public static string EnviarRecibir(byte[] trama)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(Coneccion.IP);

            string stringData = String.Empty;
            if (socket.Connected)
            {
                socket.Send(trama);
                byte[] data = new byte[1024];
                int receivedDataLength = socket.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                socket.Close();
            }
            return stringData;
        }
    }
}
