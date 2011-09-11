using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Operacion
{
    public sealed class TransformacionMensajeDA
    {
        private static DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        public static List<TRANSFORMACION> obtenerTransformacion(string nombreTransformada,
                                                          int codigoGrupoOrigen,
                                                          int codigoMensajeOrigen,
                                                          int codigoGrupoDestino,
                                                          int codigoMensajeDestino)
        {
            StringBuilder query = new StringBuilder("");
            query.Append("Select value T from TRANSFORMACION as T where " +
                "T.TRM_NOMBRE like '%" + nombreTransformada + "%'");

            if (codigoGrupoOrigen != -1)
            {
                query.Append(" and T.MENSAJE_ORIGEN.GRUPO_MENSAJE.GMJ_CODIGO =" + codigoGrupoOrigen);
            }

            if (codigoMensajeOrigen != -1)
            {
                query.Append(" and T.MENSAJE_ORIGEN.MEN_CODIGO =" + codigoMensajeOrigen);
            }

            if (codigoGrupoDestino != -1)
            {
                query.Append(" and T.MENSAJE_DESTINO.GRUPO_MENSAJE.GMJ_CODIGO =" + codigoGrupoDestino);
            }

            if (codigoMensajeDestino != -1)
            {
                query.Append(" and T.MENSAJE_ORIGEN.MEN_CODIGO =" + codigoMensajeDestino);
            }


            using (Switch contexto = new Switch())
            {
                contexto.TRANSFORMACION.MergeOption = MergeOption.NoTracking;
                ObjectQuery<TRANSFORMACION> Transformacion = new ObjectQuery<TRANSFORMACION>(query.ToString(), contexto);

                return Transformacion.Include("MENSAJE_ORIGEN").Include("MENSAJE_ORIGEN.GRUPO_MENSAJE")
                .Include("MENSAJE_DESTINO").Include("MENSAJE_DESTINO.GRUPO_MENSAJE")
                .ToList();
            }
        }

        public static List<TRANSFORMACION> obtenerListaTransformacionComponente()
        {
            using (Switch contexto = new Switch())
            {
                contexto.TRANSFORMACION.MergeOption = MergeOption.NoTracking;
                var listaTransformacion =
                contexto.TRANSFORMACION
                .Include("MENSAJE_DESTINO").Include("MENSAJE_DESTINO.GRUPO_MENSAJE").Include("MENSAJE_DESTINO.GRUPO_MENSAJE.TIPO_MENSAJE")
                .Include("TRANSFORMACION_CAMPO")
                .Include("TRANSFORMACION_CAMPO.CAMPO_DESTINO")
                .Include("TRANSFORMACION_CAMPO.CAMPO_DESTINO.TIPO_DATO")
                .Include("TRANSFORMACION_CAMPO.PARAMETRO_TRANSFORMACION_CAMPO")
                .Include("TRANSFORMACION_CAMPO.PARAMETRO_TRANSFORMACION_CAMPO.CAMPO")
                .Include("TRANSFORMACION_CAMPO.PARAMETRO_TRANSFORMACION_CAMPO.TABLA")
                .Include("TRANSFORMACION_CAMPO.PARAMETRO_TRANSFORMACION_CAMPO.COLUMNA_ORIGEN")
                .Include("TRANSFORMACION_CAMPO.PARAMETRO_TRANSFORMACION_CAMPO.COLUMNA_DESTINO")
                .ToList();

                return listaTransformacion;
            }
        }

        public static TRANSFORMACION obtenerTransformacion(int codigoTransformacion)
        {
            using (Switch contexto = new Switch())
            {
                contexto.TRANSFORMACION.MergeOption = MergeOption.NoTracking;
                var listaTransformacion =
                contexto.TRANSFORMACION
                .Include("MENSAJE_ORIGEN").Include("MENSAJE_ORIGEN.GRUPO_MENSAJE")
                .Include("MENSAJE_DESTINO").Include("MENSAJE_DESTINO.GRUPO_MENSAJE")
                .Where(o => o.TRM_CODIGO == codigoTransformacion).FirstOrDefault<TRANSFORMACION>();

                return listaTransformacion;
            }
        }

        public static List<TRANSFORMACION> obtenerTransformacionSinRelacionesPorMensajeOrigen(int codigoMensajeOrigen)
        {
            using (Switch contexto = new Switch())
            {
                contexto.TRANSFORMACION.MergeOption = MergeOption.NoTracking;
                var listaTransformacion =
                contexto.TRANSFORMACION
                .Where(o => o.MENSAJE_ORIGEN.MEN_CODIGO == codigoMensajeOrigen).ToList<TRANSFORMACION>();

                return listaTransformacion;
            }
        }

        public static List<TRANSFORMACION> obtenerTransformacionConTransformacionCampoConParametros()
        {
            using (Switch contexto = new Switch())
            {
                contexto.TRANSFORMACION.MergeOption = MergeOption.NoTracking;
                var listaTransformacion =
                contexto.TRANSFORMACION
                .Include("TRANSFORMACION_CAMPO").Include("TRANSFORMACION_CAMPO.CAMPO_DESTINO")
                .Include("MENSAJE_DESTINO").Include("MENSAJE_DESTINO.GRUPO_MENSAJE")
                .ToList<TRANSFORMACION>();

                return listaTransformacion;
            }
        
        }

        public static TRANSFORMACION obtenerTransformacionSinTransformacionCampo(int codigoTransformacion)
        {
            TRANSFORMACION tranformacion = null;
            using (DbConnection conexion = Factoria.CrearConexion(CadenaConexion.getInstance().conexion))
            {
                using (DbCommand comando = Factoria.CrearComando("ObtenerTransformacion", conexion))
                {
                    comando.Parameters.Add(Factoria.CrearParametro("@TRM_CODIGO", codigoTransformacion));
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    conexion.Open();
                    DbDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        tranformacion = new TRANSFORMACION();
                        tranformacion.TRM_CODIGO = (int)reader["TRM_CODIGO"];
                        tranformacion.TRM_NOMBRE = (String)reader["TRM_NOMBRE"];
                        tranformacion.MENSAJE_DESTINO = 
                            new MENSAJE() { MEN_CODIGO = (int)reader["MEN_CODIGO"] };
                        tranformacion.MENSAJE_DESTINO.GRUPO_MENSAJE = 
                            new GRUPO_MENSAJE() { GMJ_CODIGO = (int)reader["GMJ_CODIGO"] };
                        tranformacion.MENSAJE_DESTINO.GRUPO_MENSAJE.TIPO_MENSAJE =
                            new TIPO_MENSAJE() { TMJ_CODIGO = (int)reader["TMJ_CODIGO"] };
                    }
                    conexion.Close();
                }
            }
            return tranformacion;
        }

        public static EstadoOperacion insertarTransformacion(TRANSFORMACION Transformacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {

                    using (contexto.CreateConeccionScope())
                    {
                        Transformacion.TRM_CODIGO = (from c in contexto.TRANSFORMACION
                                                     orderby c.TRM_CODIGO descending
                                                     select c.TRM_CODIGO).FirstOrDefault() + 1;

                        string query =
                            "INSERT INTO TRANSFORMACION" +
                                       "(TRM_CODIGO" +
                                       ",TRM_NOMBRE" +
                                       ",MEN_CODIGO_MENSAJE_ORIGEN" +
                                       ",MEN_CODIGO_MENSAJE_DESTINO)" +
                                 "VALUES" +
                                       "(@codigo" +
                                       ",@nombre" +
                                       ",@mensajeorigen_codigo" +
                                       ",@mensajedestino_codigo)";

                        DbCommand Comando = crearComando(contexto, Transformacion, query);

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

        public static EstadoOperacion modificarTransformacion(TRANSFORMACION Transformacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE TRANSFORMACION" +
                           " SET TRM_NOMBRE = @nombre" +
                              ",MEN_CODIGO_MENSAJE_ORIGEN = @mensajeorigen_codigo" +
                              ",MEN_CODIGO_MENSAJE_DESTINO = @mensajedestino_codigo" +
                         " WHERE TRM_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, Transformacion, query);

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


        public static EstadoOperacion eliminarTransformacion(TRANSFORMACION Transformacion)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM TRANSFORMACION" +
                                 " WHERE TRM_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Transformacion.TRM_CODIGO));

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

        private static DbCommand crearComando(Switch contexto, TRANSFORMACION Transformacion, string query)
        {
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Transformacion.TRM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Transformacion.TRM_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensajeorigen_codigo", Transformacion.MENSAJE_ORIGEN.MEN_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensajedestino_codigo", Transformacion.MENSAJE_DESTINO.MEN_CODIGO));
            return Comando;
        }

    }
}
