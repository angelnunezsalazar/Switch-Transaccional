namespace ComunicacionAutorizador
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    using Swich.Main.Queue;

    public class StateObject
    {
        public Socket WorkSocket = null;

        public const int BufferSize = 256;

        public byte[] Buffer = new byte[BufferSize];

        public StringBuilder Sb = new StringBuilder();
    }

    public class SocketHandler
    {
        private static int ENTIDAD_ID = 2;

        private ManualResetEvent connectDone = new ManualResetEvent(false);

        private ManualResetEvent sendDone = new ManualResetEvent(false);

        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        public void Connect(string message)
        {
            // Connect to a remote device.
            try
            {
                var ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12000);

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP, ConnectCallback, client);
                connectDone.WaitOne();

                // Send test data to the remote device.
                Send(client, message);
                sendDone.WaitOne();

                // Receive the response from the remote device.
                Receive(client);
                receiveDone.WaitOne();

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket client, String content)
        {
            var frame = content.Length.ToString().PadLeft(4, '0');
            var data = frame + content;
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.WorkSocket = client;
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (StateObject)ar.AsyncState;
                var handler = state.WorkSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.Sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                    var frame = state.Sb.ToString().Substring(0, 4);
                    var clientKey = state.Sb.ToString().Substring(4, 36);
                    var content = state.Sb.ToString().Substring(40);
                    if (state.Sb.ToString().Substring(4).Length == int.Parse(frame))
                    {
                        Console.WriteLine("Receive {0} bytes from client. \n Data: {1}", state.Sb.Length, state.Sb);

                        var messageQueued = new MessageQueued
                        {
                            ClientKey = clientKey,
                            EntidadId = ENTIDAD_ID,
                            RawData = content
                        };

                        QueueSender.Send(clientKey, messageQueued, QueueConstants.RESPONSE_QUEQUE);
                        receiveDone.Set();
                    }
                    else
                    {
                        handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}