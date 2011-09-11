
using PocketPC.Mensajeria;
namespace PocketPC.Servicios
{
    public class AperturaService
    {
        public static Mensaje RealizarApertura(Mensaje mensaje)
        {
            string respuesta = Servidor.EnviarRecibir(mensaje.Trama);
            return null;
        }
    }
}
