using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;
using DataAccess.Enumeracion.EnumTablasBD;
using DataAccess.Mensajeria;

namespace BusinessLayer.Mensajeria
{
    [DataObject(true)]
    public class ValidacionCampoBL
    {
        public static List<VALIDACION_CAMPO> obtenerValidacionCampo(int grupoVal, int mensaje, int campo)
        {
            return ValidacionCampoDA.obtenerValidacionCampo(grupoVal, mensaje, campo);
        }
        
        public static VALIDACION_CAMPO obtenerValidacionCampoPorCodigo(int codigo)
        {
            return ValidacionCampoDA.obtenerValidacionCampoPorCodigo(codigo);
        }

        public static EstadoOperacion insertarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            return ValidacionCampoDA.insertarValidacionCampo(vcampo);
        }

        public static EstadoOperacion modificarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            return ValidacionCampoDA.modificarValidacionCampo(vcampo);
        }

        public static EstadoOperacion eliminarValidacionCampo(VALIDACION_CAMPO vcampo)
        {
            return ValidacionCampoDA.eliminarValidacionCampo(vcampo);
        }

        public static int obtenerCriterioAplicacionInclusion()
        {
            return (int)EnumCriterioAplicacion.Inclusion;
        }

        public static int obtenerCriterioAplicacionProcedimiento()
        {
            return (int)EnumCriterioAplicacion.Procedimiento;
        }

        public static int obtenerCriterioAplicacionComponente()
        {
            return (int)EnumCriterioAplicacion.Componente;
        }

        public static int obtenerTipoReglaCondicion()
        {
            return (int)EnumTipoRegla.Condicion;
        }

        public static int obtenerTipoReglaTablaValores()
        {
            return (int)EnumTipoRegla.TablaValores; 
        }

        public static int obtenerCondicionIgual()
        {
            return (int)EnumCondicion.Igual;
        }

        public static int obtenerCondicionDiferente()
        {
            return (int)EnumCondicion.Diferente;
        }

        public static int obtenerCondicionMayorIgual()
        {
            return (int)EnumCondicion.MayorIgual;
        }

        public static int obtenerCondicionMayor()
        {
            return (int)EnumCondicion.Mayor;
        }

        public static int obtenerCondicionMenorIgual()
        {
            return (int)EnumCondicion.MenorIgual;
        }

        public static int obtenerCondicionMenor()
        {
            return (int)EnumCondicion.Menor;
        }
    }
}
