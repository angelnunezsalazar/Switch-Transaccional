using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace DataAccess.Factoria.SqlServer
{
    public class SqlClient:DbFactory
    {
        public override DbCommand CrearComando(string query,DbConnection conexion)
        {
            return new SqlCommand(query,(SqlConnection)conexion);
        }

        public override DbConnection CrearConexion(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public override DbParameter CrearParametro(string NombreParametro, object Valor)
        {
            return new SqlParameter(NombreParametro, Valor);
        }

        public override DbExceptionProduct CrearException(DbException exception)
        {
            return new SqlExceptionProduct((SqlException)exception);
        }

        public override DbDataAdapter CrearDataAdapter(string query, DbConnection conexion)
        {
            return new SqlDataAdapter(query, (SqlConnection)conexion);
        }

        public override DbDataAdapter CrearDataAdapter()
        {
            return new SqlDataAdapter();
        }


        public override string ObtenerCadenaConexion(EntityConnectionStringBuilder builder)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(builder.ProviderConnectionString);
            return sqlBuilder.ToString();
        }

        public override DbParameter CrearParametro(string NombreParametro, ParameterDirection direction)
        {
            return new SqlParameter(NombreParametro,direction);
        }
    }
}
