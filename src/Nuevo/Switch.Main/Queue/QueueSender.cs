namespace Swich.Main.Queue
{
    using System;
    using System.Messaging;

    public class QueueSender
    {
        public static void Send(string clientKey, object body, string quequeName, string responseQueueName = null)
        {
            var requestQueue = new MessageQueue(QueueConstants.PRIVATE_QUEQUE + quequeName);
            requestQueue.Formatter = new XmlMessageFormatter(new[] { QueueConstants.TIPO_SERIALIZACION });

            var message = new Message(body);
            message.Label = clientKey;

            if (responseQueueName != null)
            {
                var responseQueue = new MessageQueue(QueueConstants.PRIVATE_QUEQUE + responseQueueName);
                responseQueue.Formatter = new XmlMessageFormatter(new[] { QueueConstants.TIPO_SERIALIZACION });
                message.ResponseQueue = responseQueue;
            }

            requestQueue.Send(message);
            Console.WriteLine("Send message. Key: {0}", message.Label);
            requestQueue.Close();
        }
    }
}