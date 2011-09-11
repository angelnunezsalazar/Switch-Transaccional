using System.Collections.Generic;
using BusinessEntity;
using DataAccess.Operacion;

namespace BusinessLayer.Operacion
{
    public class TanqueoBL
    {
        public static EstadoOperacion modificarCamposTranqueo(int idMensaje, IList<int> idCampoTanqueo, IList<int> idCampoDestanqueo)
        {
            return TanqueoDA.modificarCamposTranqueo(idMensaje, idCampoTanqueo, idCampoDestanqueo);
        }
    }
}
