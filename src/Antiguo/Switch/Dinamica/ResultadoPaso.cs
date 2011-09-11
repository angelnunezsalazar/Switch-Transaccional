using System;
using Mensajeria.Mensajes;

namespace Switch.Dinamica
{
    public class ResultadoPaso
    {
        public Mensaje Mensaje { get; private set; }

        public EnumResultadoPaso Tipo { get; private set; }

        public ResultadoPaso(Mensaje mensaje, EnumResultadoPaso tipo)
        {
            Mensaje = mensaje;
            Tipo = tipo;
        }
    }
}