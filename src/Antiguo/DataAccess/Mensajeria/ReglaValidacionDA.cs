using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;

namespace DataAccess.Mensajeria
{
    public class ReglaValidacionDA
    {

        public static List<VALIDACION_CAMPO> obtenerReglaValidacion(int grupoValidacion, int campo)
        {
            using (Switch contexto = new Switch())
            {
                contexto.VALIDACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.VALIDACION_CAMPO.Include("CAMPO").Include("CAMPO.MENSAJE").Include("CAMPO.MENSAJE.GRUPO_MENSAJE").Include("GRUPO_VALIDACION").Where(o => o.GRUPO_VALIDACION.GRV_CODIGO == grupoValidacion && o.CAMPO.CAM_CODIGO == campo).Distinct().ToList<VALIDACION_CAMPO>();
            }
        }
    }
}
