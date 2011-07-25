using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace BusinessEntity
{
    public partial class dbSwitch : global::System.Data.Objects.ObjectContext
    {
        public EstadoOperacion insertarTerminal(TERMINAL terminal)
        {
            string insert = "INSERT INTO [TERMINAL]" +
                            "([TRM_SERIAL]" +
                            ",[TRM_NOMBRE_DISPOSITIVO]" +
                            ",[TRM_VERSION_APLICACION]" +
                            ",[TTR_CODIGO_TIPO_TERMINAL]" +
                            ",[TIC_CODIGO_TIPO_COMUNICACION])" +
                            ",[EST_CODIGO_ESTADO])" +
                            ",[PSR_CODIGO_PUNTO_SERVICIO])" +
                            "VALUES(@serial,@nombre,@version,@tipoTerminal,@tipoComunicacion,@estado,@puntoServicio)";

            DbCommand ComandoInsert = this.CreateStoreCommand(insert, CommandType.Text);

            ComandoInsert.Parameters.Add(new SqlParameter("@serial", terminal.TRM_SERIAL));
            ComandoInsert.Parameters.Add(new SqlParameter("@nombre", terminal.TRM_NOMBRE_DISPOSITIVO));
            ComandoInsert.Parameters.Add(new SqlParameter("@version", terminal.TRM_VERSION_APLICACION));
            ComandoInsert.Parameters.Add(new SqlParameter("@tipoTerminal", terminal.TIPO_TERMINAL.TTR_CODIGO_TIPO_TERMINAL));
            //ComandoInsert.Parameters.Add(new SqlParameter("@tipoComunicacion", terminal.TIPO_COMUNICACION.TIC_CODIGO_TIPO_COMUNICACION));
            ComandoInsert.Parameters.Add(new SqlParameter("@estado", terminal.ESTADO_TERMINAL.EST_CODIGO_ESTADO));
            ComandoInsert.Parameters.Add(new SqlParameter("@puntoServicio", terminal.PUNTO_SERVICIO.PSR_CODIGO_PUNTO_SERVICIO));

            ComandoInsert.ExecuteNonQuery();

            return new EstadoOperacion(true, null, null);
        }
    }
}
