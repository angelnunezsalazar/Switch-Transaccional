using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Comunicacion
{
    [DataObject(true)]
    public class EntidadComunicacionBL
    {
        public static List<EntidadComunicacion> obtenerEntidadComunicacion()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacion();
        }

        public static EntidadComunicacion obtenerEntidadComunicacion(int codigo)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacion(codigo);
        }

        public static EstadoOperacion insertarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.insertarEntidadComunicacion(entidadComunicacion);
        }

        public static EstadoOperacion modificarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.modificarEntidadComunicacion(entidadComunicacion);
        }

        public static EstadoOperacion eliminarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.eliminarEntidadComunicacion(entidadComunicacion);
        }

        public static List<EntidadComunicacion> obtenerEntidadComunicacionEnGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionEnGrupoMensaje(codigoGrupoMensaje);
        }

        public static List<EntidadComunicacion> obtenerEntidadComunicacionSinGrupo()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionSinGrupo();
        }

        public static List<EntidadComunicacion> obtenerEntidadComunicacionSinRelaciones()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionSinRelaciones();
        }

        public static EstadoOperacion agregarEntidadAGrupoMensaje(int codigoGrupoMensaje, int codigoEntidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.agregarEntidadAGrupoMensaje(codigoGrupoMensaje, codigoEntidadComunicacion);
        }

        public static EstadoOperacion eliminarEntidadDeGrupoMensaje(EntidadComunicacion entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.eliminarEntidadDeGrupoMensaje(entidadComunicacion);
        }
    }
}
