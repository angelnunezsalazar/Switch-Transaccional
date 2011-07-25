using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using Switch.Dinamica;

namespace Switch.Criptografia
{
    public class Criptografia : Componente
    {
        List<DINAMICA_CRIPTOGRAFIA> listaCriptografia = null;

        public Criptografia()
        {
            this.CargarDatos();
        }

        private void CargarDatos()
        {
            throw new NotImplementedException();
        }

        public DINAMICA_CRIPTOGRAFIA ObtenerCriptografiaPorPasoDinamica(PASO_DINAMICA pasoDinamica)
        {
            return (from t in listaCriptografia
                    where t.DNC_CODIGO == pasoDinamica.PDT_PASO
                    select t).SingleOrDefault();
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMEnsaje, PASO_DINAMICA pasoDinamica)
        {
            return new CriptografiaMensaje(ObtenerCriptografiaPorPasoDinamica(pasoDinamica), pasoDinamica);
        }
    }
}
