using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Utilidades;

namespace Switch.Transformacion.TipoTransformacion
{
    public class FuncionalidadEstandar : TransformacionCampo
    {

        public override string Transformar(TRANSFORMACION_CAMPO entidadTransformacion, Mensaje mensaje)
        {
            EnumFuncionalidadEstandar funcionalidadEstandar = (EnumFuncionalidadEstandar)
                                Enum.ToObject(typeof(EnumFuncionalidadEstandar), entidadTransformacion.TCM_FUNCIONALIDAD_ESTANDAR.Value);
            switch (funcionalidadEstandar)
            {
                case EnumFuncionalidadEstandar.Fecha_DDMMYYYY:
                    return SystemTime.Now().ToString("ddMMyyyy");
                case EnumFuncionalidadEstandar.Fecha_HHMM:
                    return SystemTime.Now().ToString("HHmm");
                case EnumFuncionalidadEstandar.Llave_Maestra:
                    throw new NotImplementedException("No se implementa funcionalidad por Llave Maestra");
                default:
                    throw new NotImplementedException("Se ha producido un error");
            }

        }
    }
}
