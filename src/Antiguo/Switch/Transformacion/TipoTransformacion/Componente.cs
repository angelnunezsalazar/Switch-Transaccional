using System;
using BusinessEntity;
using Switch.DA;
using Utilidades;

namespace Switch.Transformacion.TipoTransformacion
{
    public class Componente : Concatenacion
    {
        private readonly IDllDinamica DllDinamica;

        private const string SEPARADOR = ";";

        public Componente(IDllDinamica dllDinamica, IFactoryDA factoryDA)
            : base(factoryDA)
        {
            DllDinamica = dllDinamica;
            separador = SEPARADOR;
        }

        protected override string TransformacionEspecifica(TRANSFORMACION_CAMPO entidadTransformacion, string concatenacion)
        {
            string[] parametros = concatenacion.Remove(concatenacion.Length-1,1).Split(Convert.ToChar(SEPARADOR));
            object retorno = DllDinamica.Ejecutar(entidadTransformacion.TCM_COMPONENTE, entidadTransformacion.TCM_CLASE
                                 , entidadTransformacion.TCM_METODO, parametros);

            return Convert.ToString(retorno);
        }
    }
}