using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;


namespace BusinessLayer.Seguridad
{
    [DataObject(true)]
    public sealed class CriptografiaCampoBL
    {
        public static List<CRIPTOGRAFIA_CAMPO> obtenerCriptografiaCampo()
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.obtenerCriptografiaCampo();
        }

        public static List<CRIPTOGRAFIA_CAMPO> obtenerCriptografiaCampo(int codigoDinamicaCriptografia)
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.obtenerCriptografiaCampo(codigoDinamicaCriptografia);
        }

        public static CRIPTOGRAFIA_CAMPO obtenerCriptografiaCampo(int codigoDinamicaCriptografia,int codigoCriptografiaCampo)
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.obtenerCriptografiaCampo(codigoDinamicaCriptografia, codigoCriptografiaCampo);
        }

        public static EstadoOperacion insertarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.insertarCriptografiaCampo(CriptografiaCampo);
        }

        public static EstadoOperacion modificarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.modificarCriptografiaCampo(CriptografiaCampo);
        }

        public static EstadoOperacion eliminarCriptografiaCampo(CRIPTOGRAFIA_CAMPO CriptografiaCampo)
        {
            return DataAccess.Seguridad.CriptografiaCampoDA.eliminarCriptografiaCampo(CriptografiaCampo);
        }
    }
}
