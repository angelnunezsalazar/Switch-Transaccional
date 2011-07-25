using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class ReglaMensajeTransaccionalBL
    {
        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerMensajeTransaccional()
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.obtenerMensajeTransaccional();
        }

        public static REGLA_MENSAJE_TRANSACCIONAL obtenerMensajeTransaccional(int codigoReglaMensajeTransaccional)
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.obtenerMensajeTransaccional(codigoReglaMensajeTransaccional);
        }

        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerReglaMensajeTransaccionalPorMensajeTransaccional(int codigoMensajeTransaccional)
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.obtenerReglaMensajeTransaccionalPorMensajeTransaccional(codigoMensajeTransaccional);
        }

        public static EstadoOperacion insertarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.insertarReglaMensajeTransaccional(ReglaMensajeTransaccional);
        }

        public static EstadoOperacion modificarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.modificarReglaMensajeTransaccional(ReglaMensajeTransaccional);
        }


        public static EstadoOperacion eliminarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            return DataAccess.Mensajeria.ReglaMensajeTransaccionalDA.eliminarReglaMensajeTransaccional(ReglaMensajeTransaccional);
        }
    }
}
