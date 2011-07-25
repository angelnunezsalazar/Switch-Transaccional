using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Comunicacion
{
    public class TipoEntidadDA
    {
        public static List<TIPO_ENTIDAD> obtenerTipoTerminal()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TIPO_ENTIDAD.MergeOption = MergeOption.NoTracking;
                return contexto.TIPO_ENTIDAD.ToList<TIPO_ENTIDAD>();
            }
        }
    }
}
