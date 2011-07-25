using System;
using BusinessEntity;
using Mensajeria.Mensajes;

namespace Switch.Transformacion
{
    public abstract class TransformacionCampo
    {
        public abstract String Transformar(TRANSFORMACION_CAMPO entidadTransformacion, Mensaje mensaje);
    }
}