using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Mensajeria;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public class GrupoValidacionBL
    {
        public static GRUPO_VALIDACION obtenerGrupoValidacionPorCodigo(int codigo)
        {
            return GrupoValidacionDA.obtenerGrupoValidacionPorCodigo(codigo);
        }
        
        public static List<GRUPO_VALIDACION> obtenerGrupoValidacion(int mensaje)
        {
            return GrupoValidacionDA.obtenerGrupoValidacion(mensaje);
        }

        public static List<GRUPO_VALIDACION> obtenerGrupoValidacionSinRelaciones(int mensaje)
        {
            return GrupoValidacionDA.obtenerGrupoValidacionSinRelaciones(mensaje);
        }

        public static EstadoOperacion modificarGrupoValidacion(GRUPO_VALIDACION grupo)
        {
            return GrupoValidacionDA.modificarGrupoValidacion(grupo);
        }

        public static EstadoOperacion insertarGrupoValidacion(GRUPO_VALIDACION grupo)
        {
            return GrupoValidacionDA.insertarGrupoValidacion(grupo);
        }

        public static EstadoOperacion insertarGrupoValidacion(GRUPO_VALIDACION grupo, int codMensaje)
        {
            MENSAJE mensaje = MensajeDA.obtenerMensaje(codMensaje);
            grupo.MENSAJE = mensaje;
            
            return GrupoValidacionDA.insertarGrupoValidacion(grupo);
        }

        public static EstadoOperacion eliminarGrupoValidacion(GRUPO_VALIDACION grupo)
        {
            return GrupoValidacionDA.eliminarGrupoValidacion(grupo);
        }
    }
}
