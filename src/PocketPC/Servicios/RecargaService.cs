using PocketPC.Mensajeria;
using SaveSettings;

namespace PocketPC.Servicios
{
    public class RecargaService
    {
        public static Mensaje EnviarRecarga(Mensaje mensaje)
        {
            string respuesta = Servidor.EnviarRecibir(mensaje.Trama);
            Settings.NumeroTransaccion++;
            return null;
        }
    }
}
