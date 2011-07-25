using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Mensajeria
{
    public sealed class TipoDatoDA
    {
        public static List<TIPO_DATO> obtenerTipoDato()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TIPO_DATO.MergeOption = MergeOption.NoTracking;
                return contexto.TIPO_DATO.ToList<TIPO_DATO>();
            }
        }
    }
}