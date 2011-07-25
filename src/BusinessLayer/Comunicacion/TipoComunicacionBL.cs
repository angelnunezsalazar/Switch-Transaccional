using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Comunicacion;
using DataAccess.Enumeracion.EnumTablasBD;

namespace BusinessLayer.Comunicacion
{
    [DataObject(true)]
    public class TipoComunicacionBL
    {
        public static List<TIPO_COMUNICACION> obtenerTipoComunicacion()
        {
            return TipoComunicacionDA.obtenerTipoComunicacion();
        }

        public static int obtenerCodigoTCP()
        {
            return (int)EnumTipoComunicacion.TCP;
        }

        public static int obtenerCodigoComponente()
        {
            return (int)EnumTipoComunicacion.Componente;
        }
    }
}
