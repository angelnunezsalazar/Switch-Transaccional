using System;
using System.Collections;
using System.Collections.Generic;
using System.Messaging;
using BusinessEntity;
using ColaMensajes;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Mensajeria;
using Mensajeria.Convertidor;
using Mensajeria.Mensajes;
using Switch.DA;
using Switch.Dinamica;
using Switch.Tanqueos;
using Utilidades;


namespace Switch
{
    public class Switch
    {
        readonly Mensajeria.Mensajeria mensajeria;
        readonly Dinamica.Dinamica dinamica;

        public Switch(IFactoryDA factoryDA, IDllDinamica dllDinamica)
        {
            this.mensajeria = new Mensajeria.Mensajeria();
            this.dinamica = new Dinamica.Dinamica(factoryDA, dllDinamica);
            DataAccess.CadenaConexion.getInstance().InitializeConnectionString();
            IniciarListener();
        }

        private static GRUPO_MENSAJE ObtenerGrupoMensajePorColaMensaje(string queueName)
        {
            GRUPO_MENSAJE grupoMensaje = GrupoMensajeDA.ObtenerGrupoMensajePorColaMensaje(queueName);
            return grupoMensaje;
        }

        private void MessageListenerRequest(object sender, ReceiveCompletedEventArgs e)
        {
            Message Msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);

            ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(Cola.TimeSpan, e.AsyncResult.AsyncState);

            ProcesarMensajeRequest(Msg);
        }

        private void MessageListenerResponse(object sender, ReceiveCompletedEventArgs e)
        {
            Message Msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);

            ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(Cola.TimeSpan, e.AsyncResult.AsyncState);

            ProcesarMensajeResponse(Msg);
        }

        private void IniciarListener()
        {
            Cola.StartListener(Cola.REQUEST_QUEQUE,
                               MessageListenerRequest);

            Cola.StartListener(Cola.RESPONSE_QUEQUE,
                               MessageListenerResponse);
        }

        private void ProcesarMensajeRequest(Message msg)
        {
            try
            {
                IEnumerable<PASO_DINAMICA> pasosDinamica;
                
                Mensaje mensajeEntrante = ConvertirMensajeEntrante(msg, out pasosDinamica);

                DinamicaDeMensaje dinamicaMensaje = new DinamicaDeMensaje(dinamica, mensajeEntrante, pasosDinamica);
                dinamicaMensaje.ProcesarRequest();

                byte[] mensajeSaliente = ConvertirMensajeSaliente(dinamicaMensaje);

                EnviarMensaje(msg, dinamicaMensaje, mensajeSaliente);
            }
            catch (Exception ex)
            {
                DescartarMensaje(msg.ResponseQueue);
            }
        }

        private void ProcesarMensajeResponse(Message msg)
        {
            try
            {
                IEnumerable<PASO_DINAMICA> pasosDinamica;

                Mensaje mensajeEntrante = ConvertirMensajeEntrante(msg, out pasosDinamica);

                DinamicaDeMensaje dinamicaMensaje = Tanqueo.Destanquear(mensajeEntrante);
                dinamicaMensaje.ProcesarResponse();

                byte[] mensajeSaliente = ConvertirMensajeSaliente(dinamicaMensaje);

                EnviarMensaje(msg, dinamicaMensaje, mensajeSaliente);
            }
            catch (Exception ex)
            {
                DescartarMensaje(msg.ResponseQueue);
            }
        }

        private void EnviarMensaje(Message msg, DinamicaDeMensaje dinamicaMensaje, byte[] mensajeSaliente)
        {
            switch (dinamicaMensaje.ResultadoDinamica.Tipo)
            {
                case EnumResultadoDinamica.EnviarRespuesta:
                    EnviarRespuesta(msg.ResponseQueue, mensajeSaliente);
                    break;
                case EnumResultadoDinamica.DescartarMensaje:
                    DescartarMensaje(msg.ResponseQueue);
                    break;
                case EnumResultadoDinamica.EsperarRespuesta:
                    Tanqueo.Tanquear(dinamicaMensaje);
                    EsperarRespuesta(dinamicaMensaje.ResultadoDinamica.EntidadComunicacion.EDC_COLA
                                     , mensajeSaliente);
                    break;
            }
        }

        private byte[] ConvertirMensajeSaliente(DinamicaDeMensaje dinamicaMensaje)
        {
            Convertidor convertidor = Convertidor.ObtenerConvertidor(null, dinamicaMensaje.Mensaje.Tipo);
            return convertidor.Convertir(dinamicaMensaje.Mensaje);
        }

        private Mensaje ConvertirMensajeEntrante(Message msg, out IEnumerable<PASO_DINAMICA> pasosDinamica)
        {
            GRUPO_MENSAJE grupoMensaje = ObtenerGrupoMensajePorColaMensaje(msg.ResponseQueue.Label);

            EnumTipoMensaje tipoMensaje = (EnumTipoMensaje)
                                          Enum.ToObject(typeof(EnumTipoMensaje), grupoMensaje.TIPO_MENSAJE.TMJ_CODIGO);

            Convertidor convertidor = Convertidor.ObtenerConvertidor(grupoMensaje, tipoMensaje);
            Mensaje mensaje = convertidor.Convertir((byte[])msg.Body, mensajeria);
            pasosDinamica = convertidor.ListaPasos;
            return mensaje;
        }

        private void EnviarRespuesta(MessageQueue queue, byte[] mensajeResponse)
        {
            Cola.EnviarMensaje(mensajeResponse, "Switch-" + DateTime.Now + "-EnviarRespuesta"
                , queue, null);
        }

        private void DescartarMensaje(MessageQueue queue)
        {
            Cola.EnviarMensaje(Codificador.Codificacion("DESCARTAR"), "Switch-" + DateTime.Now + "-Descartar"
                , queue, null);
        }

        private void EsperarRespuesta(string queue, byte[] mensajeResponse)
        {
            Cola.EnviarMensaje(mensajeResponse, "Switch-" + DateTime.Now + "-EsperarRespuesta"
                , queue, Cola.RESPONSE_QUEQUE);
        }


    }
}
