using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;

namespace Mensajeria.Mensajes
{
    public class MensajeXML : Mensaje
    {
        internal MensajeXML(EnumTipoMensaje tipo) : base(tipo) { }

        protected override void AgregarCuerpoMensaje(CAMPO campoMensaje, Valor valorCabecera, Valor valorCuerpo)
        {
            EnumTipoDatoCampo tipoDato = (EnumTipoDatoCampo)
                        Enum.ToObject(typeof(EnumTipoDatoCampo), campoMensaje.TIPO_DATO.TDT_CODIGO);

            Campo campo = new Campo(campoMensaje.CAM_CODIGO, campoMensaje.CAM_CABECERA, campoMensaje.CAM_REQUERIDO, campoMensaje.CAM_POSICION_RELATIVA
                , campoMensaje.CAM_NOMBRE, false, 0, null, campoMensaje.CAM_LONGITUD, valorCuerpo, tipoDato
                , campoMensaje.CAM_TANQUEO.HasValue ? campoMensaje.CAM_TANQUEO.Value : false);

            Campos.Add(campo);
        }

        protected override void AgregarCabeceraMensaje(CAMPO_PLANTILLA campoPlantilla, Valor valorCabecera, Valor valorCuerpo)
        {
            EnumTipoDatoCampo tipoDato = (EnumTipoDatoCampo)
            Enum.ToObject(typeof(EnumTipoDatoCampo), campoPlantilla.TIPO_DATO.TDT_CODIGO);

            Campo campo = new Campo(campoPlantilla.CMP_CODIGO, campoPlantilla.CMP_CABECERA, true, campoPlantilla.CMP_POSICION_RELATIVA
                , campoPlantilla.CMP_NOMBRE, false, 0, null, campoPlantilla.CMP_LONGITUD, valorCuerpo, tipoDato, false);

            Campos.Add(campo);
        }
    }
}
