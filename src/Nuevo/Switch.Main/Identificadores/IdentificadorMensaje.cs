namespace Swich.Main.Identificadores
{
    using System.Collections.Generic;

    using Swich.Main.Core;
    using Swich.Main.Exceptions;

    public interface IIdentificadorMensaje
    {
        Mensaje Identificar(string rawMessage, IEnumerable<ValorSelector> valoresSelectores);
    }

    public class IdentificadorMensaje : IIdentificadorMensaje
    {
        public Mensaje Identificar(string rawMessage, IEnumerable<ValorSelector> valoresSelectores)
        {
            foreach (var selector in valoresSelectores)
            {
                var valorCampoSeleccionado = rawMessage.Substring(selector.Posicion, selector.Longitud);
                if (valorCampoSeleccionado == selector.Valor)
                {
                    return selector.Mensaje;
                }
            }
            throw new MensajeNoIdentificadoException();
        }
    }
}