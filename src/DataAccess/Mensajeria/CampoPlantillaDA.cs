using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Mensajeria
{
    public sealed class CampoPlantillaDA
    {
        public static List<CAMPO_PLANTILLA> obtenerCampoPlantilla()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                return contexto.CAMPO_PLANTILLA.OrderBy(o => o.CMP_POSICION_RELATIVA).ToList<CAMPO_PLANTILLA>();
            }
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaNoAsignadosMensaje(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                var listaCampoPlantilla = (from m in contexto.MENSAJE
                                           where m.MEN_CODIGO == codigoMensaje
                                           from cp in contexto.CAMPO_PLANTILLA
                                           where cp.GRUPO_MENSAJE.GMJ_CODIGO == m.GRUPO_MENSAJE.GMJ_CODIGO
                                           select cp).Except
                                         (from c in contexto.CAMPO
                                          where c.MEN_CODIGO == codigoMensaje
                                          select c.CAMPO_PLANTILLA);


                return listaCampoPlantilla.ToList<CAMPO_PLANTILLA>();
            }
        }

        public static CAMPO_PLANTILLA obtenerCampoPlantilla(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                return contexto.CAMPO_PLANTILLA.Include("TIPO_DATO").Include("GRUPO_MENSAJE")
                    .Where(o => o.CMP_CODIGO == codigo).FirstOrDefault<CAMPO_PLANTILLA>();
            }
        }

        public static CAMPO_PLANTILLA obtenerCampoPlantillaSinRelaciones(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                return contexto.CAMPO_PLANTILLA
                    .Where(o => o.CMP_CODIGO == codigo).FirstOrDefault<CAMPO_PLANTILLA>();
            }
        }

        public static CAMPO_PLANTILLA obtenerCampoPlantillaPorPosicionRelativaPorGrupoMensaje(int posicionRelativa, int grupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                return contexto.CAMPO_PLANTILLA
                    .Where(o => o.CMP_POSICION_RELATIVA == posicionRelativa && o.GRUPO_MENSAJE.GMJ_CODIGO == grupoMensaje).FirstOrDefault<CAMPO_PLANTILLA>();
            }
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaCabeceraPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                var listaMensajes = (from c in contexto.CAMPO_PLANTILLA.Include("TIPO_DATO")
                                     where c.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                     && c.CMP_CABECERA == true
                                     select c).ToList<CAMPO_PLANTILLA>();

                return listaMensajes;
            }
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaCuerpoPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                var listaMensajes = (from c in contexto.CAMPO_PLANTILLA.Include("TIPO_DATO")
                                     where c.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                     && c.CMP_CABECERA == false
                                     orderby c.CMP_POSICION_RELATIVA
                                     select c).ToList<CAMPO_PLANTILLA>();

                return listaMensajes;
            }
        }

        public static List<CAMPO_PLANTILLA> obtenerCampoPlantillaPorCodigoGrupoMensaje(int codigoGrupoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO_PLANTILLA.MergeOption = MergeOption.NoTracking;
                var listaMensajes = (from c in contexto.CAMPO_PLANTILLA.Include("TIPO_DATO")
                                     where c.GRUPO_MENSAJE.GMJ_CODIGO == codigoGrupoMensaje
                                     orderby c.CMP_POSICION_RELATIVA
                                     select c).ToList<CAMPO_PLANTILLA>();

                return listaMensajes;
            }
        }

        public static EstadoOperacion insertarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        CampoPlantilla.CMP_CODIGO = (from c in contexto.CAMPO_PLANTILLA
                                                     orderby c.CMP_CODIGO descending
                                                     select c.CMP_CODIGO).FirstOrDefault() + 1;

                        string query =
                            "INSERT INTO [CAMPO_PLANTILLA]" +
                                       "([CMP_CODIGO]" +
                                       ",[CMP_NOMBRE]" +
                                       ",[CMP_LONGITUD]" +
                                       ",[CMP_SELECTOR]" +
                                       ",[CMP_VARIABLE]" +
                                       ",[CMP_PROTEGIDO_LOG]" +
                                       ",[CMP_ALMACENADO]" +
                                       ",[CMP_POSICION_RELATIVA]" +
                                       ",[CMP_LONGITUD_CABECERA]" +
                                       ",[GMJ_CODIGO]" +
                                       ",[CMP_CABECERA]" +
                                       ",[CMP_BITMAP]" +
                                       ",[CMP_TRANSACCIONAL]" +
                                       ",[TDT_CODIGO])" +
                                 "VALUES" +
                                       "(@codigo" +
                                       ",@nombre" +
                                       ",@longitud" +
                                       ",@selector" +
                                       ",@variable" +
                                       ",@protegidolog" +
                                       ",@almacenado" +
                                       ",@posicionrelativa" +
                                       ",@longitudCabecera" +
                                       ",@grupomensaje_codigo" +
                                       ",@cabecera" +
                                       ",@bitmap" +
                                       ",@transaccional" +
                                       ",@tipodato_codigo)";

                        DbCommand Comando = crearComando(contexto, CampoPlantilla, query);

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

        public static EstadoOperacion modificarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE [CAMPO_PLANTILLA]" +
                               " SET [CMP_NOMBRE]   =  @nombre" +
                                  ",[CMP_LONGITUD] = @longitud" +
                                  ",[CMP_SELECTOR] = @selector" +
                                  ",[CMP_VARIABLE] = @variable" +
                                  ",[CMP_PROTEGIDO_LOG] = @protegidolog" +
                                  ",[CMP_POSICION_RELATIVA]= @posicionrelativa" +
                                  ",[CMP_LONGITUD_CABECERA]= @longitudCabecera" +
                                  ",[CMP_ALMACENADO] = @almacenado" +
                                  ",[CMP_CABECERA] = @cabecera" +
                                  ",[CMP_BITMAP] = @bitmap" +
                                  ",[CMP_TRANSACCIONAL] = @transaccional" +
                                  ",[TDT_CODIGO] = @tipodato_codigo" +
                             " WHERE [CMP_CODIGO] = @codigo";

                        DbCommand Comando = crearComando(contexto, CampoPlantilla, query);

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (SqlException ex)
            {
                return new EstadoOperacion(false, ex.Errors[0].Message, ex, true);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }


        public static EstadoOperacion eliminarCampoPlantilla(CAMPO_PLANTILLA CampoPlantilla)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM [CAMPO_PLANTILLA]" +
                                 " WHERE [CMP_CODIGO] = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", CampoPlantilla.CMP_CODIGO));

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
                    return new EstadoOperacion(false, "El Campo Plantilla corresponde a un campo en el sistema y no se puede eliminar", e, true);
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

        private static DbCommand crearComando(dbSwitch contexto, CAMPO_PLANTILLA CampoPlantilla, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", CampoPlantilla.CMP_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", CampoPlantilla.CMP_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@longitud", CampoPlantilla.CMP_LONGITUD));
            Comando.Parameters.Add(Factoria.CrearParametro("@selector", CampoPlantilla.CMP_SELECTOR));
            Comando.Parameters.Add(Factoria.CrearParametro("@variable", CampoPlantilla.CMP_VARIABLE));
            Comando.Parameters.Add(Factoria.CrearParametro("@protegidolog", CampoPlantilla.CMP_PROTEGIDO_LOG));
            Comando.Parameters.Add(Factoria.CrearParametro("@almacenado", CampoPlantilla.CMP_ALMACENADO));

            if (CampoPlantilla.CMP_VARIABLE)
                Comando.Parameters.Add(Factoria.CrearParametro("@longitudCabecera", Util.NullableToDbValue<int>(CampoPlantilla.CMP_LONGITUD_CABECERA)));
            else
                Comando.Parameters.Add(Factoria.CrearParametro("@longitudCabecera", DBNull.Value));

            Comando.Parameters.Add(Factoria.CrearParametro("@grupomensaje_codigo", CampoPlantilla.GRUPO_MENSAJE.GMJ_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipodato_codigo", CampoPlantilla.TIPO_DATO.TDT_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@cabecera", CampoPlantilla.CMP_CABECERA));

            if (CampoPlantilla.CMP_CABECERA)
                Comando.Parameters.Add(Factoria.CrearParametro("@posicionrelativa", 0));
            else
                Comando.Parameters.Add(Factoria.CrearParametro("@posicionrelativa", CampoPlantilla.CMP_POSICION_RELATIVA));

            Comando.Parameters.Add(Factoria.CrearParametro("@bitmap", CampoPlantilla.CMP_BITMAP));
            Comando.Parameters.Add(Factoria.CrearParametro("@transaccional", CampoPlantilla.CMP_TRANSACCIONAL));

            return Comando;
        }
    }
}
