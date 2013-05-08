namespace Swich.Main
{
    using Swich.Main.Contracts;
    using Swich.Main.Contracts.Fakes;
    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    public class Worker
    {
        private readonly IMessageDataFactory messageDataFactory;

        private readonly IDinamica dinamica;

        public Worker()
        {
            this.messageDataFactory=new MessageDataFactory();
            this.dinamica=new DinamicaFake();
        }

        public Worker(IMessageDataFactory messageDataFactory, IDinamica dinamica)
        {
            this.messageDataFactory = messageDataFactory;
            this.dinamica = dinamica;
        }

        public void Procesar(MessageQueued messageQueuedIn)
        {
            var messageDataIn = messageDataFactory.Create(messageQueuedIn);
            dinamica.Ejecutar(messageDataIn);
        }
    }
}