using System;
using System.Threading;

namespace Comunicacion
{
    using System.Collections.Concurrent;
    using System.Net.Sockets;

    using Swich.Main.Queue;
    using Swich.Main.Socket;

    class Program
    {
        private static ConcurrentDictionary<string, Socket> connectedClients = new ConcurrentDictionary<string, Socket>();

        static void Main(string[] args)
        {
            var socketThread = new Thread(() =>
                {
                    var socketHandler = new SocketListener(11000, (handler, content) =>
                        {
                            var clientKey = Guid.NewGuid().ToString();
                            connectedClients.GetOrAdd(clientKey, handler);
                            QueueSender.Send(clientKey, content, QueueConstants.REQUEST_QUEQUE, QueueConstants.CELLPHONE_QUEQUE);
                        });
                    socketHandler.StartListening();
                });
            socketThread.Start();

            var queueThread = new Thread(() =>
                {
                    var queueListener = new QueueListener(QueueConstants.CELLPHONE_QUEQUE, message =>
                        {
                            Socket handler;
                            connectedClients.TryRemove(message.Label, out handler);
                            SocketSender.Send(handler, message.Body.ToString());
                        });
                    queueListener.Start();
                });
            queueThread.Start();

            Console.WriteLine("CELULAR service started....");
            socketThread.Join();
            queueThread.Join();
        }
    }
}
