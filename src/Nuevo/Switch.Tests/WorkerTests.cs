using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Switch.Tests
{
    using FakeItEasy;

    using Swich.Main;
    using Swich.Main.Contracts;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    //TODO: Ver si devuelve el mismo EntidadId

    [TestClass]
    public class WorkerTests
    {
        private IMessageDataFactory messageDataFactory;

        private IDinamica dinamica;

        private Worker worker;

        public WorkerTests()
        {
            messageDataFactory = A.Fake<IMessageDataFactory>();
            dinamica = A.Fake<IDinamica>();
            worker = new Worker(messageDataFactory, dinamica);
        }

        [TestMethod]
        public void RetornaLaRespuestaConElMismoIdDeLaSolicitud()
        {
            var messageQueuedIn = new MessageQueued { Id = 1 };
            var messageQueuedOut = worker.Procesar(messageQueuedIn);

            Assert.AreEqual(messageQueuedIn.Id, messageQueuedOut.Id);
        }

        [TestMethod]
        public void RetornaLaRespuestaConElMensajeEnRaw()
        {
            var messageQueuedIn = new MessageQueued();
            var messageDataIn = new MessageData();
            A.CallTo(() => messageDataFactory.Create(messageQueuedIn)).Returns(messageDataIn);
            var messageDataOut = new MessageData{RawData = "xxxxx"};
            A.CallTo(() => dinamica.Ejecutar(messageDataIn)).Returns(messageDataOut);

            var messageQueuedOut = worker.Procesar(messageQueuedIn);

            Assert.AreEqual("xxxxx", messageQueuedOut.RawData);
        }
    }
}
