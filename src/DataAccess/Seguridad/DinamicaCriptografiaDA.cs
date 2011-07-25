using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Seguridad
{
    public sealed class DinamicaCriptografiaDA
    {
        public static List<DINAMICA_CRIPTOGRAFIA> obtenerDinamicaCriptografia()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.DINAMICA_CRIPTOGRAFIA.MergeOption = MergeOption.NoTracking;
                return contexto.DINAMICA_CRIPTOGRAFIA.ToList<DINAMICA_CRIPTOGRAFIA>();
            }
        }

        public static DINAMICA_CRIPTOGRAFIA obtenerDinamicaCriptografia(int codigoDinamicaCriptografia)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.DINAMICA_CRIPTOGRAFIA.MergeOption = MergeOption.NoTracking;
                return contexto.DINAMICA_CRIPTOGRAFIA.Include("MENSAJE").Include("MENSAJE.GRUPO_MENSAJE")
                    .Where(o => o.DNC_CODIGO == codigoDinamicaCriptografia).FirstOrDefault<DINAMICA_CRIPTOGRAFIA>();
            }
        }

        public static List<DINAMICA_CRIPTOGRAFIA> obtenerDinamicaCriptografiaPorMensaje(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.DINAMICA_CRIPTOGRAFIA.MergeOption = MergeOption.NoTracking;
                return contexto.DINAMICA_CRIPTOGRAFIA
                    .Where(o => o.MENSAJE.MEN_CODIGO == codigoMensaje).ToList<DINAMICA_CRIPTOGRAFIA>();
            }
        }

        public static EstadoOperacion insertarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {

                    using (contexto.CreateConeccionScope())
                    {
                        DinamicaCriptografia.DNC_CODIGO = (from c in contexto.DINAMICA_CRIPTOGRAFIA
                                                           orderby c.DNC_CODIGO descending
                                                           select c.DNC_CODIGO).FirstOrDefault() + 1;

                        string query =
                                "INSERT INTO DINAMICA_CRIPTOGRAFIA" +
                               "(DNC_CODIGO" +
                               ",DNC_NOMBRE" +
                               ",DNC_TIPO" +
                               ",MEN_CODIGO)" +
                            "VALUES " +
                               "(@codigo" +
                               ",@nombre" +
                               ",@tipo" +
                               ",@mensaje_codigo)";

                        DbCommand Comando = crearComando(contexto, DinamicaCriptografia, query);

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

        public static EstadoOperacion modificarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE DINAMICA_CRIPTOGRAFIA" +
                               " SET DNC_NOMBRE = @nombre" +
                                  ",DNC_TIPO = @tipo" +
                             " WHERE DNC_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, DinamicaCriptografia, query);

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

        public static EstadoOperacion eliminarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM DINAMICA_CRIPTOGRAFIA" +
                                 " WHERE DNC_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", DinamicaCriptografia.DNC_CODIGO));

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
                    return new EstadoOperacion(false, "La Dinamica Criptografia tiene Campos Criptografía y no se puede eliminar", e, true);
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

        private static DbCommand crearComando(dbSwitch contexto, DINAMICA_CRIPTOGRAFIA DinamicaCriptografia, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", DinamicaCriptografia.DNC_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", DinamicaCriptografia.DNC_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipo", DinamicaCriptografia.DNC_TIPO));
            if (DinamicaCriptografia.MENSAJE !=null)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", DinamicaCriptografia.MENSAJE.MEN_CODIGO));
            }

            return Comando;
        }
    }
}
