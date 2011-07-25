using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace BusinessEntity
{

    public partial class dbSwitch : global::System.Data.Objects.ObjectContext
    {
        public EstadoOperacion insertarProtocolo(PROTOCOLO Protocolo)
        {
            string query =
                "INSERT INTO [PROTOCOLO]" +
                "([PTR_CODIGO]" +
                ",[PTR_NOMBRE]" +
                ",[PTR_TIMEOUT_REQUEST]" +
                ",[PTR_TIMEOUT_RESPONSE]" +
                ",[PTR_PUERTO]" +
                ",[PTR_RUTA_COMPONENTE]" +
                ",[PTR_NOMBRE_COMPONENTE]" +
                ",[PTR_NOMBRE_CLASE]" +
                ",[PTR_NOMBRE_METODO]" +
                ",[TPO_CODIGO]" +
                ",[PTR_INICIA_COMM]" +
                ",[PTR_ACEPTA_COMM])" +
                "VALUES" +
                "(@codigo" +
                ",@nombre" +
                ",@timeOutRequest" +
                ",@timeOutResponse" +
                ",@puerto" +
                ",@rutaComponente" +
                ",@nombreComponente" +
                ",@nombreClase" +
                ",@nombreMetodo" +
                ",@tipoComunicacion_codigo" +
                ",@iniciaComunicacion" +
                ",@aceptaComunicacion)";

            DbCommand Comando = crearComando(Protocolo, query);

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }

        private DbCommand crearComando(PROTOCOLO Protocolo, string query)
        {
            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);
            Comando.Parameters.Add(new SqlParameter("@codigo", Protocolo.PTR_CODIGO));
            Comando.Parameters.Add(new SqlParameter("@nombre", Protocolo.PTR_NOMBRE));
            Comando.Parameters.Add(new SqlParameter("@timeOutRequest", Utilitarios.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_REQUEST)));
            Comando.Parameters.Add(new SqlParameter("@timeOutResponse", Utilitarios.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_RESPONSE)));
            Comando.Parameters.Add(new SqlParameter("@iniciaComunicacion", Protocolo.PTR_INICIA_COMM));
            Comando.Parameters.Add(new SqlParameter("@aceptaComunicacion", Protocolo.PTR_ACEPTA_COMM));
            Comando.Parameters.Add(new SqlParameter("@tipoComunicacion_codigo", Protocolo.TIPO_COMUNICACION.TPO_CODIGO));

            Comando.Parameters.Add(new SqlParameter("@puerto", DBNull.Value));
            Comando.Parameters.Add(new SqlParameter("@rutaComponente", DBNull.Value));
            Comando.Parameters.Add(new SqlParameter("@nombreComponente", DBNull.Value));
            Comando.Parameters.Add(new SqlParameter("@nombreClase", DBNull.Value));
            Comando.Parameters.Add(new SqlParameter("@nombreMetodo", DBNull.Value));

            if (Protocolo.TIPO_COMUNICACION.TPO_CODIGO == EnumTipoComunicacion.Componente.GetHashCode())
            {
                Comando.Parameters["@rutaComponente"].Value = Protocolo.PTR_RUTA_COMPONENTE;
                Comando.Parameters["@nombreComponente"].Value = Protocolo.PTR_NOMBRE_COMPONENTE;
                Comando.Parameters["@nombreClase"].Value = Protocolo.PTR_NOMBRE_CLASE;
                Comando.Parameters["@nombreMetodo"].Value = Protocolo.PTR_NOMBRE_METODO;

            }
            else if (Protocolo.TIPO_COMUNICACION.TPO_CODIGO == EnumTipoComunicacion.TCP.GetHashCode())
            {
                Comando.Parameters["@puerto"].Value = Utilitarios.NullableToDbValue<int>(Protocolo.PTR_PUERTO);
            }
            return Comando;
        }

        public EstadoOperacion modificarProtocolo(PROTOCOLO Protocolo)
        {
            string query =
                "UPDATE [PROTOCOLO]"+
                "SET "+
                "[PTR_NOMBRE] = @nombre"+
                ",[PTR_TIMEOUT_REQUEST] = @timeOutRequest"+
                ",[PTR_TIMEOUT_RESPONSE] = @timeOutResponse"+
                ",[PTR_PUERTO] = @puerto"+
                ",[PTR_RUTA_COMPONENTE] = @rutaComponente"+
                ",[PTR_NOMBRE_COMPONENTE] = @nombreComponente"+
                ",[PTR_NOMBRE_CLASE] = @nombreClase"+
                ",[PTR_NOMBRE_METODO] = @nombreMetodo"+
                ",[TPO_CODIGO] = @tipoComunicacion_codigo"+
                ",[PTR_INICIA_COMM] = @iniciaComunicacion"+
                ",[PTR_ACEPTA_COMM] = @aceptaComunicacion" +
                " WHERE [PTR_CODIGO] =@codigo";

            DbCommand Comando = crearComando(Protocolo, query);

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }

        public EstadoOperacion eliminarProtocolo(PROTOCOLO Protocolo)
        {
            string query =
                "DELETE FROM [PROTOCOLO]" +
                "WHERE [PTR_CODIGO] =@codigo;";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", Protocolo.PTR_CODIGO));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }
    }
}
