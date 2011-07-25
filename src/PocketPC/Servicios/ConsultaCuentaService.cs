using PocketPC.Mensajeria;

namespace PocketPC.Servicios
{
    class ConsultaCuentaService
    {
        public static Mensaje SolicitarConsulta()
        {
            ConsultaCuentas mensaje = new ConsultaCuentas();
            string respuesta = Servidor.EnviarRecibir(mensaje.Trama);
            return null;
        }
    }
}
