using BusinessEntity;
using Switch.Dinamica;

namespace Switch
{
    public interface Componente
    {
        Paso ObtenerPaso(DinamicaDeMensaje dinamicaMensaje,PASO_DINAMICA pasoDinamica);
    }
}
