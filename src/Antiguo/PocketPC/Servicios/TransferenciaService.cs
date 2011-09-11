using PocketPC.Mensajeria;
using SaveSettings;

namespace PocketPC.Servicios
{
    public class TransferenciaService
    {
        public static Mensaje EnviarTransferencia(Mensaje mensaje)
        {
            string respuesta = Servidor.EnviarRecibir(mensaje.Trama);
            Settings.NumeroTransaccion++;
            return null;
        }
    }
}
