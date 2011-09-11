using System;

namespace Excepciones
{
    [Serializable]
    public class SwitchException : Exception
    {

        private EnumSwitchException tipoException;
        public SwitchException() { }

        public SwitchException(EnumSwitchException tipoException) 
            :base(MensajeException(tipoException))
        {
            this.tipoException = tipoException;
        }

        public SwitchException(string message) : base(message) { }

        public SwitchException(string message, Exception inner) : base(message, inner) { }

        protected SwitchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        private  string MensajeException(EnumSwitchException tipoException)
        {
            switch (tipoException)
            {
                case EnumSwitchException.MENSAJE_SIN_CAMPOS_OBLIGATORIOS:
                    return "El mensaje no tiene todos los campos obligatorios";
                case EnumSwitchException.TRAMA_NO_IDENTIFICADA:
                    return "No se ha podido identificar un mensaje para la trama";
                default:
                    return "Error Desconocido";
            }
        }
    }
}
