using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Switch.Tests
{
    using FakeItEasy;

    using Swich.Main;
    using Swich.Main.Contracts;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

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
        public void ProcesaLosMensajesQueIngresan()
        {
            var messageQueuedIn = new MessageQueued();
            var messageDataIn = new MessageData();
            A.CallTo(() => messageDataFactory.Create(messageQueuedIn)).Returns(messageDataIn);
            
            worker.Procesar(messageQueuedIn);

            A.CallTo(() => dinamica.Ejecutar(messageDataIn)).MustHaveHappened();
        }
    }
}
