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
    public class PuntoServicioDA
    {
        public static List<PUNTO_SERVICIO> obtenerPuntoServicio(string nombre, string estado)
        {
            StringBuilder query = new StringBuilder("");
            query.Append("Select value PS from PUNTO_SERVICIO as PS where " +
                "PS.PSR_NOMBRE like '%'+@nombre+'%'");
            if (estado != "%")
            {
                query.Append(" and PS.PSR_ESTADO =@estado");
            }

            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.PUNTO_SERVICIO.MergeOption = MergeOption.NoTracking;
                ObjectQuery<PUNTO_SERVICIO> puntoServicio = new ObjectQuery<PUNTO_SERVICIO>(query.ToString(), contexto);
                puntoServicio.Parameters.Add(new ObjectParameter("nombre", nombre));
                if (estado != "%")
                {
                    puntoServicio.Parameters.Add(new ObjectParameter("estado", Boolean.Parse(estado)));
                }

                return puntoServicio.ToList<PUNTO_SERVICIO>();
            }
        }

        public static PUNTO_SERVICIO obtenerPuntoServicio(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.PUNTO_SERVICIO.MergeOption = MergeOption.NoTracking;
                return contexto.PUNTO_SERVICIO.Where(o => o.PSR_CODIGO == codigo).FirstOrDefault<PUNTO_SERVICIO>();
            }
        }

        public static EstadoOperacion insertarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int idPuntoServicio = (from c in contexto.PUNTO_SERVICIO
                                               orderby c.PSR_CODIGO descending
                                               select c.PSR_CODIGO).FirstOrDefault() + 1;

                        puntoServicio.PSR_CODIGO = idPuntoServicio;

                        string insert = "INSERT INTO PUNTO_SERVICIO" +
                                      "(PSR_CODIGO" +
                                      ",PSR_NOMBRE" +
                                      ",PSR_ESTADO" +
                                      ",PSR_DIRECCION)" +
                                      " VALUES(@codigo,@nombre,@estado,@direccion)";

                        DbCommand Comando = contexto.CreateCommand(insert, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", puntoServicio.PSR_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@nombre", puntoServicio.PSR_NOMBRE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@estado", puntoServicio.PSR_ESTADO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@direccion", puntoServicio.PSR_DIRECCION));

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

        public static List<PUNTO_SERVICIO> obtenerPuntoServicio()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                return contexto.PUNTO_SERVICIO.ToList<PUNTO_SERVICIO>();
            }
        }

        public static EstadoOperacion modificarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            
            try
            {
                DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE PUNTO_SERVICIO" +
                            " SET PSR_NOMBRE = @nombre" +
                            ",PSR_ESTADO = @estado" +
                            ",PSR_DIRECCION = @direccion" +
                            " WHERE PSR_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", puntoServicio.PSR_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@nombre", puntoServicio.PSR_NOMBRE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@estado", puntoServicio.PSR_ESTADO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@direccion", puntoServicio.PSR_DIRECCION));

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

        public static EstadoOperacion eliminarPuntoServicio(PUNTO_SERVICIO puntoServicio)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM PUNTO_SERVICIO" +
                            " WHERE PSR_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", puntoServicio.PSR_CODIGO));

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
                    return new EstadoOperacion(false, "El Punto de Servicio esta asignado a un Terminal y no se puede eliminar", e, true);
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
