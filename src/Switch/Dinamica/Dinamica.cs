using System;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using Switch.DA;
using Utilidades;

namespace Switch.Dinamica
{
    public class Dinamica
    {
        readonly Transformacion.Transformacion transformacion;
        readonly Criptografia.Criptografia criptografia;
        readonly Descartar.Descartar descartar;
        readonly Comunicacion.Comunicacion comunicacion;
        readonly Validacion.Validacion validacion;

        public Dinamica(IFactoryDA factoryDA, IDllDinamica dllDinamica)
        {
            this.transformacion = new Transformacion.Transformacion(factoryDA, dllDinamica);
            //this.criptografia = new Criptografia.Criptografia();
            this.descartar = new Descartar.Descartar();
            this.comunicacion = new Comunicacion.Comunicacion(factoryDA);
            this.validacion = new Validacion.Validacion(factoryDA, dllDinamica);
        }

        public DinamicaDeMensaje ProcesarMensajeResponse(DinamicaDeMensaje dinamicaMensaje)
        {
            dinamicaMensaje.ProcesarResponse();
            return dinamicaMensaje;
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje, PASO_DINAMICA pasoDinamica)
        {
            EnumTipoFuncionalidad tipoMensaje = (EnumTipoFuncionalidad)
                Enum.ToObject(typeof(EnumTipoFuncionalidad), pasoDinamica.PDT_FUNCIONALIDAD);

            Componente componente;
            switch (tipoMensaje)
            {
                case EnumTipoFuncionalidad.Validacion:
                    componente = this.validacion;
                    break;
                case EnumTipoFuncionalidad.Transformacion:
                    componente = this.transformacion;
                    break;
                case EnumTipoFuncionalidad.Criptografia:
                    componente = this.criptografia;
                    break;
                case EnumTipoFuncionalidad.Enviar:
                case EnumTipoFuncionalidad.Recibir:
                    componente = this.comunicacion;
                    break;
                case EnumTipoFuncionalidad.Descartar:
                    componente = this.descartar;
                    break;
                default:
                    throw new Exception("Error: Dinamica - ObtenerPaso (No debe entrar a estar parte del código, no se encontro ningún componente");
            }

            return componente.ObtenerPaso(dinamicaMensaje, pasoDinamica);
        }
    }
}
