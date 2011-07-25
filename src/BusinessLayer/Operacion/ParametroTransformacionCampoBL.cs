using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Mensajeria;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public class ParametroTransformacionCampoBL
    {
        public static List<PARAMETRO_TRANSFORMACION_CAMPO> obtenerParametroTransformacionCampo()
        {
            return ParametroTransformacionCampoDA.obtenerParametroTransformacionCampo();
        }

        public static PARAMETRO_TRANSFORMACION_CAMPO obtenerParametroTransformacionCampo(int codigoParametro)
        {
            return ParametroTransformacionCampoDA.obtenerParametroTransformacionCampo(codigoParametro);
        }

        public static List<PARAMETRO_TRANSFORMACION_CAMPO> obtenerParametroTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            return ParametroTransformacionCampoDA.obtenerParametroTransformacionCampo(codigoTransformacion, codigoMensaje, codigoCampo);
        }
        public static EstadoOperacion insertarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            return ParametroTransformacionCampoDA.insertarParametroTransformacionCampo(parametroTransformacionCampo);
        }

        public static EstadoOperacion modificarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            return ParametroTransformacionCampoDA.modificarParametroTransformacionCampo(parametroTransformacionCampo);
        }

        public static EstadoOperacion eliminarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            return ParametroTransformacionCampoDA.eliminarParametroTransformacionCampo(parametroTransformacionCampo);
        }
    }
}
