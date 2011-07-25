using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Terminales
{
    public class EstadoTerminalDA
    {
        public static List<ESTADO_TERMINAL> obtenerEstadoTerminal()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ESTADO_TERMINAL.MergeOption = MergeOption.NoTracking;
                return contexto.ESTADO_TERMINAL.ToList<ESTADO_TERMINAL>();
            }
        }

        public static ESTADO_TERMINAL obtenerEstadoTerminal(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.ESTADO_TERMINAL.MergeOption = MergeOption.NoTracking;
                return contexto.ESTADO_TERMINAL.Where(o => o.EST_CODIGO == codigo).FirstOrDefault<ESTADO_TERMINAL>();
            }
        }

        public static EstadoOperacion insertarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int nuevoId = (from c in contexto.ESTADO_TERMINAL
                                       orderby c.EST_CODIGO descending
                                       select c.EST_CODIGO).FirstOrDefault() + 1;

                        estadoTerminal.EST_CODIGO = nuevoId;

                        string query =
                            "INSERT INTO ESTADO_TERMINAL" +
                            "(EST_CODIGO" +
                            ",EST_NOMBRE)" +
                            "VALUES" +
                            "(@codigo" +
                            ",@descripcion)";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", estadoTerminal.EST_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", estadoTerminal.EST_NOMBRE));

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

        public static EstadoOperacion modificarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE ESTADO_TERMINAL" +
                            " SET " +
                            "EST_NOMBRE = @descripcion " +
                            "WHERE EST_CODIGO=@codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", estadoTerminal.EST_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@descripcion", estadoTerminal.EST_NOMBRE));

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

        public static EstadoOperacion eliminarEstadoTerminal(ESTADO_TERMINAL estadoTerminal)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM ESTADO_TERMINAL" +
                            " WHERE EST_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", estadoTerminal.EST_CODIGO));

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
                    return new EstadoOperacion(false, "El Estado Terminal esta asignado a un Terminal y no se puede eliminar", e, true);
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
    }
}
