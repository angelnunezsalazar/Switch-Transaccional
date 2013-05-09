namespace Swich.Main.Exceptions
{
    using System;

    public class TransaccionalNoIdentificadoException : Exception
    {
        public TransaccionalNoIdentificadoException()
            : base("No se ha podido identificar el mensaje transaccional")
        {

        }
    }
}