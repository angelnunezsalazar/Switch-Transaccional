using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Mensajeria
{
    public sealed class TipoMensajeDA
    {
        public static List<TIPO_MENSAJE> obtenerTipoMensaje()
        {
            using (Switch contexto = new Switch())
            {
                contexto.TIPO_MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.TIPO_MENSAJE.ToList<TIPO_MENSAJE>();
            }
        }
    }
}
