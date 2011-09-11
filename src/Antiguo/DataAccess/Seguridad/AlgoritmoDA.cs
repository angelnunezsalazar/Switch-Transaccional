using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace DataAccess.Seguridad
{
    public class AlgoritmoDA
    {
        public static SortedList<int, string> obtenerTipoLlave()
        {
            return Util.GetEnumDataSource<EnumAlgoritmo>();
        }
    }
}
