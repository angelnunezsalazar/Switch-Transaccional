namespace DataAccess.Comunicacion
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Data.Entity;
    using BusinessEntity;

    using DataAccess.Factoria;
    using DataAccess.Utilitarios;

    public sealed class EntidadComunicacionDA
    {

        public static List<EntidadComunicacion> obtenerEntidadComunicacion()
        {
            using (Switch contexto = new Switch())
            {
                return contexto.EntidadComunicacion
                            .Include(x => x.TipoEntidad)
                            .Include(x => x.Protocolo)
                            .AsNoTracking().ToList();
            }
        }

        public static EntidadComunicacion obtenerEntidadComunicacion(int id)
        {
            using (Switch contexto = new Switch())
            {
                return contexto.EntidadComunicacion
                    .Include(x => x.Protocolo)
                    .Include(x => x.TipoEntidad)
                    .Where(o => o.Id == id)
                    .AsNoTracking().FirstOrDefault();
            }
        }


        public static List<EntidadComunicacion> obtenerEntidadComunicacionEnGrupoMensaje(int grupoMensajeId)
        {
            using (Switch contexto = new Switch())
            {
                var entidades = (from c in contexto.EntidadComunicacion
                                 where c.GrupoMensajeId == grupoMensajeId
                                 select c).AsNoTracking().ToList();
                return entidades;
            }
        }

        public static List<EntidadComunicacion> obtenerEntidadComunicacionSinGrupo()
        {
            using (Switch contexto = new Switch())
            {
                var entidades = (from c in contexto.EntidadComunicacion
                                 where c.GrupoMensaje == null
                                 select c).AsNoTracking().ToList();
                return entidades;
            }
        }

        public static List<EntidadComunicacion> obtenerEntidadComunicacionSinRelaciones()
        {
            using (Switch contexto = new Switch())
            {
                var entidades = (from c in contexto.EntidadComunicacion
                                 select c).AsNoTracking().ToList();
                return entidades;
            }
        }


        public static EstadoOperacion insertarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    contexto.EntidadComunicacion.Add(entidadComunicacion);
                    contexto.SaveChanges();
                    return new EstadoOperacion(true, null, null);
                }
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion modificarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    contexto.EntidadComunicacion.Attach(entidadComunicacion);
                    contexto.Entry(entidadComunicacion).State = EntityState.Modified;
                    contexto.SaveChanges();

                    return new EstadoOperacion(true, null, null);
                }

            }

            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }


        public static EstadoOperacion eliminarEntidadComunicacion(EntidadComunicacion entidadComunicacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    contexto.EntidadComunicacion.Attach(entidadComunicacion);
                    contexto.EntidadComunicacion.Remove(entidadComunicacion);

                    return new EstadoOperacion(true, null, null);
                }
            }
            catch (DbException e)
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
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

        public static EstadoOperacion agregarEntidadAGrupoMensaje(int codigoGrupoMensaje, int codigoEntidadComunicacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE EntidadComunicacion" +
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

        public static EstadoOperacion eliminarEntidadDeGrupoMensaje(EntidadComunicacion entidadComunicacion)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE EntidadComunicacion" +
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
