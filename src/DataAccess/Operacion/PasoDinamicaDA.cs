using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;

namespace DataAccess.Operacion
{
    public static class PasoDinamicaDA
    {
        private static DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        public static List<PASO_DINAMICA> ObtenerPasoDinamica(int mensaje, string valores)
        {
            List<PASO_DINAMICA> listaPasos = new List<PASO_DINAMICA>();
            using (DbConnection conexion = Factoria.CrearConexion(CadenaConexion.getInstance().conexion))
            {
                using (DbCommand comando = Factoria.CrearComando("ObtenerPasosDinamica", conexion))
                {
                    comando.Parameters.Add(Factoria.CrearParametro("@mensaje", mensaje));
                    comando.Parameters.Add(Factoria.CrearParametro("@valores", valores));
                    comando.Parameters.Add(Factoria.CrearParametro("@resultado", ParameterDirection.Output));
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    DbDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        PASO_DINAMICA paso = new PASO_DINAMICA
                                                 {
                                                     PDT_FUNCIONALIDAD = (int)reader["PDT_FUNCIONALIDAD"],
                                                     PDT_FIN = (bool)reader["PDT_FIN"],
                                                     PDT_NUMERO = (string)reader["PDT_NUMERO"],
                                                     PDT_PASO = (int)reader["PDT_PASO"],
                                                     EntidadComunicacion = new EntidadComunicacion
                                                                                {
                                                                                    EDC_COLA = DBNull.Value == reader["EDC_COLA"] ?
                                                                                                null : (string)reader["EDC_COLA"]
                                                                                }

                                                 };
                        listaPasos.Add(paso);
                    }
                    conexion.Close();
                }
            }
            return listaPasos;
        }

        public static List<PASO_DINAMICA> obtenerDinamicaTransaccional(int codigoMensajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from p in contexto.PASO_DINAMICA
                        where p.MENSAJE_TRANSACCIONAL.MTR_CODIGO == codigoMensajeTransaccional
                        select p).ToList();
            }
        }

        public static PASO_DINAMICA obtenerPasoDinamica(int codigoPasoDinamica)
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from p in contexto.PASO_DINAMICA
                        where p.PDT_CODIGO == codigoPasoDinamica
                        select p).FirstOrDefault();
            }
        }

        public static List<PASO_DINAMICA> obtenerPasosHijo(string numeroPaso, int codigoMensajeTransaccional)
        {
            string rama1 = numeroPaso + "1";
            string rama2 = numeroPaso + "2";
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from p in contexto.PASO_DINAMICA
                        where p.MENSAJE_TRANSACCIONAL.MTR_CODIGO == codigoMensajeTransaccional &&
                        (p.PDT_NUMERO == rama1 || p.PDT_NUMERO == rama2)
                        select p).ToList();
            }
        }

        public static PASO_DINAMICA obtenerUltimoPasoDinamica(int codigoMenajeTransaccional)
        {
            using (Switch contexto = new Switch())
            {
                contexto.PARAMETRO_TRANSFORMACION_CAMPO.MergeOption = MergeOption.NoTracking;
                return (from p in contexto.PASO_DINAMICA
                        where p.MENSAJE_TRANSACCIONAL.MTR_CODIGO == codigoMenajeTransaccional
                        orderby p.PDT_NUMERO descending
                        select p).FirstOrDefault();
            }
        }


        public static EstadoOperacion insertarPasoDinamica(PASO_DINAMICA pasoDinamica)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idPasoDinamica = (from c in contexto.PASO_DINAMICA
                                              orderby c.PDT_CODIGO descending
                                              select c.PDT_CODIGO).FirstOrDefault() + 1;

                        pasoDinamica.PDT_CODIGO = idPasoDinamica;

                        string insert = "INSERT INTO [PASO_DINAMICA]" +
                                       "([PDT_CODIGO]" +
                                       ",[PDT_FUNCIONALIDAD]" +
                                       ",[PDT_NUMERO]" +
                                       ",[PDT_FIN]" +
                                       ",[PDT_PASO]" +
                                       ",[MTR_CODIGO]" +
                                       ",[PDT_REINTENTOS]" +
                                       ",[PDT_INFORMACION_ADICIONAL]" +
                                       ",[EDC_CODIGO])" +
                                 " VALUES" +
                                       "(@codigo" +
                                       ",@funcionalidad" +
                                       ",@numero" +
                                       ",@fin" +
                                       ",@paso" +
                                       ",@mensajeTransaccional" +
                                       ",@reintentos" +
                                       ",@informacionAdicional" +
                                       ",@entidadComunicacion)";

                        DbCommand Comando = contexto.CreateCommand(insert, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", pasoDinamica.PDT_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@funcionalidad", pasoDinamica.PDT_FUNCIONALIDAD));
                        Comando.Parameters.Add(Factoria.CrearParametro("@numero", pasoDinamica.PDT_NUMERO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@fin", pasoDinamica.PDT_FIN));
                        Comando.Parameters.Add(Factoria.CrearParametro("@paso", pasoDinamica.PDT_PASO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@informacionAdicional", pasoDinamica.PDT_INFORMACION_ADICIONAL ?? "-"));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensajeTransaccional", pasoDinamica.MENSAJE_TRANSACCIONAL.MTR_CODIGO));


                        if (pasoDinamica.PDT_FUNCIONALIDAD == (int)EnumTipoFuncionalidad.Enviar ||
                            pasoDinamica.PDT_FUNCIONALIDAD == (int)EnumTipoFuncionalidad.Recibir)
                        {
                            Comando.Parameters.Add(Factoria.CrearParametro("@reintentos", pasoDinamica.PDT_REINTENTOS));
                            Comando.Parameters.Add(Factoria.CrearParametro("@entidadComunicacion", pasoDinamica.EntidadComunicacion.EDC_CODIGO));
                        }
                        else
                        {
                            Comando.Parameters.Add(Factoria.CrearParametro("@reintentos", DBNull.Value));
                            Comando.Parameters.Add(Factoria.CrearParametro("@entidadComunicacion", DBNull.Value));
                        }

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

        public static EstadoOperacion eliminarPasoDinamica(PASO_DINAMICA pasoDinamica)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM PASO_DINAMICA" +
                                 " WHERE PDT_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", pasoDinamica.PDT_CODIGO));

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

    }
}
