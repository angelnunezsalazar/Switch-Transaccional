using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;


namespace DataAccess.Operacion
{
    public class TipoParametroTransformacionCampoDA
    {
        public static SortedList<int, string> obtenerTipoTransformacionCampo()
        {
            return Util.GetEnumDataSource<EnumTipoParametroTransformacionCampo>();
        }
    }
}
