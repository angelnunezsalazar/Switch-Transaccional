using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using Mensajeria.Mensajes;
using Switch.DA;
using Switch.Dinamica;
using Utilidades;

namespace Switch.Validacion
{
    public class Validacion : Componente
    {
        private ValidacionCampo validacionCampo;

        List<GRUPO_VALIDACION> listaGrupoValidacion;

        public Validacion(IFactoryDA factoryDA, IDllDinamica dllDinamica)
        {
            listaGrupoValidacion = factoryDA.GrupoValidacion();
            validacionCampo = new ValidacionCampo(factoryDA, dllDinamica);
        }

        public EnumResultadoPaso Validar(PASO_DINAMICA pasodinamica, Mensaje mensaje)
        {
            GRUPO_VALIDACION grupo = ObtenerGrupoValidacionPorPasoDinamica(pasodinamica);

            List<VALIDACION_CAMPO> listaVCampo = grupo.VALIDACION_CAMPO.ToList();

            foreach (VALIDACION_CAMPO vcampo in listaVCampo)
            {
                Valor valor = mensaje.Campo(vcampo.CAMPO).ValorCuerpo;
                bool pasaValidacion = validacionCampo.Validar(vcampo, valor); ;

                if (!pasaValidacion)
                    break;
            }
            return EnumResultadoPaso.Correcto;
        }


        private GRUPO_VALIDACION ObtenerGrupoValidacionPorPasoDinamica(PASO_DINAMICA pasoDinamica)
        {
            return (from t in listaGrupoValidacion
                    where t.GRV_CODIGO == pasoDinamica.PDT_PASO
                    select t).SingleOrDefault();
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje, PASO_DINAMICA pasoDinamica)
        {
            return new ValidacionMensaje(this, pasoDinamica);
        }
    }
}
