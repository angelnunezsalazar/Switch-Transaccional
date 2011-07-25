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
    public sealed class GrupoMensajeDA
    {

        public static List<GRUPO_MENSAJE> obtenerGrupoMensaje()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_MENSAJE.Include("TIPO_MENSAJE").ToList<GRUPO_MENSAJE>();
            }
        }

        public static GRUPO_MENSAJE obtenerGrupoMensaje(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_MENSAJE.Include("TIPO_MENSAJE").Where(o => o.GMJ_CODIGO == codigo).FirstOrDefault<GRUPO_MENSAJE>();
            }
        }

        public static List<GRUPO_MENSAJE> obtenerGrupoMensajeConCamposConDatos()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.GRUPO_MENSAJE.MergeOption = MergeOption.NoTracking;
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                contexto.TIPO_DATO.MergeOption = MergeOption.NoTracking;
                return contexto.GRUPO_MENSAJE
                    .Include("CAMPO_PLANTILLA").Include("CAMPO_PLANTILLA.TIPO_DATO").Include("MENSAJE")
                    .Include("MENSAJE.CAMPO").Include("MENSAJE.CAMPO.TIPO_DATO").ToList<GRUPO_MENSAJE>();
            }
        }

        public static EstadoOperacion insertarGrupoMensaje(GRUPO_MENSAJE grupoMensaje)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        grupoMensaje.GMJ_CODIGO = (from c in contexto.GRUPO_MENSAJE
                                                   orderby c.GMJ_CODIGO descending
                                                   select c.GMJ_CODIGO).FirstOrDefault() + 1;

                        string query =
                                "INSERT INTO GRUPO_MENSAJE" +
                                   "(GMJ_CODIGO" +
                                   ",GMJ_NOMBRE" +
                                   ",GMJ_DESCRIPCION" +
                                   ",TMJ_CODIGO)" +
                                "VALUES" +
                                   "(@codigo" +
                                   ",@nombre" +
                                   ",@descripcion" +
                                   ",@tipomensaje_codigo)";

                        DbCommand Comando = crearComando(contexto, grupoMensaje, query);

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

        public static EstadoOperacion modificarGrupoMensaje(GRUPO_MENSAJE Mensaje)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE GRUPO_MENSAJE" +
                           " SET " +
                              "GMJ_NOMBRE = @nombre" +
                              ",GMJ_DESCRIPCION = @descripcion" +
                              ",TMJ_CODIGO = @tipomensaje_codigo " +
                         "WHERE GMJ_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, Mensaje, query);

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


        public static EstadoOperacion eliminarGrupoMensaje(GRUPO_MENSAJE grupoMensaje)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM GRUPO_MENSAJE" +
                                 " WHERE GMJ_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", grupoMensaje.GMJ_CODIGO));

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException e)
            {
                DbExceptionProduct exception = Factoria.CrearException(e);
                if (exception.ForeignKeyError())
                {
                    return new EstadoOperacion(false, "El Grupo de Mensaje se encuentra asignado en el sistema y no se puede eliminar", e, true);
                }
                else
                {
                    return new EstadoOperacion(false, e.Message, e);
                }
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        private static DbCommand crearComando(dbSwitch contexto, GRUPO_MENSAJE grupoMensaje, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", grupoMensaje.GMJ_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", grupoMensaje.GMJ_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", grupoMensaje.GMJ_DESCRIPCION));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipomensaje_codigo", grupoMensaje.TIPO_MENSAJE.TMJ_CODIGO));

            return Comando;
        }

        public static GRUPO_MENSAJE ObtenerGrupoMensajePorColaMensaje(string colaMensaje)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                string query = "ObtenerGrupoyTipoMensaje";

                DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);
                Comando.Parameters.Add(Factoria.CrearParametro("@nombreCola", colaMensaje));
                using (contexto.CreateConeccionScope())
                {
                    DbDataReader reader = Comando.ExecuteReader();

                    GRUPO_MENSAJE grupo=null;
                    while (reader.Read())
                    {
                        grupo = new GRUPO_MENSAJE { GMJ_CODIGO = reader.GetInt32(0) };
                        TIPO_MENSAJE tipomensaje = new TIPO_MENSAJE { TMJ_CODIGO = reader.GetInt32(1) };
                        grupo.TIPO_MENSAJE = tipomensaje;
                    }

                    return grupo;
                    
                }
            }
        }
    }
}
