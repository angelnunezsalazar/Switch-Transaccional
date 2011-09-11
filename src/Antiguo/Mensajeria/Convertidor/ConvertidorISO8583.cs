using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Excepciones;
using Mensajeria.Mensajes;
using Utilidades;

namespace Mensajeria.Convertidor
{
    public class ConvertidorISO8583 : Convertidor
    {

        public ConvertidorISO8583(GRUPO_MENSAJE grupoMensaje)
            : base(grupoMensaje)
        {

        }

        public override Mensaje Convertir(byte[] trama, Mensajeria mensajeria)
        {
            MensajeISO8583 mensajeTransformado = (MensajeISO8583) Mensaje.CrearMensaje(EnumTipoMensaje.Bitmap);

            List<CAMPO_PLANTILLA> listaCamposCabecera = mensajeria.ObtenerCamposCabecera(this.grupoMensaje.GMJ_CODIGO);

            List<Valor> listaValoresSelectores = new List<Valor>();
            List<string> listaTransaccionales = new List<string>();
            StringBuilder transaccionales = new StringBuilder("");

            int posicionActual = 0;

            foreach (CAMPO_PLANTILLA campoCabecera in listaCamposCabecera)
            {
                int longitud = campoCabecera.CMP_LONGITUD;

                EnumTipoDatoCampo enumTipoDato = (EnumTipoDatoCampo)
                    Enum.ToObject(typeof(EnumTipoDatoCampo), campoCabecera.TIPO_DATO.TDT_CODIGO);

                Valor valorTrama = Codificador.Valor(enumTipoDato, trama, posicionActual, longitud);

                if (campoCabecera.CMP_SELECTOR)
                {
                    listaValoresSelectores.Add(valorTrama);
                }

                if (campoCabecera.CMP_TRANSACCIONAL)
                {
                    AgregarTransaccional(valorTrama, listaTransaccionales, transaccionales);
                }

                mensajeTransformado.AgregarCabecera(campoCabecera, null, valorTrama);
                posicionActual += longitud;
            }

            MENSAJE mensajeIdentificado = mensajeria.IdentificarMensajePorValorSelector(grupoMensaje.GMJ_CODIGO, listaValoresSelectores);

            if (mensajeIdentificado == null)
            {
                throw new SwitchException(EnumSwitchException.TRAMA_NO_IDENTIFICADA);
            }

            List<CAMPO> listaCamposCuerpo = (from c in mensajeIdentificado.CAMPO
                                             where c.CAM_CABECERA == false
                                             orderby c.CAM_POSICION_RELATIVA ascending
                                             select c).ToList();

            bool[] boolBitmap = ConvertidorUtil.toBoolArray(mensajeTransformado.Bitmap);

            int contListaCampos = 0;
            int contTrama = posicionActual;
            for (int i = 0; i < boolBitmap.Length; i++)
            {
                if (boolBitmap[i])
                {
                    CAMPO campo = listaCamposCuerpo.Where(c=>c.CAM_POSICION_RELATIVA==i+1).FirstOrDefault();

                    EnumTipoDatoCampo enumTipoDato = (EnumTipoDatoCampo)
                    Enum.ToObject(typeof(EnumTipoDatoCampo), campo.TIPO_DATO.TDT_CODIGO);

                    Valor auxCabecera;
                    Valor auxValor;
                    
                    if (!campo.CAM_VARIABLE.Value)
                    {
                        auxCabecera = null;
                        auxValor = Codificador.Valor(enumTipoDato, trama, contTrama, campo.CAM_LONGITUD);
                        contTrama += campo.CAM_LONGITUD;
                    }
                    else
                    {
                        auxCabecera = Codificador.Valor(enumTipoDato, trama, contTrama, campo.CAM_LONGITUD_CABECERA.Value);
                        contTrama += campo.CAM_LONGITUD_CABECERA.Value;
                        int longitud = int.Parse(((BCD) auxCabecera).ToInt());
                        auxValor = Codificador.Valor(enumTipoDato, trama, contTrama, longitud);
                        contTrama += longitud;
                    }

                    if (campo.CAM_TRANSACCIONAL)
                    {
                        AgregarTransaccional(auxValor, listaTransaccionales, transaccionales);
                    }
                    mensajeTransformado.AgregarCuerpo(campo, auxCabecera, auxValor);

                    contListaCampos++;
                }
            }

            //TODO: Validar que esten todos los campos requeridos
            //if (contListaCampos != listaCamposCuerpo.Count)
            //{
            //    for (int i = contListaCampos; i < listaCamposCuerpo.Count; i++)
            //    {
            //        if (listaCamposCuerpo[i].CAM_REQUERIDO)
            //        {
            //            throw new SwitchException(EnumSwitchException.MENSAJE_SIN_CAMPOS_OBLIGATORIOS);
            //        }
            //    }
            //}

            if (contTrama != trama.Length)
            {
                throw new SwitchException(EnumSwitchException.TRAMA_NO_IDENTIFICADA);
            }

            this.ListaPasos = DataAccess.Operacion.PasoDinamicaDA.ObtenerPasoDinamica
                (mensajeIdentificado.MEN_CODIGO, transaccionales.ToString());

            if (ListaPasos == null || ListaPasos.Count == 0)
            {
                throw new SwitchException("No se han encontrado pasos de dinamica para el mensaje transaccional");
            }

            return mensajeTransformado;
        }

        private void AgregarTransaccional(Valor valorTrama, ICollection<string> listaTransaccionales, StringBuilder transaccionales)
        {

            listaTransaccionales.Add(valorTrama.ToString());
            transaccionales.Append(valorTrama.ToString());
            transaccionales.Append(",");
        }

        public override byte[] Convertir(Mensaje mensaje)
        {
            byte[] trama=new byte[mensaje.Longitud];
            
            for (int i = 0; i < mensaje.NroCampos; i++)
            {
                Campo campo = mensaje[i];
                if (campo.Variable)
                {
                    Buffer.BlockCopy(campo.ValorCabecera.ToByte(), 0, trama, 0, campo.ValorCabecera.Longitud);   
                }
                
                Buffer.BlockCopy(campo.ValorCuerpo.ToByte(), 0, trama, 0, campo.ValorCuerpo.Longitud);
            }

            return trama;
        }
    }
}