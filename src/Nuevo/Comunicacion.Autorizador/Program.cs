using System.Threading;

namespace ComunicacionAutorizador
{
    using System;
    using System.Messaging;

    using Swich.Main.Queue;

    class Program
    {
        static void Main(string[] args)
        {
            var queueThread = new Thread(() =>
                {
                    var queueListener = new QueueListener(QueueConstants.BANKAUTHORIZER_QUEQUE, message =>
                        {
                            var data = message.Label + message.Body;
                            new SocketHandler().Connect(data);
                        });
                    queueListener.Start();
                });
            queueThread.Start();
        }
    }
}
