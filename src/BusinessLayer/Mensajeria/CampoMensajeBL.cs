using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class CampoMensajeBL
    {
        public static List<CAMPO> obtenerCampo()
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampo();
        }

        public static List<CAMPO> obtenerCampoCabecera(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoCabecera(codigoMensaje);
        }

        public static List<CAMPO> obtenerCampoCuerpo(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoCuerpo(codigoMensaje);
        }

        public static List<CAMPO> obtenerCampo(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampo(codigoMensaje);
        }

        public static CAMPO obtenerCampo(int codigoMensaje, int codigoCampo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampo(codigoMensaje, codigoCampo);
        }

        public static List<CAMPO> obtenerCampoSelector(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoSelector(codigoMensaje);
        }

        public static List<CAMPO> obtenerCampoOrigenPorTransaccion(int codigoTransaccion)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoOrigenPorTransaccion(codigoTransaccion);
        }

        public static List<CAMPO> obtenerCampoNoSelector(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoNoSelector(codigoMensaje);
        }

        public static List<CAMPO> obtenerCampoNoSelectorNoAsignadoReglaTransaccional(int codigoMensaje)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.obtenerCampoNoSelectorNoAsignadoReglaTransaccional(codigoMensaje);
        }

        public static EstadoOperacion actualizarValorSelector(CAMPO Campo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.actualizarValorSelector(Campo);
        }

        public static EstadoOperacion insertarCampoPorCampoPlantilla(CAMPO Campo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.insertarCampoPorCampoPlantilla(Campo);
        }

        public static EstadoOperacion insertarCampo(CAMPO Campo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.insertarCampo(Campo);
        }

        public static EstadoOperacion modificarCampo(CAMPO Campo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.modificarCampo(Campo);
        }


        public static EstadoOperacion eliminarCampo(CAMPO Campo)
        {
            return DataAccess.Mensajeria.CampoMensajeDA.eliminarCampo(Campo);
        }
    }
}
