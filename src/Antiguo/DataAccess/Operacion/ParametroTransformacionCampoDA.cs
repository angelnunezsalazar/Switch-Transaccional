using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;

namespace DataAccess.Mensajeria
{
    public sealed class ParametroTransformacionCampoDA
    {
        public static List<PARAMETRO_TRANSFORMACION_CAMPO> obtenerParametroTransformacionCampo()
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from p in contexto.PARAMETRO_TRANSFORMACION_CAMPO
                        select p).ToList<PARAMETRO_TRANSFORMACION_CAMPO>();
            }
        }

        public static List<PARAMETRO_TRANSFORMACION_CAMPO> obtenerParametroTransformacionCampo(int codigoTransformacion, int codigoMensaje, int codigoCampo)
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                var listaParametros= (from p in contexto.PARAMETRO_TRANSFORMACION_CAMPO
                                      .Include("CAMPO")
                                      .Include("TABLA")
                                      .Include("COLUMNA_ORIGEN")
                                      .Include("COLUMNA_DESTINO")
                        where p.TRANSFORMACION_CAMPO.TRM_CODIGO == codigoTransformacion &&
                        p.TRANSFORMACION_CAMPO.MEN_CODIGO_MENSAJE_DESTINO == codigoMensaje &&
                        p.TRANSFORMACION_CAMPO.CAM_CODIGO_CAMPO_DESTINO == codigoCampo
                        select p).ToList<PARAMETRO_TRANSFORMACION_CAMPO>();

                return listaParametros;
            }
        }

        public static PARAMETRO_TRANSFORMACION_CAMPO obtenerParametroTransformacionCampo(int codigoParametro)
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                var listaParametros = (from p in contexto.PARAMETRO_TRANSFORMACION_CAMPO
                                       .Include("CAMPO")
                                       .Include("TABLA")
                                       .Include("COLUMNA_ORIGEN")
                                       .Include("COLUMNA_DESTINO")
                                       where p.PTC_CODIGO==codigoParametro
                                       select p).FirstOrDefault<PARAMETRO_TRANSFORMACION_CAMPO>();

                return listaParametros;
            }
        }

        public static EstadoOperacion insertarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            try
            {
                using (Switch contexto = new Switch())
                {

                    using (contexto.CreateConeccionScope())
                    {
                        parametroTransformacionCampo.PTC_CODIGO = (from c in contexto.PARAMETRO_TRANSFORMACION_CAMPO
                                                                   orderby c.PTC_CODIGO descending
                                                                   select c.PTC_CODIGO).FirstOrDefault() + 1;

                      
                        string query =
                            "INSERT INTO PARAMETRO_TRANSFORMACION_CAMPO" +
                                       "(PTC_CODIGO" +
                                       ",TRM_CODIGO" +
                                       ",CAM_CODIGO_CAMPO_DESTINO" +
                                       ",MEN_CODIGO_MENSAJE_DESTINO" +
                                       ",CAM_CODIGO_CAMPO_ORIGEN" +
                                       ",MEN_CODIGO_MENSAJE_ORIGEN" +
                                       ",PTC_POSICION_INICIAL" +
                                       ",PTC_LONGITUD" +
                                       ",TBL_CODIGO" +
                                       ",COL_CODIGO_ORIGEN" +
                                       ",COL_CODIGO_DESTINO" +
                                       ",PTC_TIPO)" +
                                 "VALUES" +
                                       "(@codigo" +
                                       ",@transformacion_codigo" +
                                       ",@campoDestino_codigo" +
                                       ",@mensajeDestino_codigo" +
                                       ",@campoOrigen_codigo" +
                                       ",@mensajeOrigen_codigo" +
                                       ",@posicionInicial" +
                                       ",@longitud" +
                                       ",@tabla" +
                                       ",@columnaOrigen_codigo" +
                                       ",@columnaDestino_codigo" +
                                       ",@tipoParametroTransformacionCampo_codigo)";

                        DbCommand Comando = crearComando(contexto, parametroTransformacionCampo, query);

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

        public static EstadoOperacion modificarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            try
            {
                using (Switch contexto = new Switch())
                {

                    using (contexto.CreateConeccionScope())
                    {
                        


                        string query ="UPDATE PARAMETRO_TRANSFORMACION_CAMPO"+
                                       " SET CAM_CODIGO_CAMPO_ORIGEN = @campoOrigen_codigo" +
                                          ",MEN_CODIGO_MENSAJE_ORIGEN = @mensajeOrigen_codigo" +
                                          ",PTC_POSICION_INICIAL = @posicionInicial" +
                                          ",PTC_LONGITUD = @longitud" +
                                          ",PTC_TIPO = @tipoParametroTransformacionCampo_codigo" +
                                          ",TBL_CODIGO = @tabla" +
                                          ",COL_CODIGO_ORIGEN = @columnaOrigen_codigo" +
                                          ",COL_CODIGO_DESTINO = @columnaDestino_codigo" +
                                     " WHERE PTC_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, parametroTransformacionCampo, query);

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


        public static EstadoOperacion eliminarParametroTransformacionCampo(PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM PARAMETRO_TRANSFORMACION_CAMPO" +
                                 " WHERE PTC_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", parametroTransformacionCampo.PTC_CODIGO));

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


        private static DbCommand crearComando(Switch contexto, PARAMETRO_TRANSFORMACION_CAMPO parametroTransformacionCampo, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", parametroTransformacionCampo.PTC_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@transformacion_codigo", parametroTransformacionCampo.TRANSFORMACION_CAMPO.TRM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@campoDestino_codigo", parametroTransformacionCampo.TRANSFORMACION_CAMPO.CAM_CODIGO_CAMPO_DESTINO));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensajeDestino_codigo", parametroTransformacionCampo.TRANSFORMACION_CAMPO.MEN_CODIGO_MENSAJE_DESTINO));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipoParametroTransformacionCampo_codigo", parametroTransformacionCampo.PTC_TIPO));


            if (parametroTransformacionCampo.PTC_TIPO == (int)EnumTipoParametroTransformacionCampo.CampoOrigen ||
                parametroTransformacionCampo.PTC_TIPO == (int)EnumTipoParametroTransformacionCampo.Tabla)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoOrigen_codigo", parametroTransformacionCampo.CAMPO.CAM_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeOrigen_codigo", parametroTransformacionCampo.CAMPO.MEN_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@posicionInicial", parametroTransformacionCampo.PTC_POSICION_INICIAL));
                Comando.Parameters.Add(Factoria.CrearParametro("@longitud", parametroTransformacionCampo.PTC_LONGITUD));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoOrigen_codigo", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeOrigen_codigo", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@posicionInicial", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@longitud", DBNull.Value));
            }

            if (parametroTransformacionCampo.PTC_TIPO == (int)EnumTipoParametroTransformacionCampo.Tabla)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@tabla", parametroTransformacionCampo.TABLA.TBL_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@columnaOrigen_codigo", parametroTransformacionCampo.COLUMNA_ORIGEN.COL_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@columnaDestino_codigo", parametroTransformacionCampo.COLUMNA_DESTINO.COL_CODIGO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@tabla", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@columnaOrigen_codigo", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@columnaDestino_codigo", DBNull.Value));
            
            }

            return Comando;
        }
    }
}
