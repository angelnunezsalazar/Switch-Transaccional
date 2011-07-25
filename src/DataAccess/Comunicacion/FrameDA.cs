using System.Collections.Generic;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace DataAccess.Comunicacion
{
    public class FrameDA
    {
        public static SortedList<int, string> obtenerFrame()
        {
            return Util.GetEnumDataSource<EnumFrame>();
        }
    }
}
