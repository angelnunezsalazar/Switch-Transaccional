using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Seguridad
{
    [DataObject(true)]
    public class AlgoritmoBL
    {
        public static SortedList<int, string> obtenerTipoLlave()
        {
            return DataAccess.Seguridad.AlgoritmoDA.obtenerTipoLlave();
        }

        public static string obtenerAlgoritmo(int codigoalgoritmo)
        {
            return Enum.GetName(typeof(EnumAlgoritmo), codigoalgoritmo);
        }
    }
}
