using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Comunicacion
{
    public sealed class EntidadComunicacionDA
    {

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacion()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ENTIDAD_COMUNICACION.MergeOption = MergeOption.NoTracking;

                return contexto.ENTIDAD_COMUNICACION.Include("PROTOCOLO").Include("TIPO_ENTIDAD").ToList<ENTIDAD_COMUNICACION>();
            }
        }

        public static ENTIDAD_COMUNICACION obtenerEntidadComunicacion(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ENTIDAD_COMUNICACION.MergeOption = MergeOption.NoTracking;
                return contexto.ENTIDAD_COMUNICACION.Include("PROTOCOLO").Include("TIPO_ENTIDAD").Where(o => o.EDC_CODIGO == codigo).FirstOrDefault<ENTIDAD_COMUNICACION>();
            }
        }


        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionEnGrupoMensaje(int codigoGrupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ENTIDAD_COMUNICACION.MergeOption = MergeOption.NoTracking;
                var entidades = (from c in contexto.ENTIDAD_COMUNICACION
                                 where c.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                 select c).ToList<ENTIDAD_COMUNICACION>();
                return entidades;
            }
        }

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionSinGrupo()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ENTIDAD_COMUNICACION.MergeOption = MergeOption.NoTracking;
                var entidades = (from c in contexto.ENTIDAD_COMUNICACION
                                 where c.GRUPO_MENSAJE == null
                                 select c).ToList<ENTIDAD_COMUNICACION>();
                return entidades;
            }
        }

        public static List<ENTIDAD_COMUNICACION> obtenerEntidadComunicacionSinRelaciones()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ENTIDAD_COMUNICACION.MergeOption = MergeOption.NoTracking;
                var entidades = (from c in contexto.ENTIDAD_COMUNICACION
                                 select c).ToList<ENTIDAD_COMUNICACION>();
                return entidades;
            }
        }


        public static EstadoOperacion insertarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int nuevoId = (from c in contexto.ENTIDAD_COMUNICACION
                                       orderby c.EDC_CODIGO descending
                                       select c.EDC_CODIGO).FirstOrDefault() + 1;

                        entidadComunicacion.EDC_CODIGO = nuevoId;

                        string query =
                            "INSERT INTO ENTIDAD_COMUNICACION" +
                               "(EDC_CODIGO" +
                               ",EDC_NOMBRE" +
                               ",EDC_DESCRIPCION" +
                               ",EDC_COLA" +
                               ",EDC_RUTA_LOG" +
                               ",EDC_NOMBRE_LOG" +
                               ",PTR_CODIGO" +
                               ",EDC_TIMEOUT_EN_COLA" +
                               ",TEM_CODIGO)" +
                                "VALUES" +
                               "(@codigo" +
                               ",@nombre" +
                               ",@descripcion" +
                               ",@cola" +
                               ",@rutalog" +
                               ",@nombrelog" +
                               ",@protocolo_codigo" +
                               ",@tiempocola" +
                               ",@tipoEntidad_codigo)";

                        DbCommand Comando = crearComando(contexto, entidadComunicacion, query);

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

        public static EstadoOperacion modificarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "UPDATE ENTIDAD_COMUNICACION" +
                                   " SET " +
                                      "EDC_NOMBRE            = @nombre" +
                                      ",EDC_DESCRIPCION       = @descripcion" +
                                      ",EDC_COLA              = @cola" +
                                      ",EDC_RUTA_LOG          = @rutalog" +
                                      ",EDC_NOMBRE_LOG        = @nombrelog" +
                                      ",PTR_CODIGO            = @protocolo_codigo" +
                                      ",EDC_TIMEOUT_EN_COLA   = @tiempocola" +
                                      ",TEM_CODIGO            = @tipoEntidad_codigo" +
                                 " WHERE EDC_CODIGO           = @codigo";

                        DbCommand Comando = crearComando(contexto, entidadComunicacion, query);

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


        public static EstadoOperacion eliminarEntidadComunicacion(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM ENTIDAD_COMUNICACION" +
                                 " WHERE EDC_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", entidadComunicacion.EDC_CODIGO));

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
                    return new EstadoOperacion(false, "La entidad Comunicacion esta asignada a un Terminal y no se puede eliminar", e, true);
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

        private static DbCommand crearComando(dbSwitch contexto, ENTIDAD_COMUNICACION entidadComunicacion, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", entidadComunicacion.EDC_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", entidadComunicacion.EDC_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", entidadComunicacion.EDC_DESCRIPCION));
            Comando.Parameters.Add(Factoria.CrearParametro("@cola", entidadComunicacion.EDC_COLA));
            Comando.Parameters.Add(Factoria.CrearParametro("@rutalog", entidadComunicacion.EDC_RUTA_LOG));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombrelog", entidadComunicacion.EDC_NOMBRE_LOG));
            Comando.Parameters.Add(Factoria.CrearParametro("@protocolo_codigo", entidadComunicacion.PROTOCOLO.PTR_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@tiempocola", Util.NullableToDbValue<int>(entidadComunicacion.EDC_TIMEOUT_EN_COLA)));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipoEntidad_codigo", entidadComunicacion.TIPO_ENTIDAD.TEM_CODIGO));

            return Comando;
        }

        public static EstadoOperacion agregarEntidadAGrupoMensaje(int codigoGrupoMensaje, int codigoEntidadComunicacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE ENTIDAD_COMUNICACION" +
                           " SET GMJ_CODIGO = @grupomensaje_codigo " +
                         "WHERE EDC_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", codigoGrupoMensaje));
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", codigoEntidadComunicacion));

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

        public static EstadoOperacion eliminarEntidadDeGrupoMensaje(ENTIDAD_COMUNICACION entidadComunicacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE ENTIDAD_COMUNICACION" +
                           " SET GMJ_CODIGO = @grupomensaje_codigo " +
                         "WHERE EDC_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", DBNull.Value));
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", entidadComunicacion.EDC_CODIGO));

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
                    return new EstadoOperacion(false, "La entidad Comunicacion esta asignada a un Terminal y no se puede eliminar", e, true);
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
    }
}
