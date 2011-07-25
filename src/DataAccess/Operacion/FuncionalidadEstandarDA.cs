using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace DataAccess.Operacion
{
    public sealed class FuncionalidadEstandarDA
    {
        public static SortedList<int, string> obtenerFuncionalidadEstandar()
        {
            return Util.GetEnumDataSource<EnumFuncionalidadEstandar>();
        }
    }
}
