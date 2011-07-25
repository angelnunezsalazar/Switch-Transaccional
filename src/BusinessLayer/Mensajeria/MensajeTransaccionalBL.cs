using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class MensajeTransaccionalBL
    {
        public static List<MENSAJE_TRANSACCIONAL> obtenerMensajeTransaccional()
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.obtenerMensajeTransaccional();
        }

        public static MENSAJE_TRANSACCIONAL obtenerMensajeTransaccional(int codigoMensajeTransaccional)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.obtenerMensajeTransaccional(codigoMensajeTransaccional);
        }

        public static List<MENSAJE_TRANSACCIONAL> obtenerMensajeTransaccionalPorMensaje(int codigoMensaje)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.obtenerMensajeTransaccionalPorMensaje(codigoMensaje);
        }
        public static MENSAJE_TRANSACCIONAL obtenerMensajeTransaccionalSinMensaje(int codigoMensajeTransaccional)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.obtenerMensajeTransaccionalSinMensaje(codigoMensajeTransaccional);
        }
        public static EstadoOperacion insertarMensajeTransaccional(MENSAJE_TRANSACCIONAL MensajeTransaccional)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.insertarMensajeTransaccional(MensajeTransaccional);
        }

        public static EstadoOperacion modificarMensajeTransaccional(MENSAJE_TRANSACCIONAL MensajeTransaccional)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.modificarMensajeTransaccional(MensajeTransaccional);
        }

        public static EstadoOperacion eliminarMensajeTransaccional(MENSAJE_TRANSACCIONAL MensajeTransaccional)
        {
            return DataAccess.Mensajeria.MensajeTransaccionalDA.eliminarMensajeTransaccional(MensajeTransaccional);
        }
    }
}
