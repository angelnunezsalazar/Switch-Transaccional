using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;
using Microsoft.Data.Extensions;

namespace DataAccess.Mensajeria
{
    public sealed class MensajeDA
    {
        public static List<MENSAJE> obtenerMensaje()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.MENSAJE.ToList<MENSAJE>();
            }
        }

        public static MENSAJE obtenerMensaje(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                return contexto.MENSAJE.Include("GRUPO_MENSAJE").Where(o => o.MEN_CODIGO == codigo).FirstOrDefault<MENSAJE>();
            }
        }

        public static List<MENSAJE> obtenerMensajePorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {

            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
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

            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                DbCommand Comando = contexto.CreateCommand(
                    "Select MEN_CODIGO,MEN_NOMBRE from MENSAJE where GMJ_CODIGO like @codigoGrupo", CommandType.Text);

                Comando.Parameters.Add(Factoria.CrearParametro("@codigoGrupo", codigoGrupoMensaje));

                List<MENSAJE> listaMensaje = Comando.Materialize<MENSAJE>().OrderBy(o => o.MEN_NOMBRE).ToList<MENSAJE>();

                return listaMensaje;
            }
        }


        public static List<MENSAJE> obtenerMensajeConCamposPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                var listaMensajes = from m in contexto.MENSAJE.Include("CAMPO").Include("CAMPO.CAMPO_PLANTILLA")
                                    where m.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                    orderby m.MEN_NOMBRE ascending
                                    select m;

                return listaMensajes.ToList<MENSAJE>();
            }
        }

        public static List<MENSAJE> obtenerMensajeConCamposConTipoDato()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.MENSAJE.MergeOption = MergeOption.NoTracking;
                var listaMensajes = from m in contexto.MENSAJE.Include("CAMPO").Include("TIPO_DATO")
                                    orderby m.GRUPO_MENSAJE.GMJ_CODIGO ascending
                                    select m;

                return listaMensajes.ToList<MENSAJE>();
            }
        } 

        public static EstadoOperacion insertarMensaje(MENSAJE Mensaje)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
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

        public static EstadoOperacion modificarMensaje(MENSAJE Mensaje)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE MENSAJE" +
                               " SET " +
                                  "MEN_DESCRIPCION = @descripcion" +
                                  ",MEN_NOMBRE = @nombre " +
                            "WHERE MEN_CODIGO = @codigo";

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


        public static EstadoOperacion eliminarMensaje(MENSAJE Mensaje)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
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

        private static DbCommand crearComando(dbSwitch contexto, MENSAJE Mensaje, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Mensaje.MEN_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Mensaje.MEN_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", Mensaje.MEN_DESCRIPCION));
            if (Mensaje.GRUPO_MENSAJE != null)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", Mensaje.GRUPO_MENSAJE.GMJ_CODIGO));
            }

            return Comando;
        }
    }
}
