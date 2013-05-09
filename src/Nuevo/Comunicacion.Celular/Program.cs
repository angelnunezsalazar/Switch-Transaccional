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
        private static int ENTIDAD_ID = 1;

        private static ConcurrentDictionary<string, Socket> connectedClients = new ConcurrentDictionary<string, Socket>();

        static void Main(string[] args)
        {
            var socketThread = new Thread(() =>
                {
                    var socketHandler = new SocketListener(11000, (handler, content) =>
                        {
                            var clientKey = Guid.NewGuid().ToString();
                            connectedClients.GetOrAdd(clientKey, handler);
                            var messageQueued = new MessageQueued
                                {
                                    ClientKey = clientKey,
                                    EntidadId = ENTIDAD_ID,
                                    RawData = content
                                };
                            QueueSender.Send(clientKey, messageQueued, QueueConstants.REQUEST_QUEQUE, QueueConstants.CELLPHONE_QUEQUE);
                        });
                    socketHandler.StartListening();
                });
            socketThread.Start();

            var queueThread = new Thread(() =>
                {
                    var queueListener = new QueueListener(QueueConstants.CELLPHONE_QUEQUE, messageQueued =>
                        {
                            Socket handler;
                            connectedClients.TryRemove(messageQueued.ClientKey, out handler);
                            if (handler==null)
                            {
                                throw new ClienteConectadoNoReconocidoException(messageQueued.ClientKey);
                            }
                            SocketSender.Send(handler, messageQueued.RawData.ToString());
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
