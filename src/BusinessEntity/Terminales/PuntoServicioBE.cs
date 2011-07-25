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
        public EstadoOperacion insertarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            string insert = "INSERT INTO [PUNTO_SERVICIO]"+
           "([PSR_CODIGO_PUNTO_SERVICIO]"+
           ",[PSR_LOCAL]"+
           ",[PSR_ESTADO_PTO_SERVICIO]"+
           ",[PSR_DIRECCION]"+
           ",[PSR_IDENTIFICADOR])"+
            "VALUES(@codigo,@local,@estado,@direccion,@nombre)";

            DbCommand Comando = this.CreateStoreCommand(insert, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", puntoServicio.PSR_CODIGO_PUNTO_SERVICIO));
            Comando.Parameters.Add(new SqlParameter("@local", puntoServicio.PSR_LOCAL));
            Comando.Parameters.Add(new SqlParameter("@estado", puntoServicio.PSR_ESTADO_PTO_SERVICIO));
            Comando.Parameters.Add(new SqlParameter("@direccion", puntoServicio.PSR_DIRECCION));
            Comando.Parameters.Add(new SqlParameter("@nombre", puntoServicio.PSR_IDENTIFICADOR));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }
            
            return new EstadoOperacion(true, null, null);
        }

        public EstadoOperacion modificarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            string query =
                "UPDATE [PUNTO_SERVICIO]" +
                "SET [PSR_LOCAL] = @local" +
                ",[PSR_ESTADO_PTO_SERVICIO] = @estado" +
                ",[PSR_DIRECCION] = @direccion" +
                ",[PSR_IDENTIFICADOR] = @identificador" +
                " WHERE [PSR_CODIGO_PUNTO_SERVICIO] =@codigo;";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", puntoServicio.PSR_CODIGO_PUNTO_SERVICIO));
            Comando.Parameters.Add(new SqlParameter("@local", puntoServicio.PSR_LOCAL));
            Comando.Parameters.Add(new SqlParameter("@estado", puntoServicio.PSR_ESTADO_PTO_SERVICIO));
            Comando.Parameters.Add(new SqlParameter("@direccion", puntoServicio.PSR_DIRECCION));
            Comando.Parameters.Add(new SqlParameter("@identificador", puntoServicio.PSR_IDENTIFICADOR));

            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }

        public EstadoOperacion eliminarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            string query =
                "DELETE FROM [ESTADO_TERMINAL]" +
                "WHERE [PSR_CODIGO_PUNTO_SERVICIO] =@codigo;";

            DbCommand Comando = this.CreateStoreCommand(query, CommandType.Text);

            Comando.Parameters.Add(new SqlParameter("@codigo", puntoServicio.PSR_CODIGO_PUNTO_SERVICIO));
            
            if (Comando.ExecuteNonQuery() != 1)
            {
                return new EstadoOperacion(false, null, null);
            }

            return new EstadoOperacion(true, null, null);
        }
    }
}
