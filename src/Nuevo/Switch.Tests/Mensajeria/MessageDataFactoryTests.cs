namespace Switch.Tests.Mensajeria
{
    using System.Collections.Generic;

    using FakeItEasy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Identificadores;
    using Swich.Main.Mensajeria;
    using Swich.Main.Queue;

    [TestClass]
    public class MessageDataFactoryTests
    {
        private MessageDataFactory factory;

        private IParser parser;

        private IIdentificadorGrupoMensaje identificadorGrupoMensaje;

        private IIdentificadorMensaje identificadorMensaje;

        private IIdentificadorTransaccional identificadorTransaccional;

        public MessageDataFactoryTests()
        {
            this.identificadorGrupoMensaje = A.Fake<IIdentificadorGrupoMensaje>();
            this.identificadorMensaje = A.Fake<IIdentificadorMensaje>();
            this.identificadorTransaccional = A.Fake<IIdentificadorTransaccional>();
            this.parser = A.Fake<IParser>();
            this.factory = new MessageDataFactory(this.parser, this.identificadorGrupoMensaje, this.identificadorMensaje, this.identificadorTransaccional);
        }

        [TestMethod]
        public void CreaUnMessageDataConLosValoresDelRequest()
        {
            var RAW_MESSAGE = "xxxxx";
            var ENTIDAD_ID = 1;

            var grupoMensaje = new GrupoMensaje { TipoMensaje = TipoMensaje.ISO8583 };
            A.CallTo(() => this.identificadorGrupoMensaje.Identificar(ENTIDAD_ID)).Returns(grupoMensaje);
            var mensaje = new Mensaje();
            A.CallTo(() => this.identificadorMensaje.Identificar(RAW_MESSAGE, A<List<ValorSelector>>.Ignored)).Returns(mensaje);
            var fields = new List<FieldData>();
            A.CallTo(() => this.parser.Parse(RAW_MESSAGE, mensaje)).Returns(fields);
            var transaccional = new MensajeTransaccional { Id = 1 };
            A.CallTo(() => this.identificadorTransaccional.Identificar(mensaje, fields)).Returns(transaccional);

            var messageQueued = new MessageQueued { EntidadId = ENTIDAD_ID, RawData = RAW_MESSAGE };
            var messageData = this.factory.Create(messageQueued);

            Assert.AreEqual(grupoMensaje.TipoMensaje, messageData.Tipo);
            Assert.AreEqual(fields, messageData.Fields);
            Assert.AreEqual(RAW_MESSAGE, messageData.RawData);
            Assert.AreEqual(transaccional.Id, messageData.MensajeTransaccionalId);
        }
    }
}