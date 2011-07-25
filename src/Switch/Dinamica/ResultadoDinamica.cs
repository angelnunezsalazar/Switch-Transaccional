using BusinessEntity;

namespace Switch.Dinamica
{
    public class ResultadoDinamica
    {
        public EnumResultadoDinamica Tipo { get; private set; }
        public ENTIDAD_COMUNICACION EntidadComunicacion { get; set; }

        public ResultadoDinamica(EnumResultadoDinamica enumResultadoDinamica)
        {
            Tipo = enumResultadoDinamica;
        }
    }
}