using System;
using DataAccess.Enumeracion.EnumTablasBD;

namespace Mensajeria.Mensajes
{
    public class MensajeISO8583 : Mensaje
    {
        internal MensajeISO8583(EnumTipoMensaje tipo) : base(tipo) { }

        private int PosicionBitmap { get; set; }

        public byte[] Bitmap
        {
            get
            {
                return (byte[])this.Campos[PosicionBitmap].ValorCuerpo.Dato;
            }
        }

        protected override void AgregarCuerpoMensaje(BusinessEntity.CAMPO campoMensaje, Valor valorCabecera, Valor valorCuerpo)
        {
            EnumTipoDatoCampo tipoDato = (EnumTipoDatoCampo)
                        Enum.ToObject(typeof(EnumTipoDatoCampo), campoMensaje.TIPO_DATO.TDT_CODIGO);

            Campo campo = new Campo(campoMensaje.CAM_CODIGO, campoMensaje.CAM_CABECERA, campoMensaje.CAM_REQUERIDO,
                                    campoMensaje.CAM_POSICION_RELATIVA
                                    , campoMensaje.CAM_NOMBRE, campoMensaje.CAM_VARIABLE.Value ? campoMensaje.CAM_VARIABLE.Value : false, campoMensaje.CAM_LONGITUD_CABECERA
                                    , valorCabecera, campoMensaje.CAM_LONGITUD, valorCuerpo, tipoDato
                                    , campoMensaje.CAM_TANQUEO.HasValue ? campoMensaje.CAM_TANQUEO.Value : false);

            Campos.Add(campo);
        }

        protected override void AgregarCabeceraMensaje(BusinessEntity.CAMPO_PLANTILLA campoPlantilla, Valor valorCabecera, Valor valorCuerpo)
        {
            EnumTipoDatoCampo tipoDato = (EnumTipoDatoCampo)
                        Enum.ToObject(typeof(EnumTipoDatoCampo), campoPlantilla.TIPO_DATO.TDT_CODIGO);

            Campo campo = new Campo(campoPlantilla.CMP_CODIGO, campoPlantilla.CMP_CABECERA, true, null
                , campoPlantilla.CMP_NOMBRE, campoPlantilla.CMP_VARIABLE, campoPlantilla.CMP_LONGITUD_CABECERA, valorCabecera
                , campoPlantilla.CMP_LONGITUD, valorCuerpo, tipoDato, false);

            Campos.Add(campo);

            if (campoPlantilla.CMP_BITMAP)
            {
                this.PosicionBitmap = Campos.Count - 1;
            }
        }

    }
}
