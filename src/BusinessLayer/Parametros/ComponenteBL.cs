using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Parametros;


namespace BusinessLayer.Parametros
{
    [DataObject(true)]
    public class ComponenteBL
    {
        public static List<COMPONENTE> obtenerComponente()
        {
            return ComponenteDA.obtenerComponente();
        }

        public static EstadoOperacion insertarComponente(COMPONENTE componente)
        {
            return ComponenteDA.insertarComponente(componente);
        }

        public static EstadoOperacion modificarComponente(COMPONENTE componente)
        {
            return ComponenteDA.modificarComponente(componente);
        }

        public static EstadoOperacion eliminarComponente(COMPONENTE componente)
        {
            return ComponenteDA.eliminarComponente(componente);
        }
    }
}
