namespace Swich.Main.Identificadores
{
    using System.Collections.Generic;

    using Swich.Main.Core;
    using Swich.Main.Exceptions;
    using Swich.Main.Queue;

    //TODO: Pasar únicamente el RawData

    public class IdentificadorMensaje
    {
        public Mensaje Identificar(MessageQueued messageQueued, IEnumerable<ValorSelector> valoresSelectores)
        {
            foreach (var selector in valoresSelectores)
            {
                var valorCampoSeleccionado = messageQueued.RawData.Substring(selector.Posicion, selector.Longitud);
                if (valorCampoSeleccionado == selector.Valor)
                {
                    return selector.Mensaje;
                }
            }
            throw new MensajeNoIdentificadoException();
        }
    }
}