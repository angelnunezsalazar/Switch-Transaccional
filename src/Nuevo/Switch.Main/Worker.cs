namespace Swich.Main
{
    using Swich.Main.Contracts;
    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Queue;

    public class Worker
    {
        private readonly IParser parser;

        private readonly IDinamica dinamica;

        private readonly IDataAccess dataAccess;

        public Worker(IParser parser, IDinamica dinamica, IDataAccess dataAccess)
        {
            this.parser = parser;
            this.dinamica = dinamica;
            this.dataAccess = dataAccess;
        }

        public GrupoMensaje IdentificarGrupoMensaje(MessageQueued message)
        {
            return this.dataAccess.GrupoMensajePorEntidad(message.EntidadId);
        }

        public Mensaje IdentificarMensaje(MessageQueued messageForQueue, GrupoMensaje grupoMensaje)
        {
            foreach (var selector in grupoMensaje.ValoresSelectores)
            {
                var valorCampoSeleccionado = messageForQueue.RawData.Substring(selector.Posicion, selector.Longitud);
                if (valorCampoSeleccionado == selector.Valor)
                {
                    return selector.Mensaje;
                }
            }
            throw new MensajeNoIdentificadoException();
        }

        public MessageQueued Procesar(MessageQueued messageQueued)
        {
            var grupoMensaje = this.IdentificarGrupoMensaje(messageQueued);
            var mensaje = this.IdentificarMensaje(messageQueued, grupoMensaje);
            var messageDataIn = this.parser.Parse(messageQueued.RawData, mensaje);
            var messageDataOut = this.dinamica.Ejecutar(messageDataIn);
            messageQueued.RawData = messageDataOut.RawData;
            return messageQueued;
        }
    }
}