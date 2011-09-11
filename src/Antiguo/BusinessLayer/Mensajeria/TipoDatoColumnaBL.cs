using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class TipoDatoColumnaBL
    {
        public static List<TIPO_DATO_COLUMNA> obtenerTipoDatoColumna()
        {
            return DataAccess.Mensajeria.TipoDatoColumnaDA.obtenerTipoDatoColumna();
        }
    }
}