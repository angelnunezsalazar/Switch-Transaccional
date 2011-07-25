using System;
using BusinessEntity;
using Mensajeria.Mensajes;

namespace Switch.Transformacion.TipoTransformacion
{
    public class ValorConstante:TransformacionCampo
    {
        public override String Transformar(TRANSFORMACION_CAMPO entidadTransformacion, Mensaje mensaje)
        {
            return entidadTransformacion.TCM_VALOR_CONSTANTE;
        }
    }
}
