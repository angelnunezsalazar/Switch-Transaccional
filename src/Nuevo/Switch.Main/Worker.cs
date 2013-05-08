namespace Swich.Main
{
    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    public class Worker
    {
        private readonly IMessageDataFactory messageDataFactory;

        private readonly IDinamica dinamica;

        public Worker(IMessageDataFactory messageDataFactory, IDinamica dinamica)
        {
            this.messageDataFactory = messageDataFactory;
            this.dinamica = dinamica;
        }

        public MessageQueued Procesar(MessageQueued messageQueuedIn)
        {
            var messageDataIn = messageDataFactory.Create(messageQueuedIn);
            var messageDataOut = this.dinamica.Ejecutar(messageDataIn);
            return new MessageQueued { Id = messageQueuedIn.Id, RawData = messageDataOut.RawData };
        }
    }
}