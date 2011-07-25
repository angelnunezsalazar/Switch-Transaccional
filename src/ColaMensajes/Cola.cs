using System;
using System.Messaging;


namespace ColaMensajes
{
    /// <summary>
    /// the information we return after we installed a message queue listener; it provides the message
    /// queue itself plus the handle to the AsyncResult object
    /// </summary>
    public struct MessageListener
    {
        public MessageQueue MQueue;
        public IAsyncResult AsyncResult;
    }

    public class Cola
    {
        public static readonly TimeSpan TimeSpan = new TimeSpan(1, 0, 0);

        private static readonly string MAQUINA = @".\";
        private static readonly string PRIVATE_QUEQUE = MAQUINA + @"Private$\";

        public static readonly string RESPONSE_QUEQUE = "ColaResponse";
        public static readonly string REQUEST_QUEQUE = "ColaRequest";

        public static readonly Type TIPO_SERIALIZACION = typeof(byte[]);

        public static void CrearCola(string nombreCola, bool transaccional)
        {
            if (!MessageQueue.Exists(PRIVATE_QUEQUE + nombreCola))
            {
                MessageQueue MQ = MessageQueue.Create(PRIVATE_QUEQUE + nombreCola, transaccional);

                MQ.Label = nombreCola;
                MQ.Close();
            }
        }

        public static void EliminarCola(string nombreCola)
        {
            if (MessageQueue.Exists(nombreCola))
            {
                try
                {
                    MessageQueue.Delete(nombreCola);
                }
                catch (MessageQueueException)
                {
                    Console.WriteLine("Se ha producido un error al crear la cola");
                }
            }
        }

        public static MessageListener StartListener(string nombreCola, ReceiveCompletedEventHandler MQueueReceiveEventHandler)
        {
            MessageListener MsgListener;

            MessageQueue.EnableConnectionCache = false;
            MessageQueue cola = new MessageQueue(PRIVATE_QUEQUE + nombreCola);

            EstablecerFormateador(cola);

            cola.ReceiveCompleted += MQueueReceiveEventHandler;

            IAsyncResult MQResult = cola.BeginReceive(TimeSpan, cola);

            MsgListener.MQueue = cola;
            MsgListener.AsyncResult = MQResult;
            return MsgListener;
        }

        public static void EnviarMensaje(object mensaje,string id,
            string nombreColaRequest, string nombreColaResponse)
        {
            MessageQueue colaRequest = new MessageQueue(PRIVATE_QUEQUE + nombreColaRequest);
            EstablecerFormateador(colaRequest);
            Message Msg = new Message(mensaje)
                              {
                                  ResponseQueue = new MessageQueue(PRIVATE_QUEQUE + nombreColaResponse)
                              };

            EstablecerFormateador(Msg.ResponseQueue);

            Msg.Label = id;
            colaRequest.Send(Msg);
            colaRequest.Close();
        }

        public static void EnviarMensaje(object mensaje, string id,
            MessageQueue colaRequest, string nombreColaResponse)
        {
            Message Msg = new Message(mensaje)
                              {
                                  ResponseQueue = new MessageQueue(PRIVATE_QUEQUE + nombreColaResponse)
                              };

            EstablecerFormateador(Msg.ResponseQueue);

            Msg.Label = id;
            colaRequest.Send(Msg);
            colaRequest.Close();
        }

        private static void EstablecerFormateador(MessageQueue cola)
        {
            cola.Formatter = new XmlMessageFormatter(new[] { TIPO_SERIALIZACION });
        }
    }
}
