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
    public class GrupoValidacionDA
    {
        public static EstadoOperacion insertarGrupoValidacion(GRUPO_VALIDACION grupo)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idGrupoValidacion = (from c in contexto.GRUPO_VALIDACION
                                                 orderby c.GRV_CODIGO descending
                                                 select c.GRV_CODIGO).FirstOrDefault() + 1;
                        grupo.GRV_CODIGO = idGrupoValidacion;

                        string insert = "INSERT INTO GRUPO_VALIDACION" +
                                        "(GRV_CODIGO" +
                                        ",GRV_NOMBRE" +
                                        ",MEN_CODIGO)" +
                                        " VALUES(@codigo,@nombre,@mensaje)";

                        SqlCommand ComandoInsert = (SqlCommand)contexto.CreateCommand(insert, CommandType.Text);

                        ComandoInsert.Parameters.Add(new SqlParameter("@codigo", grupo.GRV_CODIGO));
                        ComandoInsert.Parameters.Add(new SqlParameter("@nombre", grupo.GRV_NOMBRE));
                        ComandoInsert.Parameters.Add(new SqlParameter("@mensaje", grupo.MENSAJE.MEN_CODIGO));

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

        public static GRUPO_VALIDACION obtenerGrupoValidacionPorCodigo(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_VALIDACION.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_VALIDACION.Include("MENSAJE").Include("MENSAJE.GRUPO_MENSAJE").Where(o => o.GRV_CODIGO == codigo).First();
            }
        }

        public static List<GRUPO_VALIDACION> obtenerGrupoValidacionComponente()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_VALIDACION.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_VALIDACION.Include("VALIDACION_CAMPO")
                    .Include("VALIDACION_CAMPO.CAMPO.CAMPO_PLANTILLA")
                    .Include("VALIDACION_CAMPO.CAMPO")
                    .Include("VALIDACION_CAMPO.TABLA")
                    .Include("VALIDACION_CAMPO.COLUMNA").ToList();
            }
        }

        public static List<GRUPO_VALIDACION> obtenerGrupoValidacion(int mensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_VALIDACION.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_VALIDACION.Include("MENSAJE").Include("MENSAJE.GRUPO_MENSAJE").Where(o => o.MENSAJE.MEN_CODIGO == mensaje).ToList<GRUPO_VALIDACION>();
            }
        }

        public static List<GRUPO_VALIDACION> obtenerGrupoValidacionSinRelaciones(int mensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_VALIDACION.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_VALIDACION.Where(o => o.MENSAJE.MEN_CODIGO == mensaje).ToList<GRUPO_VALIDACION>();
            }
        }

        public static EstadoOperacion modificarGrupoValidacion(GRUPO_VALIDACION grupo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE GRUPO_VALIDACION" +
                            " SET GRV_NOMBRE = @nombre" +
                            " WHERE GRV_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", grupo.GRV_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@nombre", grupo.GRV_NOMBRE));

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

        public static List<GRUPO_VALIDACION> obtenerListaGrupoValidacionComponente()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_VALIDACION.MergeOption = MergeOption.NoTracking;
                var listaGrupoValidacion =
                contexto.GRUPO_VALIDACION
                .Include("MENSAJE")
                .Include("MENSAJE.GRUPO_MENSAJE")
                .ToList();

                return listaGrupoValidacion;
            }
        }

        public static EstadoOperacion eliminarGrupoValidacion(GRUPO_VALIDACION grupoValidacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM GRUPO_VALIDACION" +
                                 " WHERE GRV_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", grupoValidacion.GRV_CODIGO));

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
    }
}
