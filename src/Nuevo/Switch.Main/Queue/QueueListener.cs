namespace Swich.Main.Queue
{
    using System;
    using System.Messaging;
    using System.Threading;

    public class QueueListener
    {
        private readonly string queueName;

        private Action<MessageQueued> receiveHandler;

        private ManualResetEvent allDone = new ManualResetEvent(false);

        public QueueListener(string queueName, Action<MessageQueued> receiveHandler)
        {
            this.queueName = queueName;
            this.receiveHandler = receiveHandler;
        }

        public void Start()
        {
            var queue = new MessageQueue(QueueConstants.PRIVATE_QUEQUE + this.queueName);
            queue.Formatter = new XmlMessageFormatter(new[] { QueueConstants.TIPO_SERIALIZACION });

            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();

                queue.BeginReceive(new TimeSpan(1, 0, 0), queue, this.ReceiveCallback);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            var queue = (MessageQueue)ar.AsyncState;
            var message = queue.EndReceive(ar);
            Console.WriteLine("Receive message. Key: {0}", message.Label);
            this.receiveHandler(message.Body as MessageQueued);
        }
    }
}
