namespace Swich.Main.Mensajeria
{
    using Swich.Main.Contracts;
    using Swich.Main.Contracts.Fakes;
    using Swich.Main.Identificadores;
    using Swich.Main.Queue;

    public interface IMessageDataFactory
    {
        MessageData Create(MessageQueued messageQueued);
    }

    public class MessageDataFactory : IMessageDataFactory
    {
        private readonly IParser parser;

        private readonly IIdentificadorGrupoMensaje identificadorGrupoMensaje;

        private readonly IIdentificadorMensaje identificadorMensaje;

        private readonly IIdentificadorTransaccional identificadorTransaccional;

        public MessageDataFactory()
        {
            this.parser=new ParserFake();
            this.identificadorGrupoMensaje=new IdentificadorGrupoMensaje(new DataAccessFake());
            this.identificadorMensaje=new IdentificadorMensaje();
            this.identificadorTransaccional = new IdentificadorTransaccional();
        }

        public MessageDataFactory(IParser parser, IIdentificadorGrupoMensaje identificadorGrupoMensaje,
                                  IIdentificadorMensaje identificadorMensaje,
                                  IIdentificadorTransaccional identificadorTransaccional)
        {
            this.parser = parser;
            this.identificadorGrupoMensaje = identificadorGrupoMensaje;
            this.identificadorMensaje = identificadorMensaje;
            this.identificadorTransaccional = identificadorTransaccional;
        }

        public MessageData Create(MessageQueued messageQueued)
        {
            var grupoMensaje = this.identificadorGrupoMensaje.Identificar(messageQueued.EntidadId);
            var mensaje = this.identificadorMensaje.Identificar(messageQueued.RawData, grupoMensaje.ValoresSelectores);
            var fields = this.parser.Parse(messageQueued.RawData, mensaje);
            var transaccionalEncontrado = this.identificadorTransaccional.Identificar(mensaje, fields);
            return new MessageData
                {
                    RawData = messageQueued.RawData,
                    Fields = fields,
                    Tipo = grupoMensaje.TipoMensaje,
                    MensajeTransaccionalId = transaccionalEncontrado.Id,
                };
        }
    }
}