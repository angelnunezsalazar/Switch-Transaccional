using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Mensajeria
{
    public sealed class ReglaMensajeTransaccionalDA
    {
        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerMensajeTransaccional()
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL.ToList<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

        public static REGLA_MENSAJE_TRANSACCIONAL obtenerMensajeTransaccional(int codigoReglaMensajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL
                    .Where(o => o.RMT_CODIGO == codigoReglaMensajeTransaccional).FirstOrDefault<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

        public static List<REGLA_MENSAJE_TRANSACCIONAL> obtenerReglaMensajeTransaccionalPorMensajeTransaccional(int codigoMensajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.REGLA_MENSAJE_TRANSACCIONAL.MergeOption = MergeOption.NoTracking;
                return contexto.REGLA_MENSAJE_TRANSACCIONAL.Include("CAMPO").Include("CAMPO.TIPO_DATO")
                    .Where(o => o.MENSAJE_TRANSACCIONAL.MTR_CODIGO == codigoMensajeTransaccional).ToList<REGLA_MENSAJE_TRANSACCIONAL>();
            }
        }

        public static EstadoOperacion insertarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        ReglaMensajeTransaccional.RMT_CODIGO = (from c in contexto.REGLA_MENSAJE_TRANSACCIONAL
                                                                orderby c.RMT_CODIGO descending
                                                                select c.RMT_CODIGO).FirstOrDefault() + 1;

                        string query =
                                "INSERT INTO REGLA_MENSAJE_TRANSACCIONAL" +
                                           "(RMT_CODIGO" +
                                           ",RMT_POSICION_INICIAL" +
                                           ",RMT_LONGITUD" +
                                           ",RMT_VALOR" +
                                           ",CAM_CODIGO" +
                                           ",MEN_CODIGO" +
                                           ",MTR_CODIGO)" +
                                     "VALUES" +
                                           "(@codigo" +
                                           ",@posicioninicial" +
                                           ",@longitud" +
                                           ",@valor" +
                                           ",@campo_codigo" +
                                           ",@mensaje_codigo" +
                                           ",@mensajetransaccional_codigo)";

                        DbCommand Comando = crearComando(contexto, ReglaMensajeTransaccional, query);

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

        public static EstadoOperacion modificarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE REGLA_MENSAJE_TRANSACCIONAL" +
                               " SET RMT_VALOR = @valor" +
                            " WHERE RMT_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, ReglaMensajeTransaccional, query);

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


        public static EstadoOperacion eliminarReglaMensajeTransaccional(REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM REGLA_MENSAJE_TRANSACCIONAL" +
                                 " WHERE RMT_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", ReglaMensajeTransaccional.RMT_CODIGO));

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

        private static DbCommand crearComando(Switch contexto, REGLA_MENSAJE_TRANSACCIONAL ReglaMensajeTransaccional, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", ReglaMensajeTransaccional.RMT_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@posicioninicial", Util.NullableToDbValue<int>(ReglaMensajeTransaccional.RMT_POSICION_INICIAL)));
            Comando.Parameters.Add(Factoria.CrearParametro("@longitud", Util.NullableToDbValue<int>(ReglaMensajeTransaccional.RMT_LONGITUD)));
            Comando.Parameters.Add(Factoria.CrearParametro("@valor", ReglaMensajeTransaccional.RMT_VALOR));
            if (ReglaMensajeTransaccional.CAMPO != null)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campo_codigo", ReglaMensajeTransaccional.CAMPO.CAM_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", ReglaMensajeTransaccional.CAMPO.MEN_CODIGO));
            }
            if (ReglaMensajeTransaccional.MENSAJE_TRANSACCIONAL != null)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajetransaccional_codigo", 
                            ReglaMensajeTransaccional.MENSAJE_TRANSACCIONAL.MTR_CODIGO));
            }


            return Comando;
        }

    }
}
