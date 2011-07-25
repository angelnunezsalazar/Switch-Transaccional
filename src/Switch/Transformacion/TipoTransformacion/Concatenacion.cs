using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Switch.DA;

namespace Switch.Transformacion.TipoTransformacion
{
    public class Concatenacion : TransformacionCampo
    {
        protected readonly IFactoryDA FactoryDa;
        protected string separador="";

        public Concatenacion(IFactoryDA factoryDA)
        {
            FactoryDa = factoryDA;
        }

        public override string Transformar(TRANSFORMACION_CAMPO entidadTransformacion, Mensaje mensaje)
        {
            List<PARAMETRO_TRANSFORMACION_CAMPO> listaParametros = entidadTransformacion.PARAMETRO_TRANSFORMACION_CAMPO.ToList();

            StringBuilder concatenacion = new StringBuilder();

            foreach (PARAMETRO_TRANSFORMACION_CAMPO parametro in listaParametros)
            {
                EnumTipoParametroTransformacionCampo tipoParametroTransformacion = (EnumTipoParametroTransformacionCampo)
                                Enum.ToObject(typeof(EnumTipoParametroTransformacionCampo), parametro.PTC_TIPO);

                Campo campoOrigen = mensaje.Campo(parametro.CAMPO);

                Valor cadenaBuscar = campoOrigen.ValorCuerpo.SubValor(parametro.PTC_POSICION_INICIAL.Value-1, parametro.PTC_LONGITUD.Value);
                string cadenaConcatenar = null;

                switch (tipoParametroTransformacion)
                {
                    case EnumTipoParametroTransformacionCampo.Tabla:
                        cadenaConcatenar = FactoryDa.ValorTabla(parametro.TABLA.TBL_NOMBRE, parametro.COLUMNA_ORIGEN.COL_NOMBRE, parametro.COLUMNA_DESTINO.COL_NOMBRE, cadenaBuscar.Dato.ToString());
                        //TablaDA.ObtenerValorTabla
                        break;
                    case EnumTipoParametroTransformacionCampo.CampoOrigen:
                        cadenaConcatenar = cadenaBuscar.Dato.ToString();
                        break;
                    default:
                        break;
                }
                concatenacion.Append(cadenaConcatenar ?? "").Append(separador);

            }

            return TransformacionEspecifica(entidadTransformacion,concatenacion.ToString());
        }

        protected virtual string TransformacionEspecifica(TRANSFORMACION_CAMPO entidadTransformacion,string concatenacion)
        {
            return concatenacion;
        }
    }
}
