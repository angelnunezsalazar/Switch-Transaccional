using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Mensajeria
{
    public sealed class CondicionMensajeDA
    {
        public static List<CONDICION_MENSAJE> obtenerCondicionMensaje()
        {
            using (Switch contexto = new Switch())
            {
                contexto.CONDICION_MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.CONDICION_MENSAJE.ToList<CONDICION_MENSAJE>();
            }
        }
    }
}
