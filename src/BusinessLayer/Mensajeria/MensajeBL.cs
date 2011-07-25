using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public class MensajeBL
    {
        public static List<MENSAJE> obtenerMensaje()
        {
            return DataAccess.Mensajeria.MensajeDA.obtenerMensaje();
        }

        public static MENSAJE obtenerMensaje(int codigo)
        {
            return DataAccess.Mensajeria.MensajeDA.obtenerMensaje(codigo);
        }

        public static List<MENSAJE> obtenerMensajePorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Mensajeria.MensajeDA.obtenerMensajePorCodigoGrupoMensaje(codigoGrupoMensaje);
        }

        public static List<MENSAJE> obtenerMensajePorCodigoGrupoMensajeTodosEnCasoContrario(string codigoGrupoMensaje)
        {
            return DataAccess.Mensajeria.MensajeDA.obtenerMensajePorCodigoGrupoMensajeTodosEnCasoContrario(codigoGrupoMensaje);
        }

        public static EstadoOperacion insertarMensaje(MENSAJE Mensaje)
        {
            return DataAccess.Mensajeria.MensajeDA.insertarMensaje(Mensaje);
        }

        public static EstadoOperacion modificarMensaje(MENSAJE Mensaje)
        {
            return DataAccess.Mensajeria.MensajeDA.modificarMensaje(Mensaje);
        }

        public static EstadoOperacion eliminarMensaje(MENSAJE Mensaje)
        {
            return DataAccess.Mensajeria.MensajeDA.eliminarMensaje(Mensaje);
        }
    }
}
