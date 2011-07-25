using System;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using DataAccess.Factoria.SqlServer;

namespace DataAccess.Factoria
{
    public enum DataProviderType
    {
        Sql,
        Oracle
    }

    public abstract class DbFactory
    {

        public string Conexion
        {
            get
            {
                return CadenaConexion.getInstance().conexion;
            }
        }

        public string ConexionEntidades
        {
            get
            {
                return CadenaConexion.getInstance().conexionEntidades;
            }
        }

        public abstract DbCommand CrearComando(string query, DbConnection conexion);
        public abstract DbConnection CrearConexion(string connectionString);
        public abstract DbParameter CrearParametro(string NombreParametro, object Valor);
        public abstract DbParameter CrearParametro(string NombreParametro,ParameterDirection direction);
        public abstract DbExceptionProduct CrearException(DbException exception);
        public abstract DbDataAdapter CrearDataAdapter(string query, DbConnection conexion);
        public abstract DbDataAdapter CrearDataAdapter();
        public abstract string ObtenerCadenaConexion(EntityConnectionStringBuilder builder);
    }

    public class DataAccessFactory
    {
        static DataProviderType dataProvider = DataProviderType.Sql;

        public static DbFactory ObtenerProveedor()
        {
            return ObtenerProveedor(dataProvider);
        }

        public static DbFactory ObtenerProveedor(DataProviderType Tipo)
        {
            switch (Tipo)
            {
                case DataProviderType.Sql:
                    return new SqlClient();
                default:
                    throw new ArgumentException("Proveedor Invalido");

            }
        }
    }
}
