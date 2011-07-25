using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using BusinessEntity;
using DataAccess.Factoria;

namespace DataAccess.Operacion
{
    public class TanqueoDA
    {
        private static DbFactory Factoria = DataAccessFactory.ObtenerProveedor();

        public static EstadoOperacion modificarCamposTranqueo(int idMensaje, IList<int> idCampoTanqueo, IList<int> idCampoDestanqueo)
        {

            try
            {
                using (dbSwitch contexto = new dbSwitch(CadenaConexion.getInstance().conexionEntidades))
                {
                    using (contexto.CreateConeccionScope())
                    {
                        string query =
                            "UPDATE CAMPO" +
                              " SET CAM_TANQUEO = 0," +
                                   "CAM_DESTANQUEO = 0" +
                            " WHERE MEN_CODIGO = @codigoMensaje";

                        DbCommand Comando = contexto.CreateCommand(query, CommandType.Text);
                        Comando.Parameters.Add(Factoria.CrearParametro("@codigoMensaje", idMensaje));

                        if (Comando.ExecuteNonQuery() == 0)
                            return new EstadoOperacion(false, null, null);

                        if (idCampoTanqueo.Count > 0)
                        {
                            string tanqueo = idCampoTanqueo.Select(x => x.ToString())
                                .Aggregate((current, next) => current + "," + next);

                            query =
                                "UPDATE CAMPO" +
                                  " SET CAM_TANQUEO = 1" +
                                " WHERE MEN_CODIGO = @codigoMensaje" +
                                  " AND CAM_CODIGO IN (" + tanqueo + ")";

                            Comando.CommandText = query;

                            if (Comando.ExecuteNonQuery() == 0)
                                return new EstadoOperacion(false, null, null);
                        }

                        if (idCampoDestanqueo.Count > 0)
                        {
                            string destanqueo =
                                idCampoDestanqueo.Select(x => x.ToString())
                                .Aggregate((current, next) => current + "," + next);

                            query =
                                "UPDATE CAMPO" +
                                " SET CAM_DESTANQUEO = 1" +
                                " WHERE MEN_CODIGO = @codigoMensaje" +
                                " AND CAM_CODIGO IN (" + destanqueo + ")";

                            Comando.CommandText = query;

                            if (Comando.ExecuteNonQuery() == 0)
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
