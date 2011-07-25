using System;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;

namespace DataAccess.Operacion
{
    public class TransformacionCampoDA
    {
        public static TRANSFORMACION_CAMPO obtenerTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from tc in contexto.TRANSFORMACION_CAMPO

                        where tc.TRM_CODIGO == codigoTransformacion &&
                              tc.MEN_CODIGO_MENSAJE_DESTINO == codigoMensaje &&
                              tc.CAM_CODIGO_CAMPO_DESTINO == codigoCampo
                        select tc).FirstOrDefault<TRANSFORMACION_CAMPO>();
            }
        }

        public static int obtenerCantidadTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from tc in contexto.TRANSFORMACION_CAMPO
                        where tc.TRM_CODIGO == codigoTransformacion &&
                              tc.MEN_CODIGO_MENSAJE_DESTINO == codigoMensaje &&
                              tc.CAM_CODIGO_CAMPO_DESTINO == codigoCampo
                        select tc).Count();
            }
        }

        public static TRANSFORMACION_CAMPO obtenerTransformacionCampoConCampoDestino(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from tc in contexto.TRANSFORMACION_CAMPO
                            .Include("CAMPO_DESTINO")
                        where tc.TRM_CODIGO == codigoTransformacion &&
                              tc.MEN_CODIGO_MENSAJE_DESTINO == codigoMensaje &&
                              tc.CAM_CODIGO_CAMPO_DESTINO == codigoCampo
                        select tc).FirstOrDefault<TRANSFORMACION_CAMPO>();
            }
        }


        public static EstadoOperacion insertarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {

                    using (contexto.CreateConeccionScope())
                    {

                        string query =
                            "INSERT INTO TRANSFORMACION_CAMPO" +
                                       "(TRM_CODIGO" +
                                       ",CAM_CODIGO_CAMPO_DESTINO" +
                                       ",MEN_CODIGO_MENSAJE_DESTINO" +
                                       ",TCM_COMPONENTE" +
                                       ",TCM_CLASE" +
                                       ",TCM_METODO" +
                                       ",TCM_TIPO" +
                                       ",TCM_PROCEDIMIENTO" +
                                       ",TCM_VALOR_CONSTANTE" +
                                       ",TCM_FUNCIONALIDAD_ESTANDAR)" +
                                 "VALUES" +
                                       "(@transformacion_codigo" +
                                       ",@campoDestino_codigo" +
                                       ",@mensajeDestino_codigo" +
                                       ",@componente" +
                                       ",@clase" +
                                       ",@metodo" +
                                       ",@tipoTransformacion_codigo" +
                                       ",@procedimiento" +
                                       ",@valorConstante" +
                                       ",@funcionalidadEstandar_codigo)";

                        DbCommand Comando = crearComando(contexto, transformacionCampo, query);

                        Comando.Parameters.Add(Factoria.CrearParametro("@tieneParametros", 0));

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

        public static EstadoOperacion modificarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE TRANSFORMACION_CAMPO" +
                               " SET" +
                                  " TCM_COMPONENTE = @componente" +
                                  ",TCM_CLASE = @clase" +
                                  ",TCM_METODO = @metodo" +
                                  ",TCM_TIPO = @tipoTransformacion_Codigo" +
                                  ",TCM_PROCEDIMIENTO = @procedimiento" +
                                  ",TCM_VALOR_CONSTANTE = @valorConstante" +
                                  ",TCM_FUNCIONALIDAD_ESTANDAR = @funcionalidadEstandar_codigo" +
                            " WHERE TRM_CODIGO = @transformacion_codigo" +
                              " AND CAM_CODIGO_CAMPO_DESTINO = @campoDestino_codigo" +
                              " AND MEN_CODIGO_MENSAJE_DESTINO = @mensajeDestino_codigo";

                        DbCommand Comando = crearComando(contexto, transformacionCampo, query);

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

        public static EstadoOperacion eliminarTransformacionCampo(TRANSFORMACION_CAMPO transformacionCampo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM TRANSFORMACION_CAMPO" +
                                      " WHERE TRM_CODIGO=@transaccion_codigo" +
                                           " AND CAM_CODIGO_CAMPO_DESTINO=@campoDestino_codigo" +
                                           " AND MEN_CODIGO_MENSAJE_DESTINO=@mensajeDestino_codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@transformacion_codigo", transformacionCampo.TRM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@campoDestino_codigo", transformacionCampo.CAM_CODIGO_CAMPO_DESTINO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensajeDestino_codigo", transformacionCampo.MEN_CODIGO_MENSAJE_DESTINO));

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


        private static DbCommand crearComando(dbSwitch contexto, TRANSFORMACION_CAMPO transformacionCampo, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@transformacion_codigo", transformacionCampo.TRM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@campoDestino_codigo", transformacionCampo.CAM_CODIGO_CAMPO_DESTINO));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensajeDestino_codigo", transformacionCampo.MEN_CODIGO_MENSAJE_DESTINO));

            Comando.Parameters.Add(Factoria.CrearParametro("@tipoTransformacion_codigo", transformacionCampo.TCM_TIPO));



            if (transformacionCampo.TCM_TIPO == (int)EnumTipoTransformacion.Componente)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@componente", transformacionCampo.TCM_COMPONENTE));
                Comando.Parameters.Add(Factoria.CrearParametro("@clase", transformacionCampo.TCM_CLASE));
                Comando.Parameters.Add(Factoria.CrearParametro("@metodo", transformacionCampo.TCM_METODO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@componente", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@clase", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@metodo", DBNull.Value));
            }

            if (transformacionCampo.TCM_TIPO == (int)EnumTipoTransformacion.ValorConstante)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@valorConstante", transformacionCampo.TCM_VALOR_CONSTANTE));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@valorConstante", DBNull.Value));
            }

            if (transformacionCampo.TCM_TIPO == (int)EnumTipoTransformacion.ProcedimientoAlmacenado)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@procedimiento", transformacionCampo.TCM_PROCEDIMIENTO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@procedimiento", DBNull.Value));
            }

            if (transformacionCampo.TCM_TIPO == (int)EnumTipoTransformacion.FuncionalidadEstandar)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@funcionalidadEstandar_codigo", transformacionCampo.TCM_FUNCIONALIDAD_ESTANDAR));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@funcionalidadEstandar_codigo", DBNull.Value));
            }


            return Comando;
        }
    }
}
