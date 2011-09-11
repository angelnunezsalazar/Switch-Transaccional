using System;
using System.Data;
using System.Data.SqlClient;
using BusinessEntity;

namespace DataAccess.Procedures
{
    public sealed class ProcedureDA
    {
        public static object EjecutarProcedure(string procedure, string parametro)
        {
            using (Switch contexto = new Switch())
            {
                using (SqlCommand Comando = (SqlCommand)contexto.CreateCommand(procedure, CommandType.StoredProcedure))
                {
                    Comando.Parameters.Add("@input", SqlDbType.VarChar, 500).Value = parametro;
                    Comando.Parameters.Add("@output", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    using (contexto.CreateConeccionScope())
                    {
                        Comando.ExecuteNonQuery();
                        return Comando.Parameters["@output"].Value;
                    }
                }
            }
        }
    }
}