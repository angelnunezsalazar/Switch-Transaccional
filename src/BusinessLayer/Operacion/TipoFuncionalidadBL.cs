using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Utilitarios;

namespace BusinessLayer.Operacion
{
    [DataObject(true)]
    public class TipoFuncionalidadBL
    {
        public static SortedList<int, string> obtenerTipoFuncionalidad()
        {
            return Util.GetEnumDataSource<EnumTipoFuncionalidad>();
        }

        public static int obtenerCodigoValidacion()
        {
            return (int)EnumTipoFuncionalidad.Validacion;
        }

        public static int obtenerCodigoTransformacion()
        {
            return (int)EnumTipoFuncionalidad.Transformacion;
        }

        public static int obtenerCodigoCriptografia()
        {
            return (int)EnumTipoFuncionalidad.Criptografia;
        }

        public static int obtenerCodigoEnviar()
        {
            return (int)EnumTipoFuncionalidad.Enviar;
        }

        public static int obtenerCodigoRecibir()
        {
            return (int)EnumTipoFuncionalidad.Recibir;
        }

        public static int obtenerCodigoDescargar()
        {
            return (int)EnumTipoFuncionalidad.Descartar;
        }

        public static string obtenerNombrePorCodigo(int codigo)
        {
            string nombre = System.Enum.GetName(typeof(EnumTipoFuncionalidad), codigo);
            return nombre;
        }
    }
}
