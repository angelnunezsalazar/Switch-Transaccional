using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using System.Text;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Terminales
{
    public class TerminalDA
    {
        public static TERMINAL obtenerTerminal(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TERMINAL.MergeOption = MergeOption.NoTracking;
                return contexto.TERMINAL.Include("ENTIDAD_COMUNICACION").Include("ESTADO_TERMINAL").Include("PUNTO_SERVICIO").Where(o => o.TRM_CODIGO == codigo).FirstOrDefault<TERMINAL>();
            }
        }

        public static List<TERMINAL> obtenerTerminal(string serial, int entidadComunicacion, int estadoTerminal)
        {
            StringBuilder query = new StringBuilder("");
            query.Append("SELECT VALUE TER FROM TERMINAL AS TER WHERE " +
                         "TER.TRM_SERIAL LIKE'%'+ @serial +'%' ");

            if (entidadComunicacion != 0)
            {
                query.Append("AND TER.ENTIDAD_COMUNICACION.EDC_CODIGO = @entidadComunicacion ");
            }
            if (estadoTerminal != 0)
            {
                query.Append("AND TER.ESTADO_TERMINAL.EST_CODIGO = @estadoTerminal ");
            }

            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.TERMINAL.MergeOption = MergeOption.NoTracking;
                ObjectQuery<TERMINAL> terminal = new ObjectQuery<TERMINAL>(query.ToString(), contexto);

                terminal.Parameters.Add(new ObjectParameter("serial", serial));
                if (entidadComunicacion != 0)
                {
                    terminal.Parameters.Add(new ObjectParameter("entidadComunicacion", entidadComunicacion));
                }
                if (estadoTerminal != 0)
                {
                    terminal.Parameters.Add(new ObjectParameter("estadoTerminal", estadoTerminal));
                }

                return terminal.Include("ENTIDAD_COMUNICACION").Include("ESTADO_TERMINAL").Include("PUNTO_SERVICIO").ToList<TERMINAL>();
            }
        }

        public static EstadoOperacion insertarTerminal(TERMINAL terminal)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idTerminal = (from c in contexto.TERMINAL
                                          orderby c.TRM_CODIGO descending
                                          select c.TRM_CODIGO).FirstOrDefault() + 1;
                        terminal.TRM_CODIGO = idTerminal;

                        string insert = "INSERT INTO TERMINAL" +
                            "(TRM_CODIGO" +
                            ",TRM_SERIAL" +
                            ",TRM_FECHA_REGISTRO" +
                            ",EST_CODIGO" +
                            ",PSR_CODIGO" +
                            ",EDC_CODIGO)" +
                            "VALUES(@codigo,@serial,@fechaRegistro,@estado,@puntoServicio,@entidadComunicacion)";

                        DbCommand ComandoInsert = contexto.CreateCommand(insert, CommandType.Text);

                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@codigo", terminal.TRM_CODIGO));
                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@serial", terminal.TRM_SERIAL));
                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@fechaRegistro",DateTime.Today));
                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@estado", terminal.ESTADO_TERMINAL.EST_CODIGO));
                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@puntoServicio", terminal.PUNTO_SERVICIO.PSR_CODIGO));
                        ComandoInsert.Parameters.Add(Factoria.CrearParametro("@entidadComunicacion", terminal.ENTIDAD_COMUNICACION.EDC_CODIGO));

                        if (ComandoInsert.ExecuteNonQuery() != 1)
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

        public static List<TERMINAL> obtenerTerminal()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                return contexto.TERMINAL.ToList<TERMINAL>();
            }
        }

        public static EstadoOperacion modificarTerminal(TERMINAL terminal)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE TERMINAL" +
                            " SET TRM_SERIAL = @serial" +
                            ",EST_CODIGO = @codEstadoTerminal" +
                            ",PSR_CODIGO = @codPtoServicio" +
                            ",EDC_CODIGO = @codEntidad" +
                            " WHERE TRM_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", terminal.TRM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@serial", terminal.TRM_SERIAL));
                        Comando.Parameters.Add(Factoria.CrearParametro("@codEstadoTerminal", terminal.ESTADO_TERMINAL.EST_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@codPtoServicio", terminal.PUNTO_SERVICIO.PSR_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@codEntidad", terminal.ENTIDAD_COMUNICACION.EDC_CODIGO));

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


        public static EstadoOperacion eliminarTerminal(TERMINAL terminal)
        {
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM TERMINAL" +
                            " WHERE TRM_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", terminal.TRM_CODIGO));

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
