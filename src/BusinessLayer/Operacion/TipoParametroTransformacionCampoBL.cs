using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Operacion;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public class TipoParametroTransformacionCampoBL
    {
        public SortedList<int, string> obtenerTipoTransformacionCampo()
        {
            return TipoParametroTransformacionCampoDA.obtenerTipoTransformacionCampo();
        }

        public static int obtenerCodigoCampoOrigen()
        {
            return (int)EnumTipoParametroTransformacionCampo.CampoOrigen;
        }

        public static int obtenerCodigoTabla()
        {
            return (int)EnumTipoParametroTransformacionCampo.Tabla;
        }

        public static string obtenerNombrePorCodigo(int codigo)
        {
            string nombre= System.Enum.GetName(typeof(EnumTipoParametroTransformacionCampo), codigo);
            return nombre;
        }
    }
}
