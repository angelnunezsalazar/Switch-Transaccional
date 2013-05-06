namespace Switch.Tests
{
    using FakeItEasy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Swich.Main;
    using Swich.Main.Contracts;
    using Swich.Main.Identificadores;
    using Swich.Main.Queue;

    [TestClass]
    public class MessageDataFactoryTests
    {
        private MessageDataFactory factory;

        private IDataAccess dataAccess;

        private IParser parser;
        public MessageDataFactoryTests()
        {
            dataAccess = A.Fake<IDataAccess>();
            parser = A.Fake<IParser>();
            factory = new MessageDataFactory(dataAccess, parser);
        }
    }

    public class MessageDataFactory
    {
        private readonly IParser parser;

        private readonly IIdentificadorGrupoMensaje identificadorGrupoMensaje;

        private readonly IdentificadorMensaje identificadorMensaje;

        private readonly IdentificadorTransaccional identificadorTransaccional;

        public MessageDataFactory(IDataAccess dataAccess, IParser parser)
        {
            this.parser = parser;
            this.identificadorGrupoMensaje=new IdentificadorGrupoMensaje(dataAccess);
            this.identificadorMensaje = new IdentificadorMensaje();
            this.identificadorTransaccional=new IdentificadorTransaccional();
        }

        public MessageData Create(MessageQueued messageQueued)
        {
            var grupoMensaje = this.identificadorGrupoMensaje.Identificar(messageQueued);
            var mensaje = this.identificadorMensaje.Identificar(messageQueued, grupoMensaje.ValoresSelectores);
            var fields = parser.Parse1(messageQueued.RawData, mensaje);
            var transaccionalEncontrado = this.identificadorTransaccional.IdentificarTransaccional(mensaje, fields);
            return new MessageData { Tipo = grupoMensaje.TipoMensaje, Fields = fields, MensajeTransaccionalId = transaccionalEncontrado.Id };
        }
    }
}