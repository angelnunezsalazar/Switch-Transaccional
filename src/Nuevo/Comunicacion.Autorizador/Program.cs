using System.Threading;

namespace ComunicacionAutorizador
{
    using Swich.Main.Queue;

    //TODO: No se debe enviar el ClientKey al Autorizador

    class Program
    {
        static void Main(string[] args)
        {
            var queueThread = new Thread(() =>
                {
                    var queueListener = new QueueListener(QueueConstants.BANKAUTHORIZER_QUEQUE, messageQueued =>
                        {
                            var data = messageQueued.ClientKey + messageQueued.RawData;
                            new SocketHandler().Connect(data);
                        });
                    queueListener.Start();
                });
            queueThread.Start();
        }
    }
}
