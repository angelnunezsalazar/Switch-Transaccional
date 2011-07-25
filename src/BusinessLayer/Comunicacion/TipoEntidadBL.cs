using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Comunicacion
{
    [DataObject(true)]
    public class TipoEntidadBL
    {
        public static List<TIPO_ENTIDAD> obtenerTipoTerminal()
        {
            return DataAccess.Comunicacion.TipoEntidadDA.obtenerTipoTerminal();
        }
    }
}
