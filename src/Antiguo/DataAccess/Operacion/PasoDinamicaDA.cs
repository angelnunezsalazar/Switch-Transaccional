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
    }
}
