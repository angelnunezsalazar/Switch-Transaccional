using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Mensajeria
{
    public sealed class TipoDatoColumnaDA
    {
        public static List<TIPO_DATO_COLUMNA> obtenerTipoDatoColumna()
        {
            using (Switch contexto = new Switch())
            {
                contexto.TIPO_DATO_COLUMNA.MergeOption = MergeOption.NoTracking;
                return contexto.TIPO_DATO_COLUMNA.ToList<TIPO_DATO_COLUMNA>();
            }
        }
    }
}