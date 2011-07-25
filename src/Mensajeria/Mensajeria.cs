using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Convertidor;
using Mensajeria.Mensajes;

namespace Mensajeria
{
    public class Mensajeria
    {
        public Mensajeria()
        {
            CargarDatos();
        }

        List<GRUPO_MENSAJE> listaGrupoMensaje;

        public List<GRUPO_MENSAJE> ListaGrupoMensaje
        {
            get { return listaGrupoMensaje; }
            set { listaGrupoMensaje = value; }
        }

        private void CargarDatos()
        {
            this.listaGrupoMensaje = DataAccess.Mensajeria.GrupoMensajeDA.obtenerGrupoMensajeConCamposConDatos();
        }

        public Convertidor.Convertidor ObtenerConvertidor(GRUPO_MENSAJE grupoMensaje)
        {
            EnumTipoMensaje tipoMensaje = (EnumTipoMensaje)
                              Enum.ToObject(typeof(EnumTipoMensaje),grupoMensaje.TIPO_MENSAJE.TMJ_CODIGO);

            return Convertidor.Convertidor.ObtenerConvertidor(grupoMensaje, tipoMensaje);
        }

        public List<CAMPO_PLANTILLA> ObtenerCamposCabecera(int grupoMensaje)
        {
            var lista = (from g in this.listaGrupoMensaje
                         where g.GMJ_CODIGO == grupoMensaje
                         from c in g.CAMPO_PLANTILLA
                         where c.CMP_CABECERA
                         select c).ToList();

            return lista;
        }

        public MENSAJE IdentificarMensajePorValorSelector(int grupoMensaje, List<Valor> listaValores)
        {
            var listaMensajes = (from g in this.listaGrupoMensaje
                                 where g.GMJ_CODIGO == grupoMensaje
                                 select g.MENSAJE).FirstOrDefault();

            foreach (MENSAJE mensaje in listaMensajes)
            {
                bool mensajeEncontrado = false;
                bool siguienteMensaje = false;
                foreach (Valor valor in listaValores)
                {
                    foreach (CAMPO campo in mensaje.CAMPO)
                    {
                        if (campo.CAM_SELECTOR && campo.CAM_CABECERA)
                        {
                            EnumTipoDatoCampo enumTipoDato = (EnumTipoDatoCampo)
                                Enum.ToObject(typeof(EnumTipoDatoCampo), campo.TIPO_DATO.TDT_CODIGO);

                            Valor selector = Codificador.Valor(enumTipoDato, campo.CAM_VALOR_SELECTOR_REQUEST);

                            if (valor.Equals(selector))
                            {
                                mensajeEncontrado = true;
                            }
                            else
                            {
                                siguienteMensaje = true;
                            }
                            break;
                        }
                    }
                    if (siguienteMensaje)
                        break;
                }
                if (mensajeEncontrado)
                    return mensaje;
            }

            return null;
        }

    }
}
