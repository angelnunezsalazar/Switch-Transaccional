using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Extensions;


namespace BusinessEntity
{
    public partial class dbSwitch : global::System.Data.Objects.ObjectContext
    {
        public IDisposable CreateConeccionScope()
        {
            return this.Connection.CreateConnectionScope();
        }

        public DbCommand CreateCommand(string query, CommandType tipoComando)
        {
            return this.CreateStoreCommand(query, tipoComando);
        }

        public DbConnection CrearConeccion()
        {
            return this.Connection;
        }
    }
}
