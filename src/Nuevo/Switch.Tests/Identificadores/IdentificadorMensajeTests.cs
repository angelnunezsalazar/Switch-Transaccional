namespace Switch.Tests.Identificadores
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Identificadores;

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

            var rawMessage = "SELECTOR1xxxxxxxxxx";
            var identificadorMensaje = new IdentificadorMensaje();
            var mensaje = identificadorMensaje.Identificar(rawMessage, valoresSelectores);
            Assert.AreEqual(mensaje1, mensaje);

            rawMessage = "xSELECTOR2xxxxxxxxx";
            identificadorMensaje = new IdentificadorMensaje();
            mensaje = identificadorMensaje.Identificar(rawMessage, valoresSelectores);
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
            var identificadorMensaje = new IdentificadorMensaje();
            var rawMessage = "xxxxxxxxxxSELECTOR_EN_OTRA_POSICION";
            identificadorMensaje.Identificar(rawMessage, valoresSelectores);
        }

    }
}