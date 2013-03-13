using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comunicacion
{
    public class QueueHandler
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private static readonly string MAQUINA = @".\";
        private static readonly string PRIVATE_QUEQUE = MAQUINA + @"Private$\";
        public static readonly string REQUEST_QUEQUE = "Request";
        public static readonly string CELLPHONE_QUEQUE = "CellPhone";

        public static readonly Type TIPO_SERIALIZACION = typeof(string);

        public static void StartListening()
        {
            var queue = new MessageQueue(PRIVATE_QUEQUE + CELLPHONE_QUEQUE);
            queue.Formatter = new XmlMessageFormatter(new[] { TIPO_SERIALIZACION });

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
            Console.WriteLine("Receive message. Key: {0}", message.Label.ToString());

            SocketHandler.SendToClient(message.Label, message.Body.ToString());
        }


        public static void Send(string clientKey, object body)
        {
            var requestQueue = new MessageQueue(PRIVATE_QUEQUE + REQUEST_QUEQUE);
            requestQueue.Formatter = new XmlMessageFormatter(new[] { TIPO_SERIALIZACION });
            var responseQueue = new MessageQueue(PRIVATE_QUEQUE + CELLPHONE_QUEQUE);
            responseQueue.Formatter = new XmlMessageFormatter(new[] { TIPO_SERIALIZACION });

            var message = new Message(body);
            message.Label = clientKey;
            message.ResponseQueue = responseQueue;

            requestQueue.Send(message);
            Console.WriteLine("Send message. Key: {0}", message.Label.ToString());
            requestQueue.Close();
        }
    }
}
