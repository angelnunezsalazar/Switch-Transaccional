using System.Collections.Generic;
using System.Linq;
using BusinessEntity;
using Switch.DA;
using Switch.Dinamica;
using Utilidades;

namespace Switch.Transformacion
{
    public class Transformacion : Componente
    {
        public IFactoryDA FactoryDa { get; private set; }
        public IDllDinamica DllDinamica { get; set; }
        List<TRANSFORMACION> listaTransformacion;

        public Transformacion(IFactoryDA factoryDA, IDllDinamica dllDinamica)
        {
            FactoryDa = factoryDA;
            DllDinamica = dllDinamica;
            listaTransformacion = factoryDA.Transformacion();
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje, PASO_DINAMICA pasoDinamica)
        {
            return new TransformacionMensaje(dinamicaMensaje,ObtenerTransformacion(pasoDinamica),
                pasoDinamica, FactoryDa, DllDinamica);
        }

        public TRANSFORMACION ObtenerTransformacion(PASO_DINAMICA pasoDinamica)
        {
            return (from t in listaTransformacion
                    where t.TRM_CODIGO == pasoDinamica.PDT_PASO
                    select t).SingleOrDefault();
        }
    }
}

