using BusinessEntity;
using Mensajeria.Mensajes;

namespace Switch.Dinamica
{
    public enum EnumResultadoPaso
    { 
        Correcto=1,
        Incorrecto=2,
        Descartar=3,
        Recibir=4,
        Enviar=5
    }

    public abstract class Paso
    {
        public PASO_DINAMICA pasoDinamica { get; private set; }

        protected Paso(PASO_DINAMICA pasoDinamica)
        {
            this.pasoDinamica = pasoDinamica;
        }


        public abstract EnumResultadoPaso EjecutarPaso(Mensaje mensajeOrigen);
    }


}
