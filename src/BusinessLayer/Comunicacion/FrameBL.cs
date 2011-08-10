using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Comunicacion
{
    using DataAccess.Utilitarios;

    [DataObject(true)]
    public class FrameBL
    {
        public static SortedList<int, string> obtenerFrame()
        {
            return Util.GetEnumDataSource<EnumFrame>();
        }

        public static int obtenerCodigoCabecera4Bytes()
        {
            return (int)EnumFrame.Cabecera_4_Bytes;
        }

        public static int obtenerCodigoDelimitadores()
        {
            return (int)EnumFrame.Delimitadores;
        }
    }
}
