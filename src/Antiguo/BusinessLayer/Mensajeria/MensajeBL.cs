using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.Objects;

    using DataAccess.Mensajeria;

    [DataObject(true)]
    public class MensajeBL
    {
        public static List<MENSAJE> obtenerMensaje()
        {
            using (Switch contexto = new Switch())
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.MENSAJE.ToList<MENSAJE>();
            }
        }

        public static MENSAJE obtenerMensaje(int codigo)
        {
            using (Switch contexto = new Switch())
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.MENSAJE.Include("GRUPO_MENSAJE").Where(o => o.MEN_CODIGO == codigo).FirstOrDefault<MENSAJE>();
            }
        }

        public static List<MENSAJE> obtenerMensajePorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            using (Switch contexto = new Switch())
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                var listaMensajes = from m in contexto.MENSAJE
                                    where m.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                    orderby m.MEN_NOMBRE ascending
                                    select m;

                return listaMensajes.ToList<MENSAJE>();
            }
        }

        public static List<MENSAJE> obtenerMensajePorCodigoGrupoMensajeTodosEnCasoContrario(string codigoGrupoMensaje)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            using (Switch contexto = new Switch())
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                DbCommand Comando = contexto.CreateCommand(
                    "Select MEN_CODIGO,MEN_NOMBRE from MENSAJE where GMJ_CODIGO like @codigoGrupo", CommandType.Text);

                Comando.Parameters.Add(Factoria.CrearParametro("@codigoGrupo", codigoGrupoMensaje));

                List<MENSAJE> listaMensaje = Comando.Materialize<MENSAJE>().OrderBy(o => o.MEN_NOMBRE).ToList<MENSAJE>();

                return listaMensaje;
            }
        }

        public static EstadoOperacion insertarMensaje(MENSAJE Mensaje)
        {
            try
            {
                using (Switch contexto = new Switch())
                {

                    using (contexto.CreateConeccionScope())
                    {
                        Mensaje.MEN_CODIGO = (from c in contexto.MENSAJE
                                                         orderby c.MEN_CODIGO descending
                                                         select c.MEN_CODIGO).FirstOrDefault() + 1;

                        string query =
                            "INSERT INTO MENSAJE" +
                            "(MEN_CODIGO" +
                            ",MEN_DESCRIPCION" +
                            ",MEN_NOMBRE" +
                            ",GMJ_CODIGO)" +
                            "VALUES" +
                            "(@codigo" +
                            ",@descripcion" +
                            ",@nombre" +
                            ",@grupomensaje_codigo)";

                        DbCommand Comando = MensajeDA.crearComando(contexto, Mensaje, query);

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

        public static EstadoOperacion modificarMensaje(MENSAJE Mensaje)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE MENSAJE" +
                            " SET " +
                            "MEN_DESCRIPCION = @descripcion" +
                            ",MEN_NOMBRE = @nombre " +
                            "WHERE MEN_CODIGO = @codigo";

                        DbCommand Comando = MensajeDA.crearComando(contexto, Mensaje, query);

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

        public static EstadoOperacion eliminarMensaje(MENSAJE Mensaje)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM MENSAJE" +
                            " WHERE MEN_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Mensaje.MEN_CODIGO));

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
                    return new EstadoOperacion(false, "El Mensaje esta siendo utilizado por la aplicación y no se puede eliminar", e, true);
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
