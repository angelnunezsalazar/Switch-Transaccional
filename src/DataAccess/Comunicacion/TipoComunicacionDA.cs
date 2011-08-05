using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Comunicacion
{
    public class TipoComunicacionDA
    {
        public static List<TIPO_COMUNICACION> obtenerTipoComunicacion()
        {
            using (Switch contexto = new Switch())
            {
                contexto.TIPO_COMUNICACION.MergeOption = MergeOption.NoTracking;
                return contexto.TIPO_COMUNICACION.OrderBy(o=>o.TPO_CODIGO).ToList<TIPO_COMUNICACION>();
            }
        }
    }
}
