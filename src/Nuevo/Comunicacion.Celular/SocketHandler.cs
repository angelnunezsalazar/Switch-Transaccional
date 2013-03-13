using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Comunicacion
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 1024;
        public byte[] Buffer = new byte[BufferSize];
        public StringBuilder Sb = new StringBuilder();
    }

    public class SocketHandler
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private static ConcurrentDictionary<string, Socket> connectedClients = new ConcurrentDictionary<string, Socket>();

        public static void StartListening()
        {
            byte[] bytes = new Byte[1024];

            var ipHostInfo = Dns.Resolve(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddress, 11000);

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.Read();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            var listener = (Socket)ar.AsyncState;
            var handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.WorkSocket = handler;
            handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;
            var handler = state.WorkSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.Sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                var frame = state.Sb.ToString().Substring(0, 4);
                var content = state.Sb.ToString().Substring(4);
                if (content.Length == int.Parse(frame))
                {
                    Console.WriteLine("Receive {0} bytes from client. \n Data: {1}", state.Sb.Length, state.Sb);
                    //Send(handler, content);
                    var clientKey = Guid.NewGuid().ToString();
                    connectedClients.GetOrAdd(clientKey, handler);
                    QueueHandler.Send(clientKey, content);
                }
                else
                {
                    handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        public static void SendToClient(string clientKey, String data)
        {
            Socket handler;
            connectedClients.TryRemove(clientKey, out handler);
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var handler = (Socket)ar.AsyncState;

                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}