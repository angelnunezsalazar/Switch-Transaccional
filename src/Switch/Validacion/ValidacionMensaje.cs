using BusinessEntity;
using Mensajeria.Mensajes;
using Switch.Dinamica;

namespace Switch.Validacion
{
    public class ValidacionMensaje:Paso
    {
        private readonly PASO_DINAMICA paso;
        private readonly Validacion validacion;

        public ValidacionMensaje(Validacion validacion, PASO_DINAMICA pasoDinamica) : base(pasoDinamica)
        {
            this.paso = pasoDinamica;
            this.validacion = validacion;
        }

        public override EnumResultadoPaso EjecutarPaso(Mensaje mensajeOrigen)
        {
            return this.validacion.Validar(paso, mensajeOrigen);
        }
    }
}