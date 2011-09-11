using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class TablaBL
    {
        public static List<TABLA> obtenerTabla()
        {
            return DataAccess.Mensajeria.TablaDA.obtenerTabla();
        }

        public static TABLA obtenerTablaPorCodigo(int codigoTabla)
        {
            return DataAccess.Mensajeria.TablaDA.obtenerTablaPorCodigo(codigoTabla);
        }

        public static EstadoOperacion insertarTabla(TABLA tabla)
        {
            return DataAccess.Mensajeria.TablaDA.insertarTabla(tabla);
        }

        public static EstadoOperacion modificarTabla(TABLA tabla)
        {
            return DataAccess.Mensajeria.TablaDA.modificarTabla(tabla);
        }

        public static EstadoOperacion eliminarTabla(TABLA tabla)
        {
            return DataAccess.Mensajeria.TablaDA.eliminarTabla(tabla);
        }

        public static DataTable ObtenerValoresTabla(string tabla)
        {
            return DataAccess.Mensajeria.TablaDA.ObtenerValoresTabla(tabla);
        }

        public static EstadoOperacion insertarValoresTabla(string tabla,
            List<string> columnasTabla, List<string> valoresTabla)
        {
            return DataAccess.Mensajeria.TablaDA.insertarValoresTabla(tabla,
            columnasTabla,valoresTabla);
        }

        public static EstadoOperacion modificarValoresTabla(string tabla,
            List<string> columnasTabla, List<string> valoresTabla, int Id)
        {
            return DataAccess.Mensajeria.TablaDA.modificarValoresTabla(tabla,
                columnasTabla, valoresTabla,Id);
        }

        public static EstadoOperacion eliminarValoresTabla(string tabla,
            int Id)
        {
            return DataAccess.Mensajeria.TablaDA.eliminarValoresTabla(tabla, Id);
        }
    }
}
