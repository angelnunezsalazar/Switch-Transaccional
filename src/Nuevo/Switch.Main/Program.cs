using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Swich.Main
{
    using Swich.Main.Contracts.Fakes;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    class Program
    {
        static void Main(string[] args)
        {
            var queueRequestThread = new Thread(() =>
            {
                var program = new Program();
                new QueueListener(QueueConstants.REQUEST_QUEQUE, program.SentToAuthorizerQueue).Start();
            });
            queueRequestThread.Start();

            var queueResponseThread = new Thread(() =>
            {
                var program = new Program();
                new QueueListener(QueueConstants.RESPONSE_QUEQUE, program.SentToCellQueue).Start();
            });
            queueResponseThread.Start();

            Console.WriteLine("SWITCH service started....");

            queueRequestThread.Join();
            queueResponseThread.Join();
        }

        private void SentToAuthorizerQueue(Message message)
        {
            var worker = new Worker();
            worker.Procesar(null);
        }

        private void SentToCellQueue(Message message)
        {
            var worker = new Worker();
            worker.Procesar(null);
        }
    }
}
