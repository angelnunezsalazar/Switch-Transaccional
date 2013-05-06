namespace Switch.Tests.Identificadores
{
    using System.Collections.Generic;

    using FakeItEasy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Identificadores;
    using Swich.Main.Queue;

    [TestClass]
    public class IdentificadorMensajeTests
    {
        [TestMethod]
        public void IdentificaElMensajeSegunValorSelector()
        {
            var mensaje1 = new Mensaje();
            var mensaje2 = new Mensaje();
            var valoresSelectores = new List<ValorSelector>
                {
                    new ValorSelector
                        {
                            Posicion = 0, Longitud = 9, Valor = "SELECTOR1", Mensaje = mensaje1
                        },
                    new ValorSelector
                        {
                            Posicion = 1, Longitud = 9, Valor = "SELECTOR2", Mensaje = mensaje2
                        },
                };

            var messageQueued = new MessageQueued { RawData = "SELECTOR1xxxxxxxxxx" };
            var identificadorMensaje = new IdentificadorMensaje();
            var mensaje = identificadorMensaje.Identificar(messageQueued, valoresSelectores);
            Assert.AreEqual(mensaje1, mensaje);

            messageQueued = new MessageQueued { RawData = "xSELECTOR2xxxxxxxxx" };
            identificadorMensaje = new IdentificadorMensaje();
            mensaje = identificadorMensaje.Identificar(messageQueued, valoresSelectores);
            Assert.AreEqual(mensaje2, mensaje);
        }

        [TestMethod]
        [ExpectedException(typeof(MensajeNoIdentificadoException))]
        public void LanzaExceptionSiNoIdentificaUnMensaje()
        {
            var valoresSelectores = new List<ValorSelector>(new[]
                {
                    new ValorSelector
                        {
                            Posicion=0,
                            Longitud=8,
                            Valor="SELECTOR",
                            Mensaje= new Mensaje()
                        }
                });
            var messageQueued = new MessageQueued { RawData = "xxxxxxxxxxSELECTOR_EN_OTRA_POSICION" };
            var identificadorMensaje = new IdentificadorMensaje();
            identificadorMensaje.Identificar(messageQueued, valoresSelectores);
        }

    }
}