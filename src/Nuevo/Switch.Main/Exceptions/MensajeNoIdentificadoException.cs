namespace Swich.Main.Exceptions
{
    using System;

    public class MensajeNoIdentificadoException : Exception
    {
        public MensajeNoIdentificadoException()
            : base("No se ha podido identificar el mensaje dado los valores selectores")
        {

        }
    }
}