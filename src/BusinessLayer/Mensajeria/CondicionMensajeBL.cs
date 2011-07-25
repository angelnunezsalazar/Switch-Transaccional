using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public class CondicionMensajeBL
    {
        public static List<CONDICION_MENSAJE> obtenerCondicionMensaje()
        {
            return DataAccess.Mensajeria.CondicionMensajeDA.obtenerCondicionMensaje();
        }
    }
}
