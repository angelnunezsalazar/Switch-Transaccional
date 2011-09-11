using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace DataAccess.Seguridad
{
    public class TipoLlaveDA
    {
        public static SortedList<int, string> obtenerTipoLlave()
        {
            return Util.GetEnumDataSource<EnumTipoLlave>();
        }
    }
}
