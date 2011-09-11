using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public sealed class ColumnaBL
    {
        public static List<COLUMNA> obtenerColumna(int codigoTabla)
        {
            return DataAccess.Mensajeria.ColumnaDA.obtenerColumna(codigoTabla);
        }

        public static COLUMNA obtenerColumnaPorCodigo(int codigoColumna)
        {
            return DataAccess.Mensajeria.ColumnaDA.obtenerColumnaPorCodigo(codigoColumna);
        }

        public static EstadoOperacion insertarColumna(COLUMNA Columna)
        {
            COLUMNA columna = DataAccess.Mensajeria.ColumnaDA.obtenerColumnaPorNombre(Columna.COL_NOMBRE);
            if (columna != null)
            {
                return new EstadoOperacion(false, "Ya existe una columna con ese Nombre", null);
            }
            return DataAccess.Mensajeria.ColumnaDA.insertarColumna(Columna);
        }

        public static EstadoOperacion modificarColumna(COLUMNA Columna)
        {
            COLUMNA columnAntigua = DataAccess.Mensajeria.ColumnaDA.obtenerColumnaPorCodigo(Columna.COL_CODIGO);

            if (columnAntigua.COL_NOMBRE.ToUpper() != Columna.COL_NOMBRE.ToUpper())
            {
                COLUMNA columnaMismoNombre = DataAccess.Mensajeria.ColumnaDA.obtenerColumnaPorNombre(Columna.COL_NOMBRE);
                if (columnaMismoNombre != null)
                {
                    return new EstadoOperacion(false, "Ya existe una columna con ese Nombre", null);
                }
            }

            return DataAccess.Mensajeria.ColumnaDA.modificarColumna(Columna);
        }

        public static EstadoOperacion eliminarColumna(COLUMNA Columna)
        {
            return DataAccess.Mensajeria.ColumnaDA.eliminarColumna(Columna);
        }
    }
}
