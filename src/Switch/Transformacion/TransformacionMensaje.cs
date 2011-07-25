using System;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Mensajeria.Mensajes;
using Switch.DA;
using Switch.Dinamica;
using Switch.Transformacion.TipoTransformacion;
using Utilidades;

namespace Switch.Transformacion
{
    public class TransformacionMensaje : Paso
    {
        public IDllDinamica DllDinamica { get; private set; }

        public TRANSFORMACION Entidad { get; private set; }
        public DinamicaDeMensaje DinamicaMensaje { get; private set; }
        public IFactoryDA FactoryDa { get; private set; }

        public TransformacionMensaje(DinamicaDeMensaje dinamicaMensaje, TRANSFORMACION entidad, PASO_DINAMICA pasoDinamica, IFactoryDA factoryDA, IDllDinamica dllDinamica)
            : base(pasoDinamica)
        {
            DinamicaMensaje = dinamicaMensaje;
            FactoryDa = factoryDA;
            Entidad = entidad;
            DllDinamica = dllDinamica;
        }

        public TransformacionCampo TransformacionCampo(TRANSFORMACION_CAMPO entidadTransformacionCampo)
        {
            EnumTipoTransformacion tipoTransformacion = (EnumTipoTransformacion)
                Enum.ToObject(typeof(EnumTipoTransformacion), entidadTransformacionCampo.TCM_TIPO);

            switch (tipoTransformacion)
            {
                case EnumTipoTransformacion.ValorConstante:
                    return new ValorConstante();
                case EnumTipoTransformacion.ProcedimientoAlmacenado:
                    return new Procedimiento(FactoryDa);
                case EnumTipoTransformacion.FuncionalidadEstandar:
                    return new FuncionalidadEstandar();
                case EnumTipoTransformacion.Concatenacion:
                    return new Concatenacion(FactoryDa);
                case EnumTipoTransformacion.Componente:
                    return new TipoTransformacion.Componente(DllDinamica, FactoryDa);
                default:
                    throw new NotImplementedException("Default option Transformacion Campo");
            }
        }

        public override EnumResultadoPaso EjecutarPaso(Mensaje mensajeOrigen)
        {
            EnumTipoMensaje tipo = (EnumTipoMensaje)
                    Enum.ToObject(typeof(EnumTipoMensaje), this.Entidad.MENSAJE_DESTINO.GRUPO_MENSAJE.TIPO_MENSAJE.TMJ_CODIGO);

            Mensaje mensajeDestino = Mensaje.CrearMensaje(tipo);

            foreach (TRANSFORMACION_CAMPO entidadTransformacionCampo in this.Entidad.TRANSFORMACION_CAMPO.ToList())
            {
                TransformacionCampo transformacionCampo = TransformacionCampo(entidadTransformacionCampo);
                Caracter trama = new Caracter(transformacionCampo.Transformar(entidadTransformacionCampo, mensajeOrigen));
                mensajeDestino.AgregarCuerpo(entidadTransformacionCampo.CAMPO_DESTINO, null, trama);
            }

            DinamicaMensaje.Mensaje = mensajeDestino;
            return EnumResultadoPaso.Correcto;
        }
    }
}
