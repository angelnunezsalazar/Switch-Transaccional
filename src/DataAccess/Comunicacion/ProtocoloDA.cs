using System.Data.Common;

using BusinessEntity;

namespace DataAccess.Comunicacion
{
    using System.Diagnostics;

    public class ProtocoloDA
    {
        private static void crearComando(Switch contexto,Protocolo Protocolo, string query)
        {
            //DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            //DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            //Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Protocolo.PTR_CODIGO));
            //Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Protocolo.PTR_NOMBRE));
            //Comando.Parameters.Add(Factoria.CrearParametro("@timeOutRequest", Util.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_REQUEST)));
            //Comando.Parameters.Add(Factoria.CrearParametro("@timeOutResponse", Util.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_RESPONSE)));
            //Comando.Parameters.Add(Factoria.CrearParametro("@iniciaComunicacion", Protocolo.PTR_INICIA_COMM));
            //Comando.Parameters.Add(Factoria.CrearParametro("@aceptaComunicacion", Protocolo.PTR_ACEPTA_COMM));
            //Comando.Parameters.Add(Factoria.CrearParametro("@tipoComunicacion_codigo", Protocolo.TipoComunicacion.TPO_CODIGO));

            //if (Protocolo.TipoComunicacion.TPO_CODIGO == (int)EnumTipoComunicacion.Componente)
            //{
            //    Comando.Parameters.Add(Factoria.CrearParametro("@componente", Protocolo.PTR_COMPONENTE));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@nombreClase", Protocolo.PTR_NOMBRE_CLASE));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@nombreMetodo", Protocolo.PTR_NOMBRE_METODO));
            //}
            //else
            //{
            //    Comando.Parameters.Add(Factoria.CrearParametro("@componente", DBNull.Value));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@nombreClase", DBNull.Value));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@nombreMetodo", DBNull.Value));
            //}

            //if (Protocolo.TipoComunicacion.TPO_CODIGO == (int)EnumTipoComunicacion.TCP)
            //{
            //    Comando.Parameters.Add(Factoria.CrearParametro("@puerto", Util.NullableToDbValue<int>(Protocolo.PTR_PUERTO)));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@frame", Protocolo.PTR_FRAME));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@caracterInicio", Protocolo.PTR_CARACTER_INICIO));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@caracterFin", Protocolo.PTR_CARACTER_FIN));
            //}
            //else 
            //{
            //    Comando.Parameters.Add(Factoria.CrearParametro("@puerto", DBNull.Value));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@frame", DBNull.Value));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@caracterInicio", DBNull.Value));
            //    Comando.Parameters.Add(Factoria.CrearParametro("@caracterFin", DBNull.Value));
            //}

            //return Comando;
        }
    }
}
