using System;
using System.Threading;

namespace Comunicacion
{
    class Program
    {
        static void Main(string[] args)
        {
            var socketThread = new Thread(delegate()
            {
                SocketHandler.StartListening();
            });
            socketThread.Start();

            var queueThread = new Thread(delegate()
            {
                QueueHandler.StartListening();
            });
            queueThread.Start();

            Console.WriteLine("CELULAR service started....");

            socketThread.Join();
            queueThread.Join();
        }
    }
}
