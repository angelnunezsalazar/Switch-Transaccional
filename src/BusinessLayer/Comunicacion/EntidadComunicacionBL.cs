using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Comunicacion
{
    [DataObject(true)]
    public class EntidadComunicacionBL
    {
        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacion()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacion();
        }

        public static ENTIDAD_COMUNICACION obtenerEntidadComunicacion(int codigo)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacion(codigo);
        }

        public static EstadoOperacion insertarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.insertarEntidadComunicacion(entidadComunicacion);
        }

        public static EstadoOperacion modificarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.modificarEntidadComunicacion(entidadComunicacion);
        }

        public static EstadoOperacion eliminarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.eliminarEntidadComunicacion(entidadComunicacion);
        }

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionEnGrupoMensaje(int codigoGrupoMensaje)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionEnGrupoMensaje(codigoGrupoMensaje);
        }

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionSinGrupo()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionSinGrupo();
        }

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionSinRelaciones()
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.obtenerEntidadComunicacionSinRelaciones();
        }

        public static EstadoOperacion agregarEntidadAGrupoMensaje(int codigoGrupoMensaje, int codigoEntidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.agregarEntidadAGrupoMensaje(codigoGrupoMensaje, codigoEntidadComunicacion);
        }

        public static EstadoOperacion eliminarEntidadDeGrupoMensaje(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            return DataAccess.Comunicacion.EntidadComunicacionDA.eliminarEntidadDeGrupoMensaje(entidadComunicacion);
        }
    }
}
