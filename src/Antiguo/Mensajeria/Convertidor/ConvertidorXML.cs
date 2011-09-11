using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Excepciones;
using Mensajeria.Mensajes;

namespace Mensajeria.Convertidor
{
    public class ConvertidorXML : Convertidor
    {
        public ConvertidorXML(GRUPO_MENSAJE grupoMensaje)
            : base(grupoMensaje)
        {

        }

        public override Mensaje Convertir(byte[] trama, Mensajeria mensajeria)
        {
            Mensaje mensajeTransformado = Mensaje.CrearMensaje(EnumTipoMensaje.XML);

            List<CAMPO_PLANTILLA> listaCamposCabecera = mensajeria.ObtenerCamposCabecera(this.grupoMensaje.GMJ_CODIGO);

            List<Valor> listaSelectores = new List<Valor>();
            List<string> listaTransaccionales = new List<string>();

            StringBuilder transaccionales = new StringBuilder("");

            using (XmlReader reader = new XmlTextReader(new StringReader(Codificador.Codificacion(trama))))
            {
                reader.Read();
                foreach (CAMPO_PLANTILLA campoCabecera in listaCamposCabecera)
                {
                    reader.Read();

                    if (campoCabecera.CMP_NOMBRE.ToUpper() != reader.Name.ToUpper())
                    {
                        throw new SwitchException(EnumSwitchException.MENSAJE_SIN_CAMPOS_OBLIGATORIOS);
                    }

                    reader.Read();
                    Valor valor = Codificador.Valor(EnumTipoDatoCampo.Alfabetico, reader.Value);
                    if (campoCabecera.CMP_SELECTOR)
                    {
                        listaSelectores.Add(valor);
                    }
                    if (campoCabecera.CMP_TRANSACCIONAL)
                    {
                        listaTransaccionales.Add(reader.Value);
                        transaccionales.Append(valor);
                        transaccionales.Append(",");
                    }

                    mensajeTransformado.AgregarCabecera(campoCabecera, null, valor);

                    reader.Read();

                }

                MENSAJE mensajeIdentificado = mensajeria.IdentificarMensajePorValorSelector(this.grupoMensaje.GMJ_CODIGO, listaSelectores);

                if (mensajeIdentificado == null)
                {
                    throw new SwitchException(EnumSwitchException.TRAMA_NO_IDENTIFICADA);
                }

                List<CAMPO> listaCamposCuerpo = (from c in mensajeIdentificado.CAMPO
                                                 where c.CAM_CABECERA == false
                                                 orderby c.CAM_POSICION_RELATIVA ascending
                                                 select c).ToList();

                foreach (CAMPO campoMensaje in listaCamposCuerpo)
                {
                    reader.Read();

                    if (campoMensaje.CAM_NOMBRE.ToUpper() != reader.Name.ToUpper())
                    {
                        throw new SwitchException(EnumSwitchException.MENSAJE_SIN_CAMPOS_OBLIGATORIOS);
                    }

                    reader.Read();
                    Valor valor = Codificador.Valor(EnumTipoDatoCampo.Alfabetico, reader.Value);
                    if (campoMensaje.CAM_TRANSACCIONAL)
                    {
                        listaTransaccionales.Add(reader.Value);
                        transaccionales.Append(valor);
                        transaccionales.Append(",");
                    }
                    mensajeTransformado.AgregarCuerpo(campoMensaje, null, valor);
                    reader.Read();
                }

                this.ListaPasos = DataAccess.Operacion.PasoDinamicaDA.ObtenerPasoDinamica
                    (mensajeIdentificado.MEN_CODIGO, transaccionales.ToString());
            }



            return mensajeTransformado;
        }

        public override byte[] Convertir(Mensaje mensaje)
        {
            StringBuilder palabra = new StringBuilder("");
            int nroCampos = mensaje.NroCampos;
            for (int i = 0; i < nroCampos; i++)
            {
                Campo campo = mensaje[i];
                String tagInicio = "<" + campo.Nombre + ">";
                String tagFinal = "</" + campo.Nombre + ">";
                palabra.Append(tagInicio);
                palabra.Append(campo.ValorCabecera);
                palabra.Append(tagFinal);
            }

            return Codificador.Codificacion(palabra.ToString());
        }
    }
}