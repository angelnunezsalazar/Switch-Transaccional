using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Operacion
{
    public sealed class TipoTransformacionDA
    {
        private static DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        public static SortedList<int, string> obtenerTipoTransformacion()
        {
            return Util.GetEnumDataSource<EnumTipoTransformacion>();
        }

        public static string ejecutarTransformacionProcedimiento(string nombreProcedimiento,string parametro)
        {
            string resultado = null;
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {

                        DbCommand Comando = contexto.CreateCommand(nombreProcedimiento, CommandType.StoredProcedure);
                        Comando.Parameters.Add(Factoria.CrearParametro("@entrada", parametro));
                        Comando.Parameters.Add(Factoria.CrearParametro("@salida", ParameterDirection.Output));

                        Comando.ExecuteNonQuery();
                        resultado = Comando.Parameters["@salida"].Value.ToString();

                    }
                }
            }
            catch (Exception) { }

            return resultado;
        }
    }
}
