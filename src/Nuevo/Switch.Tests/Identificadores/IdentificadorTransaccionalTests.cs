namespace Switch.Tests.Identificadores
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Identificadores;
    using Swich.Main.Mensajeria;

    [TestClass]
    public class IdentificadorTransaccionalTests
    {
        [TestMethod]
        public void IdentificaElMensajeTransaccional()
        {
            var transaccional1 = new MensajeTransaccional
                {
                    Id = 1,
                    CampoId = 1,
                    Valor = "TRANSACCIONAL1"
                };
            var transaccional2 = new MensajeTransaccional
                {
                    Id = 2,
                    CampoId = 2,
                    Valor = "TRANSACCIONAL2"
                };
            var mensaje = new Mensaje();
            mensaje.Transaccionales = new List<MensajeTransaccional>
                {
                    transaccional1,transaccional2
                };

            var fields = new List<FieldData> { new FieldData { CampoId = 1, Data = "TRANSACCIONAL1" }, new FieldData { CampoId = 2, Data = "xxxxxxxxxxx" } };
            var identificadorTransaccional = new IdentificadorTransaccional();
            var transaccional = identificadorTransaccional.Identificar(mensaje, fields);
            Assert.AreEqual(transaccional1, transaccional);

            fields = new List<FieldData> { new FieldData { CampoId = 1, Data = "xxxxxxxxxxx" }, new FieldData { CampoId = 2, Data = "TRANSACCIONAL2" } };
            identificadorTransaccional = new IdentificadorTransaccional();
            transaccional = identificadorTransaccional.Identificar(mensaje, fields);
            Assert.AreEqual(transaccional2, transaccional);
        }

        [TestMethod]
        [ExpectedException(typeof(TransaccionalNoIdentificadoException))]
        public void LanzaExcepcionSiNoIdentificaElTransaccional()
        {
            var identificadorTransaccional = new IdentificadorTransaccional();
            var mensaje = new Mensaje
                {
                    Transaccionales = new List<MensajeTransaccional>
                        {
                            new MensajeTransaccional
                                {
                                    CampoId = 1, Valor = "VALOR"
                                }
                        }
                };
            var fields = new List<FieldData>
                {
                    new FieldData
                        {
                            CampoId = 1,
                            Data = "VALOR_INCORRECTO"
                        }
                };
            identificadorTransaccional.Identificar(mensaje, fields);
        }
    }
}