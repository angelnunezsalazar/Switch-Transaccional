using System.Collections.Generic;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;

namespace Mensajeria.Convertidor
{
    public abstract class Convertidor
    {
        protected Convertidor(GRUPO_MENSAJE grupoMensaje)
        {
            this.grupoMensaje = grupoMensaje;
        }

        protected GRUPO_MENSAJE grupoMensaje;

        public List<PASO_DINAMICA> ListaPasos { get; set; }

        public abstract Mensaje Convertir(byte[] trama, Mensajeria mensajeria);
        public abstract byte[] Convertir(Mensaje mensaje);

        public  Convertidor ObtenerConvertidor(GRUPO_MENSAJE grupoMensaje, EnumTipoMensaje tipo)
        {
            switch (tipo)
            {
                case EnumTipoMensaje.Bitmap:
                    return new ConvertidorISO8583(grupoMensaje);
                case EnumTipoMensaje.XML:
                    return new ConvertidorXML(grupoMensaje);
            }
            return null;
        }
    }
}