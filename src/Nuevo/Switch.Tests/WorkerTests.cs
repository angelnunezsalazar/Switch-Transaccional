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

            MessageFromQueue messageForQueue = new MessageFromQueue { EntidadId = 1, Contenido = "SELECTORAxxxxxxxxxx" };
            Mensaje mensajeObtenido = worker.IdentificarMensaje(messageForQueue);
            Assert.AreEqual(mensajeA, mensajeObtenido);

            messageForQueue = new MessageFromQueue { EntidadId = 1, Contenido = "xSELECTORBxxxxxxxxx" };
            mensajeObtenido = worker.IdentificarMensaje(messageForQueue);
            Assert.AreEqual(mensajeB, mensajeObtenido);
        }

        [TestMethod]
        [ExpectedException(typeof(MensajeNoIdentificadoException))]
        public void LanzaExceptionSiNoEncuentraUnMensajeDadoUnValorSelector()
        {
            var mensaje = new Mensaje();
            var grupoMensaje = new GrupoMensaje();
            grupoMensaje.ValoresSelectores = new List<ValorSelector>(new[]
                {
                    new ValorSelector
                        {
                            Posicion=1,
                            Longitud=8,
                            Valor="SELECTOR",
                            Mensaje= mensaje
                        }
                });
            A.CallTo(() => dataAccess.GrupoMensajePorEntidad(A<int>.Ignored)).Returns(grupoMensaje);

            MessageFromQueue messageForQueue = new MessageFromQueue { Contenido = "xxxxxxxxxxSELECTOR_EN_OTRA_POSICION" };
            worker.IdentificarMensaje(messageForQueue);
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

            MessageFromQueue messageForQueueIn = new MessageFromQueue { Id = 1, EntidadId = 1, Contenido = "SELECTOR_otros_campos_del_mensaje" };
            MessageFromQueue messageForQueueOut = worker.Procesar(messageForQueueIn);

            Assert.AreEqual(1, messageForQueueOut.Id);
            Assert.AreEqual(1, messageForQueueOut.EntidadId);
            Assert.AreEqual("nuevo_mensaje", messageForQueueOut.Contenido);
        }
    }
}


