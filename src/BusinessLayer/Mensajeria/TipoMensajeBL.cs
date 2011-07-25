using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class TipoMensajeBL
    {
        public static List<TIPO_MENSAJE> obtenerTipoMensaje()
        {
            return DataAccess.Mensajeria.TipoMensajeDA.obtenerTipoMensaje();
        }

        public static int obtenerCodigoBitmap()
        {
            return (int)EnumTipoMensaje.Bitmap;
        }

        public static int obtenerCodigoXML()
        {
            return (int)EnumTipoMensaje.XML;
        }
    }
}
