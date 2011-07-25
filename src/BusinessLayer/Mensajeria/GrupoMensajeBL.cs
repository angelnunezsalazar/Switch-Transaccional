using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{

        [DataObject(true)]
        public class GrupoMensajeBL
        {
            public static List<GRUPO_MENSAJE> obtenerGrupoMensaje()
            {
                return DataAccess.Mensajeria.GrupoMensajeDA.obtenerGrupoMensaje();
            }

            public static GRUPO_MENSAJE obtenerGrupoMensaje(int codigo)
            {
                return DataAccess.Mensajeria.GrupoMensajeDA.obtenerGrupoMensaje(codigo);
            }

            public static EstadoOperacion insertarGrupoMensaje(GRUPO_MENSAJE grupoMensaje)
            {
                return DataAccess.Mensajeria.GrupoMensajeDA.insertarGrupoMensaje(grupoMensaje);
            }

            public static EstadoOperacion modificarGrupoMensaje(GRUPO_MENSAJE grupoMensaje)
            {
                return DataAccess.Mensajeria.GrupoMensajeDA.modificarGrupoMensaje(grupoMensaje);
            }

            public static EstadoOperacion eliminarGrupoMensaje(GRUPO_MENSAJE grupoMensaje)
            {
                return DataAccess.Mensajeria.GrupoMensajeDA.eliminarGrupoMensaje(grupoMensaje);
            }
        }
}
