using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Mensajeria
{
    public sealed class ColumnaDA
    {
        public static List<COLUMNA> obtenerColumna(int codigoTabla)
        {
            using (Switch contexto = new Switch())
            {
                contexto.COLUMNA.MergeOption = MergeOption.NoTracking;
                return contexto.COLUMNA.Include("TIPO_DATO_COLUMNA").Where(o => o.TABLA.TBL_CODIGO == codigoTabla).ToList<COLUMNA>();
            }
        }

        public static COLUMNA obtenerColumnaPorCodigo(int codigoColumna)
        {
            using (Switch contexto = new Switch())
            {
                contexto.COLUMNA.MergeOption = MergeOption.NoTracking;
                return contexto.COLUMNA.Where(o => o.COL_CODIGO == codigoColumna).FirstOrDefault();
            }
        }
        public static COLUMNA obtenerColumnaPorNombre(string nombreColumna)
        {
            using (Switch contexto = new Switch())
            {
                contexto.COLUMNA.MergeOption = MergeOption.NoTracking;
                return contexto.COLUMNA.Where(o => o.COL_NOMBRE == nombreColumna).FirstOrDefault();
            }
        }
        public static EstadoOperacion insertarColumna(COLUMNA Columna)
        {
            try
            {
                using (Switch contexto = new Switch())
                {

                    using (contexto.CreateConeccionScope())
                    {
                        Columna.COL_CODIGO = 0;
                        string query = "InsertarColumna";

                        DbCommand Comando = crearComando(contexto, Columna, query);

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }
                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException ex)
            {
                return new EstadoOperacion(false, ex.Message, ex, false);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        public static EstadoOperacion modificarColumna(COLUMNA Columna)
        {
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "ModificarColumna";

                        DbCommand Comando = crearComando(contexto, Columna, query);

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException ex)
            {
                return new EstadoOperacion(false, ex.Message, ex, false);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }


        public static EstadoOperacion eliminarColumna(COLUMNA Columna)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (Switch contexto = new Switch())
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "EliminarColumna";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Columna.COL_CODIGO));

                        if (Comando.ExecuteNonQuery() != 1)
                        {
                            return new EstadoOperacion(false, null, null);
                        }

                        return new EstadoOperacion(true, null, null);
                    }
                }
            }
            catch (DbException dbex)
            {
                return new EstadoOperacion(false, dbex.Message, dbex);
            }
            catch (Exception e)
            {

                return new EstadoOperacion(false, e.Message, e);
            }
        }

        private static DbCommand crearComando(Switch contexto, COLUMNA Columna, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.StoredProcedure);

            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Columna.COL_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Columna.COL_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@longitud", Columna.COL_LONGITUD));
            Comando.Parameters.Add(Factoria.CrearParametro("@tabla", Columna.TABLA != null ? Columna.TABLA.TBL_CODIGO : 0));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipoDatoColumna", Columna.TIPO_DATO_COLUMNA.TDC_CODIGO));

            return Comando;
        }
    }
}
