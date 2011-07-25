using System.Data.SqlClient;

namespace DataAccess.Factoria.SqlServer
{
    class SqlExceptionProduct : DbExceptionProduct
    {
        SqlException exception;

        public SqlExceptionProduct(SqlException exception)
        {
            this.exception = exception;
        }
        public override bool ForeignKeyError()
        {
            if (this.exception.Number == (int)EnumSqlErrores.FOREIGN_KEY_VIOLATION)
            {
                return true;
            }
            else
                return false;
        }
    }
}
