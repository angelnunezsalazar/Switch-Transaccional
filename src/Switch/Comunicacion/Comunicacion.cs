using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Switch.DA;
using Switch.Dinamica;

namespace Switch.Comunicacion
{
    public class Comunicacion : Componente
    {
        List<ENTIDAD_COMUNICACION> listaEntidades;

        public Comunicacion(IFactoryDA factoryDA)
        {
            listaEntidades = factoryDA.Entidad();
        }

        public ENTIDAD_COMUNICACION ObtenerEntidad(int idEntidad)
        {
            return listaEntidades.Where(e => e.EDC_CODIGO == idEntidad).SingleOrDefault();
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje, PASO_DINAMICA paso)
        {
            EnumTipoFuncionalidad tipoMensaje = (EnumTipoFuncionalidad)
                        Enum.ToObject(typeof(EnumTipoFuncionalidad), paso.PDT_FUNCIONALIDAD);

            switch (tipoMensaje)
            {
                case EnumTipoFuncionalidad.Enviar:
                    return new EnviarMensaje(paso);
                case EnumTipoFuncionalidad.Recibir:
                    return new RecibirMensaje(paso);
                default:
                    throw new Exception("Error: Comunicacion - ObtenerPaso (No se encontra ninguna comunicacion");
            }
        }
    }

    public class EnviarMensaje : Paso
    {
        public ENTIDAD_COMUNICACION EntidadComunicacion { get; private set; }

        public EnviarMensaje(PASO_DINAMICA pasoDinamica)
            : base(pasoDinamica)
        {
            EntidadComunicacion = pasoDinamica.ENTIDAD_COMUNICACION;
        }

        public override EnumResultadoPaso EjecutarPaso(Mensajeria.Mensajes.Mensaje mensajeOrigen)
        {
            return EnumResultadoPaso.Enviar;
        }
    }

    public class RecibirMensaje : Paso
    {
        public Comunicacion Comunicacion { get; private set; }

        public RecibirMensaje(PASO_DINAMICA pasoDinamica)
            : base(pasoDinamica)
        {

        }

        public override EnumResultadoPaso EjecutarPaso(Mensajeria.Mensajes.Mensaje mensajeOrigen)
        {
            return EnumResultadoPaso.Recibir;
        }
    }
}
