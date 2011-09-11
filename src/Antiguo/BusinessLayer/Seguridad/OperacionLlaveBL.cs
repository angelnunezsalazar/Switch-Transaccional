using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Seguridad
{
    [DataObject(true)]
    public class OperacionLlaveBL
    {
        public static SortedList<int, string> obtenerOperacionLlave()
        {
            return DataAccess.Seguridad.OperacionLlaveDA.obtenerOperacionLlave();
        }

        public static string obtenerOperacionLlave(int codigoOperacionLlave)
        {
            string retornar = Enum.GetName(typeof(EnumOperacionLlave), codigoOperacionLlave);
            return retornar;
        }
    }
}
