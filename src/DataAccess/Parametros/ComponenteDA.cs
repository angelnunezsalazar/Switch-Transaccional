using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Parametros
{
    public class ComponenteDA
    {
        public static List<COMPONENTE> obtenerComponente()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.COMPONENTE.MergeOption = MergeOption.NoTracking;
                var lista= contexto.COMPONENTE.ToList<COMPONENTE>();
                return lista;
            }
        }

        public static EstadoOperacion insertarComponente(COMPONENTE componente)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idComponente= (from c in contexto.COMPONENTE
                                          orderby c.COM_CODIGO descending
                                           select c.COM_CODIGO).FirstOrDefault() + 1;

                        componente.COM_CODIGO = idComponente;

                        string query = "INSERT INTO COMPONENTE" +
                            "(COM_CODIGO" +
                            ",COM_NOMBRE" +
                            ",COM_ARCHIVO)" +
                            "VALUES(@codigo,@nombre,@archivo)";

                        DbCommand Comando = crearComando(contexto, componente, query);

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

        public static EstadoOperacion modificarComponente(COMPONENTE componente)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query = "UPDATE COMPONENTE" +
                               " SET COM_ARCHIVO = @archivo" +
                            " WHERE COM_CODIGO = @codigo";

                        DbCommand Comando = crearComando(contexto, componente, query);

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


        public static EstadoOperacion eliminarComponente(COMPONENTE componente)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM COMPONENTE" +
                                 " WHERE COM_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", componente.COM_CODIGO));

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

        private static DbCommand crearComando(dbSwitch contexto, COMPONENTE Componente, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Componente.COM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Componente.COM_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@archivo", Componente.COM_ARCHIVO));

            return Comando;
        }
    }
}
