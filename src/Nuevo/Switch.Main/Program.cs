using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Swich.Main
{
    class Program
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private static readonly string MAQUINA = @".\";
        private static readonly string PRIVATE_QUEQUE = MAQUINA + @"Private$\";

        public static readonly string RESPONSE_QUEQUE = "Response";
        public static readonly string REQUEST_QUEQUE = "Request";

        public static readonly Type TIPO_SERIALIZACION = typeof(string);

        static void Main(string[] args)
        {
            MessageQueue queue = new MessageQueue(PRIVATE_QUEQUE + REQUEST_QUEQUE);
            queue.Formatter = new XmlMessageFormatter(new[] { TIPO_SERIALIZACION });

            Console.WriteLine("SWITCH service started....");
            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();

                queue.BeginReceive(new TimeSpan(1, 0, 0), queue, new AsyncCallback(ReceiveCallback));

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            var queue = (MessageQueue)ar.AsyncState;
            var message = queue.EndReceive(ar);

            Console.WriteLine("Processing message. Key: {0}", message.Label.ToString());
            var responseMessage = new Message(message.Body);
            responseMessage.Label = message.Label;
            message.ResponseQueue.Send(responseMessage);
        }
    }
}
