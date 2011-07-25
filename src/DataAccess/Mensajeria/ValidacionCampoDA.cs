using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Mensajeria
{
    public class ValidacionCampoDA
    {
        public static List<VALIDACION_CAMPO> obtenerValidacionCampo(int grupoVal, int mensaje, int campo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.VALIDACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.VALIDACION_CAMPO.Include("TABLA").Include("COLUMNA").Where(o => o.GRV_CODIGO == grupoVal && o.MEN_CODIGO == mensaje && o.CAM_CODIGO == campo).ToList<VALIDACION_CAMPO>();
            }
        }

        public static VALIDACION_CAMPO obtenerValidacionCampoPorCodigo(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.VALIDACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.VALIDACION_CAMPO.Include("TABLA").Include("COLUMNA").Where(o => o.VLC_CODIGO == codigo).First();
            }
        }

        public static EstadoOperacion insertarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idValidacionCampo = (from c in contexto.VALIDACION_CAMPO
                                                 orderby c.VLC_CODIGO descending
                                                 select c.VLC_CODIGO).FirstOrDefault() + 1;
                        vcampo.VLC_CODIGO = idValidacionCampo;

                        string insert = "INSERT INTO [VALIDACION_CAMPO]" +
                                        "([VLC_CODIGO]" +
                                        ",[GRV_CODIGO]" +
                                        ",[MEN_CODIGO]" +
                                        ",[CAM_CODIGO]" +
                                        ",[VLC_INCLUSION_EXCLUSION]" +
                                        ",[VLC_CONDICION]" +
                                        ",[VLC_VALOR]" +
                                        ",[TBL_CODIGO]" +
                                        ",[COL_CODIGO]" +
                                        ",[VLC_PROCEDIMIENTO])" +
                                        " VALUES(@codigo,@grupo,@mensaje,@campo,@incexc,@condicion,@valor,@tabla,@columna,@procedimiento)";

                        SqlCommand ComandoInsert = (SqlCommand)contexto.CreateCommand(insert, CommandType.Text);

                        ComandoInsert.Parameters.Add(new SqlParameter("@codigo", vcampo.VLC_CODIGO));
                        ComandoInsert.Parameters.Add(new SqlParameter("@grupo", vcampo.GRV_CODIGO));
                        ComandoInsert.Parameters.Add(new SqlParameter("@mensaje", vcampo.MEN_CODIGO));
                        ComandoInsert.Parameters.Add(new SqlParameter("@campo", vcampo.CAM_CODIGO));
                        ComandoInsert.Parameters.Add(new SqlParameter("@incexc", vcampo.VLC_INCLUSION_EXCLUSION));
                        if (vcampo.VLC_CONDICION != null)
                            ComandoInsert.Parameters.Add(new SqlParameter("@condicion", vcampo.VLC_CONDICION));
                        else
                            ComandoInsert.Parameters.Add(new SqlParameter("@condicion", DBNull.Value));
                        if (vcampo.VLC_VALOR != null)
                            ComandoInsert.Parameters.Add(new SqlParameter("@valor", vcampo.VLC_VALOR));
                        else
                            ComandoInsert.Parameters.Add(new SqlParameter("@valor", DBNull.Value));
                        if (vcampo.TABLA != null)
                            ComandoInsert.Parameters.Add(new SqlParameter("@tabla", vcampo.TABLA.TBL_CODIGO));
                        else
                            ComandoInsert.Parameters.Add(new SqlParameter("@tabla", DBNull.Value));
                        if (vcampo.COLUMNA != null)
                            ComandoInsert.Parameters.Add(new SqlParameter("@columna", vcampo.COLUMNA.COL_CODIGO));
                        else
                            ComandoInsert.Parameters.Add(new SqlParameter("@columna", DBNull.Value));
                        if (vcampo.VLC_PROCEDIMIENTO != null)
                            ComandoInsert.Parameters.Add(new SqlParameter("@procedimiento", vcampo.VLC_PROCEDIMIENTO));
                        else
                            ComandoInsert.Parameters.Add(new SqlParameter("@procedimiento", DBNull.Value));

                        if (ComandoInsert.ExecuteNonQuery() != 1)
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

        public static EstadoOperacion modificarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE [VALIDACION_CAMPO]" +
                            "SET [VLC_INCLUSION_EXCLUSION] = @incexc" +
                            ",[VLC_CONDICION] = @condicion" +
                            ",[VLC_VALOR] = @valor" +
                            ",[TBL_CODIGO] = @tabla" +
                            ",[COL_CODIGO] = @columna" +
                            ",[VLC_PROCEDIMIENTO] = @procedimiento" +
                            " WHERE [VLC_CODIGO] = @codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", vcampo.VLC_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@incexc", vcampo.VLC_INCLUSION_EXCLUSION));
                        if (vcampo.VLC_CONDICION != null)
                            Comando.Parameters.Add(Factoria.CrearParametro("@condicion", vcampo.VLC_CONDICION));
                        else
                            Comando.Parameters.Add(Factoria.CrearParametro("@condicion", DBNull.Value));
                        if (vcampo.VLC_VALOR != null)
                            Comando.Parameters.Add(Factoria.CrearParametro("@valor", vcampo.VLC_VALOR));
                        else
                            Comando.Parameters.Add(Factoria.CrearParametro("@valor", DBNull.Value));
                        if (vcampo.TABLA != null)
                            Comando.Parameters.Add(Factoria.CrearParametro("@tabla", vcampo.TABLA.TBL_CODIGO));
                        else
                            Comando.Parameters.Add(Factoria.CrearParametro("@tabla", DBNull.Value));
                        if (vcampo.COLUMNA != null)
                            Comando.Parameters.Add(Factoria.CrearParametro("@columna", vcampo.COLUMNA.COL_CODIGO));
                        else
                            Comando.Parameters.Add(Factoria.CrearParametro("@columna", DBNull.Value));
                        if (vcampo.VLC_PROCEDIMIENTO != null)
                            Comando.Parameters.Add(Factoria.CrearParametro("@procedimiento", vcampo.VLC_PROCEDIMIENTO));
                        else
                            Comando.Parameters.Add(Factoria.CrearParametro("@procedimiento", DBNull.Value));

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

        public static EstadoOperacion eliminarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "DELETE FROM [VALIDACON_CAMPO] " +
                                       "WHERE [VLC_CODIGO] = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", vcampo.VLC_CODIGO));

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

        public static List<VALIDACION_CAMPO> obtenerListaValidacionCampoComponente()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.VALIDACION_CAMPO.MergeOption = MergeOption.NoTracking;
                var listaValidacionCampo =
                contexto.VALIDACION_CAMPO
                .Include("GRUPO_VALIDACION")
                .Include("CAMPO")
                .Include("COLUMNA")
                .Include("TABLA")
                .ToList<VALIDACION_CAMPO>();
                return listaValidacionCampo;
            }
        }
    }
}
