using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Operacion;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public sealed class FuncionalidadEstandarBL
    {
        public static SortedList<int, string> obtenerFuncionalidadEstandar()
        {
            return FuncionalidadEstandarDA.obtenerFuncionalidadEstandar();
        }
    }
}
