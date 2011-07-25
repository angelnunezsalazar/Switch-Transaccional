using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Mensajeria
{
    public sealed class TablaDA
    {
        public static List<TABLA> obtenerTabla()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TABLA.MergeOption = MergeOption.NoTracking;
                return contexto.TABLA.ToList<TABLA>();
            }
        }

        public static TABLA obtenerTablaPorCodigo(int codigoTabla)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TABLA.MergeOption = MergeOption.NoTracking;
                return contexto.TABLA.Where(o => o.TBL_CODIGO == codigoTabla).First();
            }
        }

        public static EstadoOperacion insertarTabla(TABLA tabla)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {

                    using (contexto.CreateConeccionScope())
                    {
                        tabla.TBL_CODIGO = 0;
                        string query = "InsertarTabla";

                        DbCommand Comando = crearComando(contexto, tabla, query);

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }
                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException ex)
            {
                return new EstadoOperacion(false, ex.Message, ex, false);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion modificarTabla(TABLA tabla)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "ModificarTabla";

                        DbCommand Comando = crearComando(contexto, tabla, query);

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException ex)
            {
                return new EstadoOperacion(false, ex.Message, ex, false);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion eliminarTabla(TABLA tabla)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "EliminarTabla";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", tabla.TBL_CODIGO));

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException dbex)
            {
                return new EstadoOperacion(false, dbex.Message, dbex);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        private static DbCommand crearComando(dbSwitch contexto, TABLA tabla, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);

            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", tabla.TBL_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", tabla.TBL_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", tabla.TBL_DESCRIPCION));

            return Comando;
        }

        public static DataTable ObtenerValoresTabla(string tabla)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DataTable dataTable = new DataTable();
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                using (DbCommand Comando = contexto.CreateCommand("Select * from " + tabla, CommandType.Text))
                {
                    using (DbDataAdapter adapter = Factoria.CrearDataAdapter())
                    {
                        adapter.SelectCommand = Comando;
                        using (contexto.CreateConeccionScope())
                        {
                            adapter.Fill(dataTable);
                        }

                        return dataTable;
                    }
                }
            }
        }

        public static bool ExisteValorTabla(string tabla, string columnaOrigen, string cadenaBuscar)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                using (DbCommand Comando = contexto.CreateCommand("Select count(*) from " + tabla + " where " + columnaOrigen + "='" + cadenaBuscar + "'",
                CommandType.Text))
                {
                    Comando.Connection.Open();
                    int cantidad = (int)Comando.ExecuteScalar();
                    return cantidad>0?true:false;
                }
            }
        }

        public static string ObtenerValorTabla(string tabla, string columnaOrigen, string columnaDestino, string cadenaBuscar)
        {
            string cadenaEncontrada = null;
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                using (DbCommand Comando = contexto.CreateCommand("Select " + columnaDestino + " from " + tabla + " where " + columnaOrigen + "='" + cadenaBuscar + "'",
                CommandType.Text))
                {
                    cadenaEncontrada = Comando.ExecuteScalar().ToString();
                }
            }
            return cadenaEncontrada;
        }

        public static EstadoOperacion insertarValoresTabla(string tabla,
            List<string> columnasTabla, List<string> valoresTabla)
        {
            string queryMaxId = "Select max(ID)+1 from " + tabla;

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    DbCommand Comando = contexto.CreateCommand(queryMaxId, CommandType.Text);

                    using (contexto.CreateConeccionScope())
                    {
                        object Id = Comando.ExecuteScalar();
                        int NuevoId = 1;
                        if (Id != DBNull.Value)
                        {
                            NuevoId = (int)Id;
                        }
                        string queryInsert = GenerarInsertQuery(tabla, columnasTabla, valoresTabla, NuevoId);

                        Comando.CommandText = queryInsert;

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }
                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion modificarValoresTabla(string tabla,
            List<string> columnasTabla, List<string> valoresTabla, int Id)
        {
            string queryUpdate = GenerarUpdateQuery(tabla, columnasTabla, valoresTabla, Id);

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    DbCommand Comando = contexto.CreateCommand(queryUpdate, CommandType.Text);

                    using (contexto.CreateConeccionScope())
                    {
                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }
                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion eliminarValoresTabla(string tabla,
            int Id)
        {
            string query = "delete from " + tabla + " where ID=" + Id.ToString();

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                    using (contexto.CreateConeccionScope())
                    {
                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }
                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static string GenerarInsertQuery(string tabla,
            List<string> columnasTabla, List<string> valoresTabla, int Id)
        {

            string queryInsert = "Insert into  " + tabla + "(ID, ";

            for (int i = 0; i < columnasTabla.Count; i++)
            {
                if (i == columnasTabla.Count - 1)
                    queryInsert = queryInsert + columnasTabla[i];
                else
                    queryInsert = queryInsert + columnasTabla[i] + ", ";

            }
            queryInsert = queryInsert + ")" + "Values (" + Id.ToString() + ",";

            for (int i = 0; i < valoresTabla.Count; i++)
            {
                if (i == valoresTabla.Count - 1)
                    queryInsert = queryInsert + "'" + valoresTabla[i] + "' ";
                else
                    queryInsert = queryInsert + "'" + valoresTabla[i] + "', ";

            }

            queryInsert = queryInsert + ")";

            return queryInsert;
        }


        public static string GenerarUpdateQuery(string tabla,
            List<string> columnasTabla, List<string> valoresTabla, int Id)
        {

            string queryUpdate = "Update  " + tabla + " set ";

            for (int i = 0; i < columnasTabla.Count; i++)
            {
                if (i == columnasTabla.Count - 1)
                    queryUpdate = queryUpdate + columnasTabla[i] + "='" + valoresTabla[i] + "' ";
                else
                    queryUpdate = queryUpdate + columnasTabla[i] + "='" + valoresTabla[i] + "', ";
            }

            queryUpdate = queryUpdate + " where ID" + " = " + Id.ToString();

            return queryUpdate;
        }
    }
}
