using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Seguridad
{
    [DataObject(true)]
    public class TipoLlaveBL
    {
        public static SortedList<int, string> obtenerTipoLlave()
        {
            return DataAccess.Seguridad.TipoLlaveDA.obtenerTipoLlave();
        }

        public static int obtenerCodigoCampo()
        {
            return (int)EnumTipoLlave.Campo;
        }

        public static int obtenerCodigoWorkingKey()
        {
            return (int)EnumTipoLlave.WorkingKey;
        }

        public static int obtenerCodigoLlaveFija()
        {
            return (int)EnumTipoLlave.LlaveFija;
        }

        public static string obtenerTipoLlave(int codigoTipoLlave)
        {
            string retornar=Enum.GetName(typeof(EnumTipoLlave), codigoTipoLlave);
            return retornar;
        }
    }
}
