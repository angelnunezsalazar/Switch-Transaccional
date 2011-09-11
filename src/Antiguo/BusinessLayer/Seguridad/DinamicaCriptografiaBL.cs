using System.Collections.Generic;
using System.ComponentModel;
using BusinessEntity;


namespace BusinessLayer.Seguridad
{
    [DataObject(true)]
    public sealed class DinamicaCriptografiaBL
    {
        public static DINAMICA_CRIPTOGRAFIA obtenerDinamicaCriptografia(int codigoDinamicaCriptografia)
        {
            return DataAccess.Seguridad.DinamicaCriptografiaDA.obtenerDinamicaCriptografia(codigoDinamicaCriptografia);
        }

        public static List<DINAMICA_CRIPTOGRAFIA> obtenerDinamicaCriptografiaPorMensaje(int codigoMensaje)
        {
            return DataAccess.Seguridad.DinamicaCriptografiaDA.obtenerDinamicaCriptografiaPorMensaje(codigoMensaje);
        }

        public static EstadoOperacion insertarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            return DataAccess.Seguridad.DinamicaCriptografiaDA.insertarDinamicaCriptografia(DinamicaCriptografia);
        }

        public static EstadoOperacion modificarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            return DataAccess.Seguridad.DinamicaCriptografiaDA.modificarDinamicaCriptografia(DinamicaCriptografia);
        }

        public static EstadoOperacion eliminarDinamicaCriptografia(DINAMICA_CRIPTOGRAFIA DinamicaCriptografia)
        {
            return DataAccess.Seguridad.DinamicaCriptografiaDA.eliminarDinamicaCriptografia(DinamicaCriptografia);
        }
    }
}
