namespace Comunicacion
{
    using System;

    public class ClienteConectadoNoReconocidoException : Exception
    {
        public ClienteConectadoNoReconocidoException(string clientKey)
            : base("No se ha obtenido ningún cliente para el ClientKey:" + clientKey)
        {

        }
    }
}