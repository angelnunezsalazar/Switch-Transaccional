using BusinessEntity;
using Switch.Dinamica;

namespace Switch.Descartar
{
    public class Descartar : Paso, Componente
    {
        public Descartar()
            : base(null)
        {

        }

        public override EnumResultadoPaso EjecutarPaso(Mensajeria.Mensajes.Mensaje mensajeOrigen)
        {
            return EnumResultadoPaso.Descartar;
        }

        public Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje, PASO_DINAMICA pasoDinamica)
        {
            return this;
        }
    }
}
