using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class TipoDatoBL
    {
        public static List<TIPO_DATO> obtenerTipoDato()
        {
            return DataAccess.Mensajeria.TipoDatoDA.obtenerTipoDato();
        }
    }
}
