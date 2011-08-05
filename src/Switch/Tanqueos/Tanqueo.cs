using System.Collections.Generic;
using System.Linq;
using Mensajeria.Mensajes;
using Switch.Dinamica;

namespace Switch.Tanqueos
{
    internal struct MensajeTanqueado
    {
        public DinamicaDeMensaje dinamica;
    }

    public class Tanqueo
    {
        private  List<MensajeTanqueado> MensajesTanqueados = new List<MensajeTanqueado>();

        public  void Tanquear(DinamicaDeMensaje dinamica)
        {
            MensajeTanqueado mensajeTanqueado;
            mensajeTanqueado.dinamica = dinamica;
            MensajesTanqueados.Add(mensajeTanqueado);
        }

        public  DinamicaDeMensaje Destanquear(Mensaje mensaje)
        {
            MensajeTanqueado mensajeTanqueado = MensajesTanqueados
                .Where(m => m.dinamica.Mensaje.EsMensajeTanqueado(mensaje))
                .SingleOrDefault();
            MensajesTanqueados.Remove(mensajeTanqueado);
            mensajeTanqueado.dinamica.Mensaje = mensaje;
            return mensajeTanqueado.dinamica;
        }
    }
}