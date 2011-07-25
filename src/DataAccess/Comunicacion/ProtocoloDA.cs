using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Comunicacion
{
    public class ProtocoloDA
    {
        public static List<PROTOCOLO> obtenerProtocolo()
        {
            using (dbSwitch contexto = new dbSwitch())
            {
                contexto.PROTOCOLO.MergeOption = MergeOption.NoTracking;
                return contexto.PROTOCOLO.Include("TIPO_COMUNICACION").ToList<PROTOCOLO>();
            }
        }

        public static PROTOCOLO obtenerProtocolo(int codigo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.PROTOCOLO.MergeOption = MergeOption.NoTracking;
                return contexto.PROTOCOLO.Include("TIPO_COMUNICACION").Where(o => o.PTR_CODIGO == codigo).FirstOrDefault<PROTOCOLO>();
            }
        }

        public static List<PROTOCOLO> obtenerProtocolosNoAsignados()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.PROTOCOLO.MergeOption = MergeOption.NoTracking;
                List<PROTOCOLO> lista = (from p in contexto.PROTOCOLO
                                         where p.ENTIDAD_COMUNICACION.Count == 0
                                         select p).ToList<PROTOCOLO>();

                return lista;

            }
        }

        public static EstadoOperacion insertarProtocolo(PROTOCOLO Protocolo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        int nuevoId = (from c in contexto.PROTOCOLO
                                       orderby c.PTR_CODIGO descending
                                       select c.PTR_CODIGO).FirstOrDefault() + 1;

                        Protocolo.PTR_CODIGO = nuevoId;

                        string query =
                            "INSERT INTO PROTOCOLO" +
                            "(PTR_CODIGO" +
                            ",PTR_NOMBRE" +
                            ",PTR_TIMEOUT_REQUEST" +
                            ",PTR_TIMEOUT_RESPONSE" +
                            ",PTR_PUERTO" +
                            ",PTR_FRAME" +
                            ",PTR_CARACTER_INICIO" +
                            ",PTR_CARACTER_FIN" +
                            ",PTR_COMPONENTE" +
                            ",PTR_NOMBRE_CLASE" +
                            ",PTR_NOMBRE_METODO" +
                            ",TPO_CODIGO" +
                            ",PTR_INICIA_COMM" +
                            ",PTR_ACEPTA_COMM)" +
                            "VALUES" +
                            "(@codigo" +
                            ",@nombre" +
                            ",@timeOutRequest" +
                            ",@timeOutResponse" +
                            ",@puerto" +
                            ",@frame" +
                            ",@caracterInicio" +
                            ",@caracterFin" +
                            ",@componente" +
                            ",@nombreClase" +
                            ",@nombreMetodo" +
                            ",@tipoComunicacion_codigo" +
                            ",@iniciaComunicacion" +
                            ",@aceptaComunicacion)";


                        DbCommand Comando = crearComando(contexto,Protocolo, query);

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

        public static EstadoOperacion modificarProtocolo(PROTOCOLO Protocolo)
        {
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE PROTOCOLO" +
                            " SET " +
                            "PTR_NOMBRE = @nombre" +
                            ",PTR_TIMEOUT_REQUEST = @timeOutRequest" +
                            ",PTR_TIMEOUT_RESPONSE = @timeOutResponse" +
                            ",PTR_PUERTO = @puerto" +
                            ",PTR_FRAME = @frame" +
                            ",PTR_CARACTER_INICIO = @caracterInicio" +
                            ",PTR_CARACTER_FIN = @caracterFin" +
                            ",PTR_COMPONENTE = @componente" +
                            ",PTR_NOMBRE_CLASE = @nombreClase" +
                            ",PTR_NOMBRE_METODO = @nombreMetodo" +
                            ",TPO_CODIGO = @tipoComunicacion_codigo" +
                            ",PTR_INICIA_COMM = @iniciaComunicacion" +
                            ",PTR_ACEPTA_COMM = @aceptaComunicacion" +
                            " WHERE PTR_CODIGO =@codigo";

                        DbCommand Comando = crearComando(contexto,Protocolo, query);

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

        public static EstadoOperacion eliminarProtocolo(PROTOCOLO Protocolo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "DELETE FROM PROTOCOLO" +
                            " WHERE PTR_CODIGO =@codigo;";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);

                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Protocolo.PTR_CODIGO));

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
                    return new EstadoOperacion(false, "El Protocolo esta asignado a una Entidad Comunicacion y no se puede eliminar", e, true);
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


        private static DbCommand crearComando(dbSwitch contexto,PROTOCOLO Protocolo, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Protocolo.PTR_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Protocolo.PTR_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@timeOutRequest", Util.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_REQUEST)));
            Comando.Parameters.Add(Factoria.CrearParametro("@timeOutResponse", Util.NullableToDbValue<int>(Protocolo.PTR_TIMEOUT_RESPONSE)));
            Comando.Parameters.Add(Factoria.CrearParametro("@iniciaComunicacion", Protocolo.PTR_INICIA_COMM));
            Comando.Parameters.Add(Factoria.CrearParametro("@aceptaComunicacion", Protocolo.PTR_ACEPTA_COMM));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipoComunicacion_codigo", Protocolo.TIPO_COMUNICACION.TPO_CODIGO));

            if (Protocolo.TIPO_COMUNICACION.TPO_CODIGO == (int)EnumTipoComunicacion.Componente)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@componente", Protocolo.PTR_COMPONENTE));
                Comando.Parameters.Add(Factoria.CrearParametro("@nombreClase", Protocolo.PTR_NOMBRE_CLASE));
                Comando.Parameters.Add(Factoria.CrearParametro("@nombreMetodo", Protocolo.PTR_NOMBRE_METODO));
            }
            else
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@componente", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@nombreClase", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@nombreMetodo", DBNull.Value));
            }

            if (Protocolo.TIPO_COMUNICACION.TPO_CODIGO == (int)EnumTipoComunicacion.TCP)
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@puerto", Util.NullableToDbValue<int>(Protocolo.PTR_PUERTO)));
                Comando.Parameters.Add(Factoria.CrearParametro("@frame", Protocolo.PTR_FRAME));
                Comando.Parameters.Add(Factoria.CrearParametro("@caracterInicio", Protocolo.PTR_CARACTER_INICIO));
                Comando.Parameters.Add(Factoria.CrearParametro("@caracterFin", Protocolo.PTR_CARACTER_FIN));
            }
            else 
            {
                Comando.Parameters.Add(Factoria.CrearParametro("@puerto", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@frame", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@caracterInicio", DBNull.Value));
                Comando.Parameters.Add(Factoria.CrearParametro("@caracterFin", DBNull.Value));
            }

            return Comando;
        }
    }
}
