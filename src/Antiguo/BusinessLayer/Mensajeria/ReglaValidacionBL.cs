using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Mensajeria;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public class ReglaValidacionBL
    {
        public static List<VALIDACION_CAMPO> obtenerReglaValidacion(int grupoValidacion, int campo)
        {
            return ReglaValidacionDA.obtenerReglaValidacion(grupoValidacion, campo);
        }


    }
}
