using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Switch.Tests
{
    using System.Collections.Generic;

    using FakeItEasy;

    using Swich.Main;
    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Queue;

    //TODO: Obtener el mensaje Transaccional

    [TestClass]
    public class WorkerTests
    {
        private IDataAccess dataAccess;

        private IDinamica dinamica;

        private IParser parser;

        private Worker worker;

        [TestInitialize]
        public void Setup()
        {
            dataAccess = A.Fake<IDataAccess>();
            dinamica = A.Fake<IDinamica>();
            parser = A.Fake<IParser>();
            worker = new Worker(parser, dinamica, dataAccess);
        }

        [TestMethod]
        public void IdentificaElMensajeDadoUnValorSelector()
        {
            var mensajeA = new Mensaje();
            var mensajeB = new Mensaje();
            var grupoMensaje = new GrupoMensaje();
            grupoMensaje.ValoresSelectores = new List<ValorSelector>(new[]
                {
                    new ValorSelector
                        {
                            Posicion=0,
                            Longitud=9,
                            Valor="SELECTORA",
                            Mensaje= mensajeA
                        },
                    new ValorSelector
                        {
                            Posicion=1,
                            Longitud=9,
                            Valor="SELECTORB",
                            Mensaje= mensajeB
                        },
                });

            A.CallTo(() => dataAccess.GrupoMensajePorEntidad(1)).Returns(grupoMensaje);

            MessageQueued messageForQueue = new MessageQueued { EntidadId = 1, RawData = "SELECTORAxxxxxxxxxx" };
            Mensaje mensajeObtenido = worker.IdentificarMensaje(messageForQueue, this.worker.IdentificarGrupoMensaje(messageForQueue));
            Assert.AreEqual(mensajeA, mensajeObtenido);

            messageForQueue = new MessageQueued { EntidadId = 1, RawData = "xSELECTORBxxxxxxxxx" };
            mensajeObtenido = worker.IdentificarMensaje(messageForQueue, this.worker.IdentificarGrupoMensaje(messageForQueue));
            Assert.AreEqual(mensajeB, mensajeObtenido);
        }

        [TestMethod]
        public void RetornaElMensajeDeRespuesta()
        {
            GrupoMensaje grupoMensaje = new GrupoMensaje();
            Mensaje mensaje = new Mensaje();
            mensaje.GrupoMensaje = grupoMensaje;
            grupoMensaje.ValoresSelectores = new List<ValorSelector>(new[]
                {
                    new ValorSelector
                        {
                            Posicion=0,
                            Longitud=8,
                            Valor="SELECTOR",
                            Mensaje= mensaje
                        }
                });

            A.CallTo(() => dataAccess.GrupoMensajePorEntidad(1)).Returns(grupoMensaje);

            MessageData messageDataIn = new MessageData();
            A.CallTo(() => parser.Parse("SELECTOR_otros_campos_del_mensaje", mensaje)).Returns(messageDataIn);

            MessageData messageDataOut = new MessageData();
            messageDataOut.RawData = "nuevo_mensaje";
            A.CallTo(() => dinamica.Ejecutar(messageDataIn)).Returns(messageDataOut);

            MessageQueued messageForQueueIn = new MessageQueued { Id = 1, EntidadId = 1, RawData = "SELECTOR_otros_campos_del_mensaje" };
            MessageQueued messageForQueueOut = worker.Procesar(messageForQueueIn);

            Assert.AreEqual(1, messageForQueueOut.Id);
            Assert.AreEqual(1, messageForQueueOut.EntidadId);
            Assert.AreEqual("nuevo_mensaje", messageForQueueOut.RawData);
        }
    }
}


