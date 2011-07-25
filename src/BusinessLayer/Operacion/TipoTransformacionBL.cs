using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Operacion;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public sealed class TipoTransformacionBL
    {
        public static SortedList<int, string> obtenerTipoTransformacion()
        {
            return TipoTransformacionDA.obtenerTipoTransformacion();
        }

        public static int obtenerCodigoValorConstante()
        {
            return (int)EnumTipoTransformacion.ValorConstante;
        }

        public static int obtenerCodigoComponente()
        {
            return (int)EnumTipoTransformacion.Componente;
        }

        public static int obtenerCodigoConcatenacion()
        {
            return (int)EnumTipoTransformacion.Concatenacion;
        }

        public static int obtenerCodigoFuncionalidadEstandar()
        {
            return (int)EnumTipoTransformacion.FuncionalidadEstandar;
        }

        public static int obtenerCodigoProcedimientoAlmacenado()
        {
            return (int)EnumTipoTransformacion.ProcedimientoAlmacenado;
        }

        public static string obtenerNombrePorCodigo(int codigo)
        {
            string nombre = System.Enum.GetName(typeof(EnumTipoTransformacion), codigo);
            return nombre;
        }

    }

    
}
