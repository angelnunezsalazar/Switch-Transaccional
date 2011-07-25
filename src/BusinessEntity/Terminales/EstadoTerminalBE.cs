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
        public EstadoOperacion insertarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            string query =
                "INSERT INTO [ESTADO_TERMINAL]" +
                "([EST_CODIGO_ESTADO]" +
                ",[EST_DESCRIPCION_ESTADO])" +
                "VALUES" +
                "(@codigo" +
                ",@descripcion)";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", estadoTerminal.EST_CODIGO_ESTADO));
            Comando.Parameters.Add(new SqlParameter("@descripcion", estadoTerminal.EST_DESCRIPCION_ESTADO));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }

        public EstadoOperacion modificarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            string query =
                "UPDATE [ESTADO_TERMINAL]" +
                "SET " +
                "[EST_DESCRIPCION_ESTADO] = @descripcion " +
                "WHERE [EST_CODIGO_ESTADO]=@codigo";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", estadoTerminal.EST_CODIGO_ESTADO));
            Comando.Parameters.Add(new SqlParameter("@descripcion", estadoTerminal.EST_DESCRIPCION_ESTADO));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }

        public EstadoOperacion eliminarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            string query =
                "DELETE FROM [ESTADO_TERMINAL]" +
                "WHERE [EST_CODIGO_ESTADO] =@codigo;";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", estadoTerminal.EST_CODIGO_ESTADO));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }
    }
}
