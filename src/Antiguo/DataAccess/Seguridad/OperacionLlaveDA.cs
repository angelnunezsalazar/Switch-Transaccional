using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace DataAccess.Seguridad
{
    public class OperacionLlaveDA
    {
        public static SortedList<int, string> obtenerOperacionLlave()
        {
            return Util.GetEnumDataSource<EnumOperacionLlave>();
        }
    }
}
