using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;

namespace DataAccess.Seguridad
{
    public sealed class CriptografiaCampoDA
    {
        public static List<CRIPTOGRAFIA_CAMPO> obtenerCriptografiaCampo()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CRIPTOGRAFIA_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.CRIPTOGRAFIA_CAMPO.Include("CAMPO_RESULTADO").ToList<CRIPTOGRAFIA_CAMPO>();
            }
        }

        public static List<CRIPTOGRAFIA_CAMPO> obtenerCriptografiaCampo(int codigoDinamicaCriptografia)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CRIPTOGRAFIA_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.CRIPTOGRAFIA_CAMPO.Include("CAMPO_LLAVE_1").Include("CAMPO_LLAVE_2").Include("CAMPO_RESULTADO")
                    .Where(o => o.DINAMICA_CRIPTOGRAFIA.DNC_CODIGO == codigoDinamicaCriptografia).ToList<CRIPTOGRAFIA_CAMPO>();
            }
        }

        public static CRIPTOGRAFIA_CAMPO obtenerCriptografiaCampo(int codigoDinamicaCriptografia,int codigoCriptografiaCampo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CRIPTOGRAFIA_CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.CRIPTOGRAFIA_CAMPO.Include("CAMPO_LLAVE_1").Include("CAMPO_LLAVE_2").Include("CAMPO_RESULTADO")
                    .Where(o => o.DNC_CODIGO == codigoDinamicaCriptografia 
                        && o.CRC_CODIGO==codigoCriptografiaCampo).FirstOrDefault<CRIPTOGRAFIA_CAMPO>();
            }
        }


        public static EstadoOperacion insertarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idCriptografiaCampo = (from c in contexto.CRIPTOGRAFIA_CAMPO
                                                   where c.DINAMICA_CRIPTOGRAFIA.DNC_CODIGO ==
                                                        CriptografiaCampo.DINAMICA_CRIPTOGRAFIA.DNC_CODIGO
                                                   orderby c.CRC_CODIGO descending
                                                   select c.CRC_CODIGO).FirstOrDefault() + 1;

                        CriptografiaCampo.CRC_CODIGO = idCriptografiaCampo;

                        string insert = "INSERT INTO CRIPTOGRAFIA_CAMPO" +
                                       "(CRC_CODIGO" +
                                       ",DNC_CODIGO" +
                                       ",CAM_CODIGO_RESULTADO" +
                                       ",MEN_CODIGO_RESULTADO" +
                                       ",CAM_CODIGO_LLAVE_1" +
                                       ",MEN_CODIGO_LLAVE_1" +
                                       ",CAM_CODIGO_LLAVE_2" +
                                       ",MEN_CODIGO_LLAVE_2" +
                                       ",CRC_LLAVE_1" +
                                       ",CRC_LLAVE_2" +
                                       ",CRC_TIPO_LLAVE_1" +
                                       ",CRC_TIPO_LLAVE_2" +
                                       ",CRC_SEGUNDA_LLAVE" +
                                       ",CRC_ALGORITMO" +
                                       ",CRC_OPERACION_LLAVE)" +
                                 " VALUES " +
                                       "(@codigo" +
                                       ",@dinamica_codigo" +
                                       ",@campoResultado_codigo" +
                                       ",@mensajeResultado_codigo" +
                                       ",@campoLlave1_codigo" +
                                       ",@mensajeLlave1_codigo" +
                                       ",@campoLlave2_codigo" +
                                       ",@mensajeLlave2_codigo" +
                                       ",@llave1" +
                                       ",@llave2" +
                                       ",@tipoLlave1" +
                                       ",@tipoLlave2" +
                                       ",@segundaLlave" +
                                       ",@algoritmo" +
                                       ",@operacionLlave)";

                        DbCommand Comando = crearComando(contexto, CriptografiaCampo, insert);

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

        public static EstadoOperacion modificarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                        "UPDATE dbSwitch.dbo.CRIPTOGRAFIA_CAMPO" +
                           " SET CAM_CODIGO_RESULTADO = @campoResultado_codigo" +
                              ",MEN_CODIGO_RESULTADO = @mensajeResultado_codigo" +
                              ",CAM_CODIGO_LLAVE_1 = @campoLlave1_codigo" +
                              ",MEN_CODIGO_LLAVE_1 = @mensajeLlave1_codigo" +
                              ",CAM_CODIGO_LLAVE_2 = @campoLlave2_codigo" +
                              ",MEN_CODIGO_LLAVE_2 = @mensajeLlave2_codigo" +
                              ",CRC_LLAVE_1 = @llave1" +
                              ",CRC_LLAVE_2 = @llave2" +
                              ",CRC_TIPO_LLAVE_1 = @tipoLlave1" +
                              ",CRC_TIPO_LLAVE_2 = @tipoLlave2" +
                              ",CRC_SEGUNDA_LLAVE = @segundaLlave" +
                              ",CRC_ALGORITMO = @algoritmo" +
                              ",CRC_OPERACION_LLAVE = @operacionLlave" +
                         " WHERE CRC_CODIGO = @codigo  AND " +
                              "DNC_CODIGO = @dinamica_codigo";

                        DbCommand Comando = crearComando(contexto, CriptografiaCampo, query);
                       
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


        public static EstadoOperacion eliminarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM CRIPTOGRAFIA_CAMPO" +
                            " WHERE CRC_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", CriptografiaCampo.CRC_CODIGO));

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

        private static DbCommand crearComando(dbSwitch contexto, CRIPTOGRAFIA_CAMPO CriptografiaCampo, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", CriptografiaCampo.CRC_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@dinamica_codigo", CriptografiaCampo.DINAMICA_CRIPTOGRAFIA.DNC_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@campoResultado_codigo", CriptografiaCampo.CAMPO_RESULTADO.CAM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensajeResultado_codigo", CriptografiaCampo.CAMPO_RESULTADO.MEN_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipoLlave1", CriptografiaCampo.CRC_TIPO_LLAVE_1));
            Comando.Parameters.Add(Factoria.CrearParametro("@algoritmo", CriptografiaCampo.CRC_ALGORITMO));

            if (CriptografiaCampo.CRC_TIPO_LLAVE_1 == (int)EnumTipoLlave.Campo)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoLlave1_codigo", CriptografiaCampo.CAMPO_LLAVE_1.CAM_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeLlave1_codigo", CriptografiaCampo.CAMPO_LLAVE_1.MEN_CODIGO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoLlave1_codigo", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeLlave1_codigo", DBNull.Value));
            }

            if (CriptografiaCampo.CRC_TIPO_LLAVE_1 == (int)EnumTipoLlave.LlaveFija)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@llave1", CriptografiaCampo.CRC_LLAVE_1));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@llave1", DBNull.Value));
            }

            Comando.Parameters.Add(Factoria.CrearParametro("@segundaLlave", CriptografiaCampo.CRC_SEGUNDA_LLAVE));

            if (CriptografiaCampo.CRC_SEGUNDA_LLAVE)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@tipoLlave2", CriptografiaCampo.CRC_TIPO_LLAVE_2));
                Comando.Parameters.Add(Factoria.CrearParametro("@operacionLlave", CriptografiaCampo.CRC_OPERACION_LLAVE));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@tipoLlave2", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@operacionLlave", DBNull.Value));
            }

            if (CriptografiaCampo.CRC_SEGUNDA_LLAVE && CriptografiaCampo.CRC_TIPO_LLAVE_2.Value == (int)EnumTipoLlave.Campo)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoLlave2_codigo", CriptografiaCampo.CAMPO_LLAVE_2.CAM_CODIGO));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeLlave2_codigo", CriptografiaCampo.CAMPO_LLAVE_2.MEN_CODIGO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@campoLlave2_codigo", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@mensajeLlave2_codigo", DBNull.Value));
            }

            if (CriptografiaCampo.CRC_SEGUNDA_LLAVE && CriptografiaCampo.CRC_TIPO_LLAVE_2.Value == (int)EnumTipoLlave.LlaveFija)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@llave2", CriptografiaCampo.CRC_LLAVE_2));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@llave2", DBNull.Value));
            }

            return Comando;
        }
    }
}
