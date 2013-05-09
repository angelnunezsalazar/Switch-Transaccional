using System;
using System.Threading;

namespace Swich.Main
{
    using Swich.Main.Queue;

    class Program
    {
        static void Main(string[] args)
        {
            var queueRequestThread = new Thread(() => new QueueListener(QueueConstants.REQUEST_QUEQUE, messageQueued =>
                {
                    var worker = new Worker();
                    worker.Procesar(messageQueued);

                }).Start());
            queueRequestThread.Start();

            var queueResponseThread = new Thread(() => new QueueListener(QueueConstants.RESPONSE_QUEQUE, messageQueued =>
                {
                    var worker = new Worker();
                    worker.Procesar(messageQueued);

                }).Start());
            queueResponseThread.Start();

            Console.WriteLine("SWITCH service started....");

            queueRequestThread.Join();
            queueResponseThread.Join();
        }
    }
}
