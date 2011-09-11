using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public sealed class TransformacionMensajeBL
    {
        public static List<TRANSFORMACION> obtenerTransformacion(string nombreTransformada,
                                                  int codigoGrupoOrigen,
                                                  int codigoMensajeOrigen,
                                                  int codigoGrupoDestino,
                                                  int codigoMensajeDestino)
        {

            return DataAccess.Operacion.TransformacionMensajeDA.obtenerTransformacion(nombreTransformada, codigoGrupoOrigen, codigoMensajeOrigen, codigoGrupoDestino, codigoMensajeDestino);
        }

        public static TRANSFORMACION obtenerTransformacion(int codigoTransformacion)
        {
            return DataAccess.Operacion.TransformacionMensajeDA.obtenerTransformacion(codigoTransformacion);
        }
        public static List<TRANSFORMACION> obtenerTransformacionSinRelacionesPorMensajeOrigen(int codigoMensajeOrigen)
        {
            return DataAccess.Operacion.TransformacionMensajeDA.obtenerTransformacionSinRelacionesPorMensajeOrigen(codigoMensajeOrigen);
        }
        public static EstadoOperacion insertarTransformacion(TRANSFORMACION Transformacion)
        {
            return DataAccess.Operacion.TransformacionMensajeDA.insertarTransformacion(Transformacion);
        }

        public static EstadoOperacion modificarTransformacion(TRANSFORMACION Transformacion)
        {
            return DataAccess.Operacion.TransformacionMensajeDA.modificarTransformacion(Transformacion);
        }

        public static EstadoOperacion eliminarTransformacion(TRANSFORMACION Transformacion)
        {
            return DataAccess.Operacion.TransformacionMensajeDA.eliminarTransformacion(Transformacion);
        }

    }
}
