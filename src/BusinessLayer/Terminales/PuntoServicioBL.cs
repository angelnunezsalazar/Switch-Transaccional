using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Terminales;

namespace BusinessLayer.Terminales
{
    [DataObject(true)]
    public class PuntoServicioBL
    {
        public static List<PUNTO_SERVICIO> obtenerPuntoServicio(string nombre, string estado)
        {
            return PuntoServicioDA.obtenerPuntoServicio(nombre, estado);
        }

        public static PUNTO_SERVICIO obtenerPuntoServicio(int codigo)
        {
            return PuntoServicioDA.obtenerPuntoServicio(codigo);
        }

        public static List<PUNTO_SERVICIO> obtenerPuntoServicio()
        {
            return PuntoServicioDA.obtenerPuntoServicio();
        }

        public static EstadoOperacion insertarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            return PuntoServicioDA.insertarPuntoServicio(puntoServicio);
        }

        public static EstadoOperacion modificarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            return PuntoServicioDA.modificarPuntoServicio(puntoServicio);
        }

        public static EstadoOperacion eliminarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            return PuntoServicioDA.eliminarPuntoServicio(puntoServicio);
        }
    }
}
