using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public sealed class TransformacionCampoBL
    {
        public static TRANSFORMACION_CAMPO obtenerTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.obtenerTransformacionCampo(codigoTransformacion, codigoMensaje, codigoCampo);
        }

        public static int obtenerCantidadTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.obtenerCantidadTransformacionCampo(codigoTransformacion, codigoMensaje, codigoCampo);
        }

        public static TRANSFORMACION_CAMPO obtenerTransformacionCampoConCampoDestino(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.obtenerTransformacionCampoConCampoDestino(codigoTransformacion, codigoMensaje, codigoCampo);
        }

        public static EstadoOperacion insertarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.insertarTransformacionCampo(transformacionCampo);
        }

        public static EstadoOperacion modificarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.modificarTransformacionCampo(transformacionCampo);
        }
        public static EstadoOperacion eliminarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            return DataAccess.Operacion.TransformacionCampoDA.eliminarTransformacionCampo(transformacionCampo);
        }
    }
}
