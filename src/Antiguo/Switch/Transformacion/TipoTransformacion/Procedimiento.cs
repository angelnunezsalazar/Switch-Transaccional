using BusinessEntity;
using Switch.DA;

namespace Switch.Transformacion.TipoTransformacion
{
    public class Procedimiento : Concatenacion
    {
        private const string SEPARADOR= ";";

        public Procedimiento(IFactoryDA factoryDA)
            : base(factoryDA)
        {
            separador = SEPARADOR;
        }

        protected override string TransformacionEspecifica(TRANSFORMACION_CAMPO entidadTransformacion, string concatenacion)
        {
            //TipoTransformacionDA.ejecutarTransformacionProcedimiento(entidadTransformacion.TCM_PROCEDIMIENTO, parametroProcedimiento.ToString());
            return FactoryDa.TransformarProcedure(entidadTransformacion.TCM_PROCEDIMIENTO,concatenacion);
        }

    }
}
