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

        private GrupoMensaje IdentificarGrupoMensaje(MessageFromQueue message)
        {
            return this.dataAccess.GrupoMensajePorEntidad(message.EntidadId);
        }

        public Mensaje IdentificarMensaje(MessageFromQueue messageForQueue)
        {
            GrupoMensaje grupoMensaje = this.IdentificarGrupoMensaje(messageForQueue);

            foreach (var selector in grupoMensaje.ValoresSelectores)
            {
                var valorCampoSeleccionado = messageForQueue.Contenido.Substring(selector.Posicion, selector.Longitud);
                if (valorCampoSeleccionado == selector.Valor)
                {
                    return selector.Mensaje;
                }
            }
            throw new MensajeNoIdentificadoException();
        }

        public MessageFromQueue Procesar(MessageFromQueue messageForQueue)
        {
            Mensaje mensaje = this.IdentificarMensaje(messageForQueue);
            MessageData messageDataIn = this.parser.Parse(messageForQueue.Contenido, mensaje);
            var messageDataOut = this.dinamica.Ejecutar(messageDataIn);
            messageForQueue.Contenido = messageDataOut.RawData;
            return messageForQueue;
        }
    }
}