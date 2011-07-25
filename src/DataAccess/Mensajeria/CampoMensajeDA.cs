using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;
using DataAccess.Utilitarios;

namespace DataAccess.Mensajeria
{
    public sealed class CampoMensajeDA
    {
        public static List<CAMPO> obtenerCampo()
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                return contexto.CAMPO.ToList();
            }
        }

        public static List<CAMPO> obtenerCampo(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                var listaMensaje = (from c in contexto.CAMPO.Include("TIPO_DATO")
                                    where c.MENSAJE.MEN_CODIGO == codigoMensaje
                                    orderby c.CAM_POSICION_RELATIVA ascending
                                    select c).ToList();

                return listaMensaje;
            }
        }


        public static List<CAMPO> obtenerCampoCabecera(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                var listaMensaje = (from c in contexto.CAMPO.Include("TIPO_DATO")
                                    where c.MENSAJE.MEN_CODIGO == codigoMensaje
                                    && c.CAM_CABECERA==true
                                    select c).ToList<CAMPO>();

                return listaMensaje;
            }
        }

        public static List<CAMPO> obtenerCampoCuerpo(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                var listaMensaje = (from c in contexto.CAMPO.Include("TIPO_DATO")
                                    where c.MENSAJE.MEN_CODIGO == codigoMensaje
                                    && c.CAM_CABECERA == false
                                    orderby c.CAM_POSICION_RELATIVA ascending
                                    select c).ToList<CAMPO>();

                return listaMensaje;
            }
        }

        public static CAMPO obtenerCampo(int codigoMensaje, int codigoCampo)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                var mensaje = (from c in contexto.CAMPO.Include("TIPO_DATO")
                               where c.MENSAJE.MEN_CODIGO == codigoMensaje && c.CAM_CODIGO == codigoCampo
                               select c).FirstOrDefault<CAMPO>();

                return mensaje;
            }
        }

        public static List<CAMPO> obtenerCampoSelector(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                return (from c in contexto.CAMPO.Include("TIPO_DATO")
                        where c.MENSAJE.MEN_CODIGO == codigoMensaje 
                                 && c.CAM_SELECTOR
                        select c).ToList();

            }
        }

        public static List<CAMPO> obtenerCampoNoSelector(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                return (from c in contexto.CAMPO.Include("TIPO_DATO")
                        where c.MENSAJE.MEN_CODIGO == codigoMensaje
                                 && c.CAM_SELECTOR == false
                        select c).ToList();

            }
        }


        public static List<CAMPO> obtenerCampoNoSelectorNoAsignadoReglaTransaccional(int codigoMensaje)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                return (from c in contexto.CAMPO.Include("TIPO_DATO")
                        where c.MENSAJE.MEN_CODIGO == codigoMensaje
                                 && c.CAM_TRANSACCIONAL==true
                                 //TODO: modificar la regla para que sea que no tenga ninguna regla transaccional
                                 //pero dentro del mismo mensaje transaccional
                                 //&& c.REGLA_MENSAJE_TRANSACCIONAL.Count==0
                        select c).ToList<CAMPO>();
            }
        }

        public static List<CAMPO> obtenerCampoOrigenPorTransaccion(int codigoTransaccion)
        {
            using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
            {
                contexto.CAMPO.MergeOption = MergeOption.NoTracking;
                var listaCampos = (from t in contexto.TRANSFORMACION
                                    where t.TRM_CODIGO == codigoTransaccion
                                    select t.MENSAJE_ORIGEN.CAMPO).FirstOrDefault();

                return listaCampos.ToList<CAMPO>() ;
            }
        }

        public static EstadoOperacion actualizarValorSelector(CAMPO Campo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE CAMPO" +
                               " SET CAM_VALOR_SELECTOR_REQUEST = @valorselectorRequest" +
                               " ,CAM_VALOR_SELECTOR_RESPONSE = @valorselectorResponse" +
                             " WHERE CAM_CODIGO = @codigo" +
                                    " AND MEN_CODIGO = @mensaje_codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@valorselectorRequest", Campo.CAM_VALOR_SELECTOR_REQUEST));
                        Comando.Parameters.Add(Factoria.CrearParametro("@valorselectorResponse", Campo.CAM_VALOR_SELECTOR_RESPONSE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));

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
                return new EstadoOperacion(false, e.Message, null);
            }
        }

        public static EstadoOperacion insertarCampoPorCampoPlantilla(CAMPO Campo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        Campo.CAM_CODIGO = (from c in contexto.CAMPO
                                            orderby c.CAM_CODIGO descending
                                            select c.CAM_CODIGO).FirstOrDefault() + 1;

                        CAMPO_PLANTILLA campoPlantilla = (from cp in contexto.CAMPO_PLANTILLA.Include("TIPO_DATO")
                                                          where cp.CMP_CODIGO == Campo.CAMPO_PLANTILLA.CMP_CODIGO
                                                          select cp).FirstOrDefault();

                        Campo.TIPO_DATO = new TIPO_DATO() { TDT_CODIGO = campoPlantilla.TIPO_DATO.TDT_CODIGO };
                        Campo.CAM_NOMBRE = campoPlantilla.CMP_NOMBRE;
                        Campo.CAM_LONGITUD = campoPlantilla.CMP_LONGITUD;
                        Campo.CAM_VARIABLE = campoPlantilla.CMP_VARIABLE;
                        Campo.CAM_ALMACENADO = campoPlantilla.CMP_ALMACENADO;
                        Campo.CAM_PROTEGIDO_LOG = campoPlantilla.CMP_PROTEGIDO_LOG;
                        Campo.CAM_POSICION_RELATIVA = campoPlantilla.CMP_POSICION_RELATIVA;
                        Campo.CAM_LONGITUD_CABECERA = campoPlantilla.CMP_LONGITUD_CABECERA;
                        Campo.CAM_SELECTOR=campoPlantilla.CMP_SELECTOR;
                        Campo.CAM_TRANSACCIONAL = campoPlantilla.CMP_TRANSACCIONAL;
                        Campo.CAM_CABECERA = campoPlantilla.CMP_CABECERA;
                        Campo.CAM_BITMAP = campoPlantilla.CMP_BITMAP;

                        string query =
                                "INSERT INTO CAMPO" +
                                       "(CAM_CODIGO" +
                                       ",MEN_CODIGO" +
                                       ",CAM_NOMBRE" +
                                       ",CAM_LONGITUD" +
                                       ",CAM_LONGITUD_CABECERA" +
                                       ",CAM_SELECTOR" +
                                       ",CAM_VARIABLE" +
                                       ",CAM_REQUERIDO" +
                                       ",CAM_ALMACENADO" +
                                       ",CAM_POSICION_RELATIVA" +
                                       ",TDT_CODIGO" +
                                       ",CMP_CODIGO" +
                                       ",CAM_PROTEGIDO_LOG" +
                                       ",CAM_CABECERA" +
                                       ",CAM_BITMAP" +
                                       ",CAM_TRANSACCIONAL"+
                                       ",CAM_TANQUEO"+
                                       ",CAM_DESTANQUEO)" +
                                 "VALUES" +
                                       "(@codigo" +
                                       ",@mensaje_codigo" +
                                       ",@nombre" +
                                       ",@longitud" +
                                       ",@longitudCabecera" +
                                       ",@selector" +
                                       ",@variable" +
                                       ",@requerido" +
                                       ",@almacenado" +
                                       ",@posicionrelativa" +
                                       ",@tipodato_codigo" +
                                       ",@campoplantilla_codigo" +
                                       ",@protegidolog" +
                                       ",@cabecera" +
                                       ",@bitmap" +
                                       ",@transaccional"+
                                       ",@tanqueo"+
                                       ",@destanqueo)";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Campo.CAM_NOMBRE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@longitud", Campo.CAM_LONGITUD));
                        Comando.Parameters.Add(Factoria.CrearParametro("@longitudCabecera",Util.NullableToDbValue<int>(Campo.CAM_LONGITUD_CABECERA)));
                        Comando.Parameters.Add(Factoria.CrearParametro("@selector", Campo.CAM_SELECTOR));
                        Comando.Parameters.Add(Factoria.CrearParametro("@variable", Campo.CAM_VARIABLE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@requerido", Campo.CAM_REQUERIDO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@almacenado", Campo.CAM_ALMACENADO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@posicionrelativa", Campo.CAM_POSICION_RELATIVA));
                        Comando.Parameters.Add(Factoria.CrearParametro("@tipodato_codigo", Campo.TIPO_DATO.TDT_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@campoplantilla_codigo", Campo.CAMPO_PLANTILLA.CMP_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@protegidolog", Campo.CAM_PROTEGIDO_LOG));
                        Comando.Parameters.Add(Factoria.CrearParametro("@cabecera", Campo.CAM_CABECERA));
                        Comando.Parameters.Add(Factoria.CrearParametro("@bitmap", Campo.CAM_BITMAP));
                        Comando.Parameters.Add(Factoria.CrearParametro("@transaccional", Campo.CAM_TRANSACCIONAL));
                        Comando.Parameters.Add(Factoria.CrearParametro("@tanqueo", false));
                        Comando.Parameters.Add(Factoria.CrearParametro("@destanqueo", false));

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
                return new EstadoOperacion(false, e.Message, null);
            }
        }

        public static EstadoOperacion insertarCampo(CAMPO Campo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        Campo.CAM_CODIGO = (from c in contexto.CAMPO
                                            orderby c.CAM_CODIGO descending
                                            select c.CAM_CODIGO).FirstOrDefault() + 1;

                        string query =
                                "INSERT INTO CAMPO" +
                                       "(CAM_CODIGO" +
                                       ",MEN_CODIGO" +
                                       ",CAM_NOMBRE" +
                                       ",CAM_LONGITUD" +
                                       ",CAM_VARIABLE" +
                                       ",CAM_REQUERIDO" +
                                       ",CAM_ALMACENADO" +
                                       ",CAM_POSICION_RELATIVA" +
                                       ",TDT_CODIGO" +
                                       ",CMP_CODIGO" +
                                       ",CAM_PROTEGIDO_LOG"+
                                       ",CAM_TANQUEO"+
                                       ",CAM_DESTANQUEO)" +
                                 "VALUES" +
                                       "(@codigo" +
                                       ",@mensaje_codigo" +
                                       ",@nombre" +
                                       ",@longitud" +
                                       ",@variable" +
                                       ",@requerido" +
                                       ",@almacenado" +
                                       ",@posicionrelativa" +
                                       ",@tipodato_codigo" +
                                       ",@campoplantilla_codigo" +
                                       ",@protegidolog"+
                                       ",@tanqueo"+
                                       ",@destanqueo)";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Campo.CAM_NOMBRE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@longitud", Campo.CAM_LONGITUD));
                        Comando.Parameters.Add(Factoria.CrearParametro("@variable", Campo.CAM_VARIABLE));
                        Comando.Parameters.Add(Factoria.CrearParametro("@requerido", Campo.CAM_REQUERIDO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@almacenado", Campo.CAM_ALMACENADO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@posicionrelativa", Campo.CAM_POSICION_RELATIVA));
                        Comando.Parameters.Add(Factoria.CrearParametro("@tipodato_codigo", Campo.TIPO_DATO.TDT_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@campoplantilla_codigo", Campo.CAMPO_PLANTILLA.CMP_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@protegidolog", Campo.CAM_PROTEGIDO_LOG));
                        Comando.Parameters.Add(Factoria.CrearParametro("@tanqueo", false));
                        Comando.Parameters.Add(Factoria.CrearParametro("@destanqueo", false));

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

        public static EstadoOperacion modificarCampo(CAMPO Campo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE CAMPO" +
                               " SET CAM_REQUERIDO = @requerido" +
                             " WHERE CAM_CODIGO = @codigo" +
                                    " AND MEN_CODIGO = @mensaje_codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@requerido", Campo.CAM_REQUERIDO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));

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


        public static EstadoOperacion eliminarCampo(CAMPO Campo)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                                "DELETE FROM CAMPO" +
                                 " WHERE MEN_CODIGO = @mensaje_codigo " +
                                 "and CAM_CODIGO = @codigo";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
                        Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));

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

        private static DbCommand crearComando(dbSwitch contexto, CAMPO Campo, string query)
        {
            DbFactory Factoria = DataAccessFactory.ObtenerProveedor();
            DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
            Comando.Parameters.Add(Factoria.CrearParametro("@codigo", Campo.CAM_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@nombre", Campo.CAM_NOMBRE));
            Comando.Parameters.Add(Factoria.CrearParametro("@mensaje_codigo", Campo.MEN_CODIGO));
            Comando.Parameters.Add(Factoria.CrearParametro("@longitud", Campo.CAM_LONGITUD));
            Comando.Parameters.Add(Factoria.CrearParametro("@variable", Campo.CAM_VARIABLE));
            Comando.Parameters.Add(Factoria.CrearParametro("@requerido", Campo.CAM_REQUERIDO));
            Comando.Parameters.Add(Factoria.CrearParametro("@almacenado", Campo.CAM_ALMACENADO));
            Comando.Parameters.Add(Factoria.CrearParametro("@tipodato_codigo", Campo.TIPO_DATO.TDT_CODIGO));
            return Comando;
        }
    }
}
